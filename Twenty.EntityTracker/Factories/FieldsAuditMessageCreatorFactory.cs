﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twenty.EntityTracker.Interfaces;

namespace Twenty.EntityTracker.Factories
{
    public class FieldsAuditMessageCreatorFactory
    {
        public IChangedFieldsAuditMessage CreateFieldsAuditMessageCreator()
        {
            return new FieldsMessageCreator();
        }
    }
}
