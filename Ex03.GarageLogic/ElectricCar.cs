using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class ElectricCar : Car
    {
        private const float k_MaxBatteryInHour = 3.2f;

        public ElectricCar(string i_License, string i_ModelName) 
            : base (i_License, i_ModelName)
        {
            VehicleTank = new Electric(k_MaxBatteryInHour, 0);
        }
    }
}
