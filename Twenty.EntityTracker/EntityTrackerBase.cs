using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Twenty.EntityTracker.Interfaces;

namespace Twenty.EntityTracker
{
    /// <summary>
    /// base class for any class/entity that needs to be tracked and audited
    /// </summary>
    public abstract class EntityTrackerBase : ITrackable
    {
        // TODO: use factory method to instantiate FieldsMessageCreator, PropertiesMessageCreator
        // did not have time with current implementation, however I created the interfaces that will decouple
        // the tracking of entities from the creation of audit messages so that they can vary independently 
        // as there is a good chance that messages will change in the future (bridge pattern)
        protected EntityTrackerBase()
        {
            // Will reference the derived type/class
            entityType = this.GetType();
            FieldsAuditMessageCreator = new FieldsMessageCreator();
            PropertiesAuditMessageCreator = new PropertiesMessageCreator();
        }
        /// <summary>
        /// Client should call Audit method after entity queried from database and before entity is saved to database
        /// </summary>
        public virtual void Audit()
        {
            CopyCurrentToPreviousMembers();
            SetCurrentValues();
        }

        /// <summary>
        /// each public field and property is preserved into a dictionary of field/property name and current value
        /// will track only primitive data types
        /// </summary>
        private void SetCurrentValues()
        {
            MemberInfo[] minfos = entityType.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (var memberInfo in minfos)
            {
                if (memberInfo.MemberType == MemberTypes.Property)
                {
                    currentPropertyValues[memberInfo.Name] = GetPropertyValue(memberInfo.Name);
                }

                else if (memberInfo.MemberType == MemberTypes.Field)
                {
                    currentFieldValues[memberInfo.Name] = GetFieldValue(memberInfo.Name);
                }
            }
        }

        /// <summary>
        /// Will return property value for a class based on property name and type
        /// </summary>
        /// <param name="name">property name</param>
        /// <returns>property value</returns>
        private object GetPropertyValue(string name)
        {
            return entityType.GetProperty(name).GetValue(this);
        }

        /// <summary>
        /// Will return fild value for a type
        /// </summary>
        /// <param name="name">field name</param>
        /// <returns>field value</returns>
        private object GetFieldValue(string name)
        {
            return entityType.GetField(name).GetValue(this);
        }

        /// <summary>
        /// Will preserve entities' previous fields and properties values
        /// </summary>
        private void CopyCurrentToPreviousMembers()
        {
            previousFieldValues = currentFieldValues;
            previousPropertyValues = currentPropertyValues;
            currentFieldValues = new Dictionary<string, object>();
            currentPropertyValues = new Dictionary<string, object>();
        }

        /// <summary>
        /// Will call an implementor that formats audit message for changed properties
        /// </summary>
        /// <returns></returns>
        public string GetChangedPropertiesAuditMessage()
        {
            return propertiesAuditMessageCreator.GetChangedPropertiesAuditMessage();
        }

        /// <summary>
        /// Will call an implementor that formats audit message for changed fields
        /// </summary>
        /// <returns></returns>
        public string GetChangedFieldsAuditMessage()
        {
            return fieldsAuditMessageCreator.GetChangedFieldsAuditMessage();
        }

        /// <summary>
        /// TODO: set implementor with a factory method
        /// </summary>
        public IChangedFieldsAuditMessage FieldsAuditMessageCreator
        {
            get
            {
                return fieldsAuditMessageCreator;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("FieldsAuditMessageCreator");
                fieldsAuditMessageCreator = value;
                fieldsAuditMessageCreator.Tracker = this;
            }
        }

        /// <summary>
        /// TODO: set implementor with a factory method
        /// </summary>
        public IChangedPropertiesAuditMessage PropertiesAuditMessageCreator
        {
            get
            {
                return propertiesAuditMessageCreator;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("PropertiesAuditMessageCreator");
                propertiesAuditMessageCreator = value;
                propertiesAuditMessageCreator.Tracker = this;
            }
        }

        public Dictionary<string, object> PreviousFieldValues
        {
            get { return previousFieldValues; }
        }
        public Dictionary<string, object> CurrentFieldValues
        {
            get { return currentFieldValues; }
        }
        public Dictionary<string, object> PreviousPropertyValues
        {
            get { return previousPropertyValues; }
        }
        public Dictionary<string, object> CurrentPropertyValues
        {
            get { return currentPropertyValues; }
        }

        private IChangedFieldsAuditMessage fieldsAuditMessageCreator;
        private IChangedPropertiesAuditMessage propertiesAuditMessageCreator;
        private Dictionary<string, object> previousFieldValues = new Dictionary<string, object>();
        private Dictionary<string, object> currentFieldValues = new Dictionary<string, object>();
        private Dictionary<string, object> previousPropertyValues = new Dictionary<string, object>();
        private Dictionary<string, object> currentPropertyValues = new Dictionary<string, object>();
        private Type entityType;

    }
}
