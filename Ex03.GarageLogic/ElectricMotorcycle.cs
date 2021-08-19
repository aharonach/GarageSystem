namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryInHour = 1.8f;

        public ElectricMotorcycle(string i_License, string i_ModelName)
            : base(i_License, i_ModelName, new ElectricTank(k_MaxBatteryInHour, 0))
        {
        }
    }
}
