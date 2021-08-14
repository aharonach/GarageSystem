using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private eColor m_Color;
        private eDoors m_NumOfDoors;

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
