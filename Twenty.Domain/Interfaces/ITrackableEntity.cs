using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twenty.EntityTracker.Interfaces;

namespace Twenty.Domain.Interfaces
{
    public interface ITrackableEntity
    {
       void Audit(IAuditable entity);
       string GetChangedPropertiesAuditMessage(IPropertiesAuditMessage entity);
       string GetChangedFieldsAuditMessage(IFieldsAuditMessage entity);

    }
}
