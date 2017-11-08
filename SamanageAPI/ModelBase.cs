using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SamanageAPI
{
    public class ModelBase : INotifyPropertyChanging, INotifyPropertyChanged, INotifyDataErrorInfo
    {
        #region Fields
        private ConcurrentDictionary<string, List<string>> _errors = new ConcurrentDictionary<string, List<string>>();
        private object _lock = new object();
        #endregion // Fields

        #region Events
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        #endregion // Events

        #region Properties
        /// <summary>
        /// Model validation. If this returns true, some properties of the model have failed validation.
        /// </summary>
        public bool HasErrors
        {
            get { return _errors.Any(kv => kv.Value != null && kv.Value.Count > 0); }
        }
        #endregion // Properties

        #region Getter Methods
        /// <summary>
        /// Fetches validation errors for the specified property name
        /// </summary>
        /// <param name="propertyName">Name of the property to check for validation errors</param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errorsForName;
            _errors.TryGetValue(propertyName, out errorsForName);
            return errorsForName;
        }
        #endregion // Getter Methods

        #region Setter Methods
        /// <summary>
        /// Sets the specified property and triggers appropriate events.
        /// </summary>
        /// <typeparam name="T">Object class</typeparam>
        /// <param name="storage">Backing field</param>
        /// <param name="value">Value to set</param>
        /// <param name="propertyName">Name of the property to set</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            OnPropertyChanging(propertyName);
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion // Setter Methods

        #region Event Methods
        /// <summary>
        /// Triggers the ErrorsChanged event
        /// </summary>
        /// <param name="propertyName">Property name triggering the event</param>
        protected void OnErrorsChanged([CallerMemberName] string propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Triggers the PropertyChanging event
        /// </summary>
        /// <param name="propertyName">Property name triggering the event</param>
        protected void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Triggers the PropertyChanged event and kicks off validation.
        /// </summary>
        /// <param name="propertyName">Property name triggering the event</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ValidateAsync();
        }
        #endregion // Event Methods

        #region Validation Methods
        /// <summary>
        /// Triggers an asynchronous validation of all properties in this model.
        /// </summary>
        /// <returns></returns>
        public Task ValidateAsync()
        {
            return Task.Run(() => Validate());
        }

        /// <summary>
        /// Triggers a synchronous validation of all properties in this model.
        /// </summary>
        public void Validate()
        {
            lock (_lock)
            {
                ValidationContext validationContext = new ValidationContext(this, null, null);
                List<ValidationResult> validationResults = new List<ValidationResult>();
                Validator.TryValidateObject(this, validationContext, validationResults, true);

                foreach (KeyValuePair<string, List<string>> kv in _errors.ToList())
                {
                    if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                    {
                        List<string> outList;
                        _errors.TryRemove(kv.Key, out outList);
                        OnErrorsChanged(kv.Key);
                    }
                }

                IEnumerable<IGrouping<string, ValidationResult>> q = from r in validationResults
                                                                     from m in r.MemberNames
                                                                     group r by m into g
                                                                     select g;

                foreach (var prop in q)
                {
                    List<string> messages = prop.Select(r => r.ErrorMessage).ToList();

                    if (_errors.ContainsKey(prop.Key))
                    {
                        List<string> outList;
                        _errors.TryRemove(prop.Key, out outList);
                    }
                    _errors.TryAdd(prop.Key, messages);
                    OnErrorsChanged(prop.Key);
                }
            }
        }
        #endregion // Validation Methods
    }
}
