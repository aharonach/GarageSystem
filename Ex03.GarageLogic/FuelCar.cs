namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private const int k_MaxFuelInLiter = 45;
        private const FuelTank.eFuelType k_FuelType = FuelTank.eFuelType.Octan95;

        public FuelCar(string i_License, string i_ModelName)
            : base(i_License, i_ModelName, new FuelTank(k_FuelType, k_MaxFuelInLiter, 0))
        {
        }
    }
}
