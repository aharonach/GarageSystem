using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class FuelCar : Car
    {
        private const int k_MaxFuelInLiter = 45;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan95;

        public FuelCar(string i_License, string i_ModelName)
            : base(i_License, i_ModelName)
        {
            VehicleTank = new Fuel(k_FuelType, k_MaxFuelInLiter, 0);
        }
    }
}
