using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twenty.EntityTracker.Interfaces
{
    public interface IChangedPropertiesAuditMessage
    {
        string GetChangedPropertiesAuditMessage();
        EntityTrackerBase Tracker { get; set; }
    }
}
