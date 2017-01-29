using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twenty.EntityTracker.Interfaces;

namespace Twenty.EntityTracker.Factories
{
    public class PropertiesMessageCreatorFactory
    {
        public IChangedPropertiesAuditMessage CreatePropertiesMessageCreator()
        {
            return new PropertiesMessageCreator();
        }
    }
}
