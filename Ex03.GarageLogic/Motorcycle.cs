using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private readonly int r_EngineCapacity;
        private eLicenseType m_LicenseType;

        public Motorcycle(
            string i_License,
            List<Wheel> i_Wheels,
            string i_Name,
            Tank i_Tank,
            int i_EngineCapacity,
            eLicenseType i_LicenseType) : base(i_License, i_Wheels, i_Name, i_Tank)
        {
            VehicleTank = i_Tank;
            r_EngineCapacity = i_EngineCapacity;
            m_LicenseType = i_LicenseType;
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
