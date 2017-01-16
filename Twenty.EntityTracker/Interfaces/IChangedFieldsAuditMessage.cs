using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twenty.EntityTracker.Interfaces
{
    public interface IChangedFieldsAuditMessage
    {
        string GetChangedFieldsAuditMessage();
        EntityTrackerBase Tracker { get; set; }
    }
}
