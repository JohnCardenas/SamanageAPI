using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SamanageAPI.Utility;

namespace SamanageAPI
{
    public abstract class SamanageObject : ModelBase
    {
        #region Fields
        private bool _dirtyObject = false;
        private bool _readOnly = true;
        private dynamic _originalObject;
        private dynamic _currentObject;
        private SamanageClient _client;
        #endregion // Fields

        #region Properties
        /// <summary>
        /// Data layer for this object at the time of creation or retrieval
        /// </summary>
        internal dynamic OriginalObject
        {
            get { return _originalObject; }
            set { SetProperty(ref _originalObject, value); }
        }

        /// <summary>
        /// Data layer for modifications to this object
        /// </summary>
        internal dynamic CurrentObject
        {
            get { return _currentObject; }
            set { SetProperty(ref _currentObject, value); }
        }

        /// <summary>
        /// Indicates that this object has pending, uncommitted changes.
        /// </summary>
        public bool IsDirty
        {
            get { return _dirtyObject; }
            internal set { SetProperty(ref _dirtyObject, value); }
        }

        /// <summary>
        /// Indicates if this object can be reverted to the original values.
        /// </summary>
        public bool IsRevertible
        {
            get
            {
                return !ReferenceEquals(null, OriginalObject);
            }
        }

        /// <summary>
        /// Indicates that this object is read-only.
        /// </summary>
        public bool ReadOnly
        {
            get { return _readOnly; }
            internal set { SetProperty(ref _readOnly, value); }
        }

        /// <summary>
        /// Reference to a SamanageClient, used for commits and other operations.
        /// </summary>
        private SamanageClient Client { get; set; }
        #endregion // Properties

        #region Constructor
        /// <summary>
        /// Creates a new SamanageObject from a JSON data object.
        /// </summary>
        /// <param name="obj">JSON data object</param>
        /// <param name="existingObject">If true, this object will be set up to allow reversion.</param>
        /// <param name="readOnly">If true, this object will be marked read-only.</param>
        internal SamanageObject(ExpandoObject obj, bool existingObject = false, bool readOnly = true)
        {
            CurrentObject = obj.DeepCopy();

            if (existingObject)
                OriginalObject = obj;

            ReadOnly = readOnly;
        }

        /// <summary>
        /// Creates a copy of another SamanageObject.
        /// </summary>
        /// <param name="otherObject"></param>
        internal SamanageObject(SamanageObject otherObject)
        {
            CurrentObject = (otherObject.CurrentObject as ExpandoObject).DeepCopy();
            OriginalObject = otherObject.OriginalObject;
            IsDirty = otherObject.IsDirty;
            ReadOnly = otherObject.ReadOnly;
        }

        /// <summary>
        /// Creates a blank SamanageObject belonging to the specified client.
        /// </summary>
        internal SamanageObject(SamanageClient client)
        {
            CurrentObject = new ExpandoObject();
            Client = client;
            ReadOnly = false;
        }
        #endregion // Constructor

        #region Model Methods
        /// <summary>
        /// Reverts pending changes by discarding the current values.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when attempting to revert an object where IsRevertible is false.</exception>
        public void Revert()
        {
            if (!IsRevertible)
                throw new InvalidOperationException("Cannot revert a non-revertible object.");

            CurrentObject = (OriginalObject as ExpandoObject).DeepCopy();
            Validate();
        }

        /// <summary>
        /// Commits the changes to this object to the Samanage API
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
        public bool Commit()
        {
            if (Client == null)
                throw new InvalidOperationException("Attempted commit of object without an associated client!");

            if (HasErrors)
                return false;

            return Client.Commit(this);
        }
        #endregion // Model Methods

        #region Model Property Getter Methods
        /// <summary>
        /// Gets a primitive value by property name.
        /// </summary>
        /// <typeparam name="T">Primitive type</typeparam>
        /// <param name="modelProperty">Primitive's data model property name</param>
        /// <returns></returns>
        protected T GetPrimitiveValue<T>(string modelProperty)
        {
            if ((CurrentObject as ExpandoObject).HasProperty(modelProperty))
            {
                return (CurrentObject as ExpandoObject).GetProperty<T>(modelProperty);
            }

            return default(T);
        }

        /// <summary>
        /// Gets a reference value by property name.
        /// </summary>
        /// <param name="modelProperty">Model property name</param>
        /// <param name="type">Reference type</param>
        /// <returns></returns>
        protected string GetReferenceValue(string modelProperty, ReferenceType type)
        {
            if ((CurrentObject as ExpandoObject).HasProperty(modelProperty))
            {
                string propertyRef = SamanageReference.ReferenceTypeToString(type);
                var propertyObj = (CurrentObject as ExpandoObject).GetProperty<ExpandoObject>(modelProperty);

                if (propertyObj.HasProperty(propertyRef))
                {
                    return propertyObj.GetProperty<string>(propertyRef);
                }
                else
                    return null;
            }

            return null;
        }
        #endregion // Getter Methods

        #region Model Property Setter Methods
        /// <summary>
        /// Sets a primitive value by property name
        /// </summary>
        /// <typeparam name="T">Type of the primitive</typeparam>
        /// <param name="modelProperty">Primitive's data model property name name</param>
        /// <param name="value">Primitive value</param>
        /// <param name="objectProperty">Name of the property of the object that is changing.</param>
        protected void SetPrimitiveValue<T>(string modelProperty, T value, [CallerMemberName] string objectProperty = null)
        {
            if (this.ReadOnly)
                throw new InvalidOperationException("Cannot set value; object is read-only.");

            var objectData = (IDictionary<string, object>)CurrentObject;

            OnPropertyChanging(objectProperty);

            if (value == null)
                objectData[modelProperty] = null;
            else
                objectData[modelProperty] = value.ToString();

            IsDirty = true;

            OnPropertyChanged(objectProperty);
        }

        /// <summary>
        /// Sets a reference value by property name
        /// </summary>
        /// <param name="modelProperty">Name of the property in the data model</param>
        /// <param name="refType">Reference type</param>
        /// <param name="value">Value to set</param>
        /// <param name="objectProperty">Name of the property of the object that is changing.</param>
        protected void SetReferenceValue(string modelProperty, ReferenceType refType, string value, [CallerMemberName] string objectProperty = null)
        {
            if (this.ReadOnly)
                throw new InvalidOperationException("Cannot set value; object is read-only.");

            var objectData = (IDictionary<string, object>)CurrentObject;

            if (value == null)
                objectData[modelProperty] = null;
            else
            {
                ExpandoObject refObj = new ExpandoObject();
                var refData = (IDictionary<string, object>)refObj;

                string propertyRef = SamanageReference.ReferenceTypeToString(refType);
                refData[propertyRef] = value;

                OnPropertyChanging(objectProperty);
                objectData[modelProperty] = refObj;
                OnPropertyChanged(objectProperty);
            }
        }
        #endregion // Setter Methods
    }
}
