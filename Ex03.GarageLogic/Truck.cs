using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private readonly float r_MaxCarryingCapacity;
        private bool m_DrivesDangerousMaterials;

        public Truck(
            string i_License,
            List<Wheel> i_Wheels,
            string i_Name,
            Fuel i_FuelTank,
            float i_MaxCarryingCapacity,
            bool i_DrivesDangerousMaterials) 
            : base(i_License, i_Wheels, i_Name, i_FuelTank)
        {
            r_MaxCarryingCapacity = i_MaxCarryingCapacity;
            DrivesDangerousMaterials = i_DrivesDangerousMaterials;
        }

        public float MaxCarryingCapacity
        {
            get { return r_MaxCarryingCapacity; }
        }

        public bool DrivesDangerousMaterials
        {
            get { return m_DrivesDangerousMaterials; }
            set { m_DrivesDangerousMaterials = value; }
        }
    }
}
