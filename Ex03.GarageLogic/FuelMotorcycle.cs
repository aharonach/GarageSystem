﻿namespace Ex03.GarageLogic
{
    class FuelMotorcycle : Motorcycle
    {
        private const int k_MaxFuelInLiter = 6;
        private const FuelTank.eFuelType k_FuelType = FuelTank.eFuelType.Octan98;

        public FuelMotorcycle(string i_License, string i_ModelName)
            : base(i_License, i_ModelName)
        {
            Tank = new FuelTank(k_FuelType, k_MaxFuelInLiter, 0);
        }
    }
}
