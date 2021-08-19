namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaxBatteryInHour = 3.2f;

        public ElectricCar(string i_License, string i_ModelName)
            : base(i_License, i_ModelName)
        {
            Tank = new ElectricTank(k_MaxBatteryInHour, 0);
        }
    }
}