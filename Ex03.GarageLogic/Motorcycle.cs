using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        private const float k_MaxWheelAirPressure = 32;
        private const int k_NumOfWheels = 2;
        private eLicenseType m_LicenseType;

        protected Motorcycle(string i_License, string i_Name, Tank i_Tank)
            : base(i_License, i_Name, k_NumOfWheels, k_MaxWheelAirPressure, i_Tank)
        {
        }

        public override Dictionary<string, PropertyInfo> GetFieldsToUpdate()
        {
            Dictionary<string, PropertyInfo> res =
                new Dictionary<string, PropertyInfo>
                {
                    { "License type", GetType().GetProperty("LicenseType") },
                };

            return res;
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public enum eLicenseType
        {
            A,
            B1,
            AA,
            BB
        }
    }
}
