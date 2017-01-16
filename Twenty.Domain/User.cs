﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twenty;
using Twenty.EntityTracker;

namespace Twenty.Domain
{
    public class User : EntityTrackerBase
    {
        public string city;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
