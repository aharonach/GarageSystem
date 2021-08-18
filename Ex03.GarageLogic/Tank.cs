using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Tank
    {
        public abstract eType Type
        {
            get;
        }

        public abstract float EnergyPercent
        {
            get;
        }

        public virtual Dictionary<string, object> GetFieldsValues()
        {
            Dictionary<string, object> fields =
                new Dictionary<string, object>
                {
                    { "Tank type", Type },
                    { "Energy (%)", EnergyPercent }
                };

            return fields;
        }

        public abstract Dictionary<string, PropertyInfo> GetFieldsToUpdate();

        public enum eType
        {
            Electric,
            Fuel
        }
    }
}
