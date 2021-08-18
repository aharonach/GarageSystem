using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_MaxFuelInLiter = 120;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Soler;
        protected const float k_MaxWheelAirPressure = 26;
        private const int k_NumOfWheels = 16;
        private float m_MaxCarryingCapacity;
        private bool m_DrivesDangerousMaterials;

        public Truck(string i_License, string i_Name) : base(i_License, i_Name, k_NumOfWheels, k_MaxWheelAirPressure)
        {
            VehicleTank = new Fuel(k_FuelType, k_MaxFuelInLiter, 0);
        }

        public override Dictionary<string, PropertyInfo> GetFieldsToUpdate()
        {
            Dictionary<string, PropertyInfo> res = 
                new Dictionary<string, PropertyInfo>
                    {
                        { "Max carrying capacity", GetType().GetProperty("MaxCarryingCapacity") },
                        { "Drives dangerous materials", GetType().GetProperty("DrivesDangerousMaterials") },
                    };
            return res;
        }

        public float MaxCarryingCapacity
        {
            get { return m_MaxCarryingCapacity; }
            set { m_MaxCarryingCapacity = value; }
        }

        public bool DrivesDangerousMaterials
        {
            get { return m_DrivesDangerousMaterials; }
            set { m_DrivesDangerousMaterials = value; }
        }
    }
}
