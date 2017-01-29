using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twenty.Domain.Interfaces;
using Twenty.EntityTracker.Interfaces;

namespace Twenty.Domain
{
    /// <summary>
    /// Entity must derive from EntityTrackerBase, but can be any class
    /// </summary>
    public class EntityTrackerService:ITrackableEntity
    {
        public void Audit(IAuditable entity)
        {
            entity.Audit();
        }

        public string GetChangedFieldsAuditMessage(IFieldsAuditMessage entity)
        {
            string message = entity.GetChangedFieldsAuditMessage();
            return message;
        }

        public string GetChangedPropertiesAuditMessage(IPropertiesAuditMessage entity)
        {
            string message = entity.GetChangedPropertiesAuditMessage();
            return message;
        }
    }
}
