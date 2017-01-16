using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twenty.Domain.Interfaces;

namespace Twenty.Domain.NinjectModules
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ITrackableEntity>().To<EntityTrackerService>();
        }
    }
}
