using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        protected const float k_MaxWheelAirPressure = 32;
        protected const int k_NumOfWheels = 2;
        private eLicenseType m_LicenseType;

        public Motorcycle(string i_License, string i_Name) : base(i_License, i_Name, k_NumOfWheels, k_MaxWheelAirPressure)
        {
        }

        public override Dictionary<string, PropertyInfo> GetFieldsToUpdate()
        {
            throw new System.NotImplementedException();
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
