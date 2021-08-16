using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        protected const float k_MaxWheelAirPressure = 32;
        protected const int k_NumOfWheels = 4;
        private eColor m_Color;
        private eDoors m_NumOfDoors;

        protected Car(string i_License, string i_Name) : base(i_License, i_Name, k_NumOfWheels, k_MaxWheelAirPressure)
        {
        }

        public eColor Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public eDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }

        public enum eColor
        {
            Red,
            Silver,
            White,
            Black
        }

        public enum eDoors
        {
            Two = 2,
            Three,
            Four,
            Five
        }
    }
}
