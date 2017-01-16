using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twenty.EntityTracker.Interfaces;

namespace Twenty.EntityTracker
{
    public class PropertiesMessageCreator : IChangedPropertiesAuditMessage
    {
        public string GetChangedPropertiesAuditMessage()
        {
            List<MemberAudit> changedPropertiesList = GetChangedProperties();
            StringBuilder sb = new StringBuilder();
            foreach (var ma in changedPropertiesList)
                sb.AppendFormat("property {0}, original value: {1}, new value: {2}{3}", ma.Key, ma.PreviousValue, ma.CurrentValue, Environment.NewLine);

            return sb.ToString();
        }
        private List<MemberAudit> GetChangedProperties()
        {
            var previous = Tracker.PreviousPropertyValues;
            var current = Tracker.CurrentPropertyValues;
            var query = from p in previous
                        let c = current[p.Key]
                        where IsNotSameValue(p.Value, c)
                        orderby p.Key
                        select new MemberAudit { Key = p.Key, PreviousValue = p.Value, CurrentValue = c };
            return query.ToList();
        }
        private bool IsNotSameValue(object x, object y)
        {
            if (x == null && y == null)
                return false;
            else if (x == null && y != null)
                return true;
            else if (x != null && y == null)
                return true;
            else
                return ((IComparable)x).CompareTo(((IComparable)y)) != 0;
        }
        public EntityTrackerBase Tracker { get; set; }

    }
}
