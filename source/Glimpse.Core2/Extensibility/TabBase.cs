﻿using System;

namespace Glimpse.Core2.Extensibility
{
    public abstract class TabBase:ITab
    {
        public abstract object GetData(ITabContext context);

        public abstract string Name{get; }

        public virtual RuntimeEvent ExecuteOn
        {
            get { return RuntimeEvent.EndRequest; }
        }

        public Type RequestContextType
        {
            get { return null; }
        }
    }
}