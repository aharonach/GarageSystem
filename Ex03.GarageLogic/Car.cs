using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColor m_Color;
        private eDoors m_NumOfDoors;

        public Car(
            string i_License, 
            List<Wheel> i_Wheels,
            string i_Name,
            Tank i_Tank,
            eColor i_Color,
            eDoors i_Doors) 
            : base(i_License, i_Wheels, i_Name, i_Tank)
        {
            Color = i_Color;
            NumOfDoors = i_Doors;
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
