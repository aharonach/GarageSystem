using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        protected const float k_MaxWheelAirPressure = 26;
        private const int k_NumOfWheels = 16;
        private float m_MaxCarryingCapacity;
        private bool m_DrivesDangerousMaterials;

        public Truck(string i_License, string i_Name) : base(i_License, i_Name, k_NumOfWheels, k_MaxWheelAirPressure)
        {
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
