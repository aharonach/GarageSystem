namespace Ex03.GarageLogic
{
    class FuelMotorcycle : Motorcycle
    {
        private const int k_MaxFuelInLiter = 6;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan98;

        public FuelMotorcycle(string i_License, string i_ModelName)
            : base(i_License, i_ModelName)
        {
            VehicleTank = new Fuel(k_FuelType, k_MaxFuelInLiter, 0);
        }
    }
}
