﻿namespace Ex03.GarageLogic
{
    class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryInHour = 1.8f;

        public ElectricMotorcycle(string i_License, string i_ModelName)
            : base(i_License, i_ModelName)
        {
            VehicleTank = new Electric(k_MaxBatteryInHour, 0);
        }
    }
}
