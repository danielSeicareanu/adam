using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twenty.EntityTracker
{
    public class MemberAudit
    {
        public string Key { get; set; }
        public object PreviousValue { get; set; }
        public object CurrentValue { get; set; }
    }
}
