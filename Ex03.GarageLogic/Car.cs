using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const float k_MaxWheelAirPressure = 32;
        private const int k_NumOfWheels = 4;
        private eColor m_Color;
        private eDoors m_NumOfDoors;

        protected Car(string i_License, string i_Name, Tank i_Tank)
            : base(i_License, i_Name, k_NumOfWheels, k_MaxWheelAirPressure, i_Tank)
        {
        }

        public override Dictionary<string, PropertyInfo> GetFieldsToUpdate()
        {
            Dictionary<string, PropertyInfo> res = 
                new Dictionary<string, PropertyInfo>
                {
                    { "Color", GetType().GetProperty("Color") },
                    { "Number Of Doors", GetType().GetProperty("NumOfDoors") }
                };

            return res;
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
