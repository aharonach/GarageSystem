using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal static class VehicleCreator
    {
        public static Vehicle Create(eType i_VehicleType, string i_License, string i_ModelName)
        {
            Vehicle createdVehicle = null;

            switch(i_VehicleType)
            {
                case eType.FuelCar:
                    createdVehicle = new FuelCar(i_License, i_ModelName);
                    break;

                case eType.ElectricCar:
                    createdVehicle = new ElectricCar(i_License, i_ModelName);
                    break;

                case eType.FuelMotorcycle:
                    createdVehicle = new FuelMotorcycle(i_License, i_ModelName);
                    break;

                case eType.ElectricMotorcycle:
                    createdVehicle = new ElectricMotorcycle(i_License, i_ModelName);
                    break;

                case eType.Truck:
                    createdVehicle = new Truck(i_License, i_ModelName);
                    break;
            }

            return createdVehicle;
        }

        public static Dictionary<int, string> GetAvailableTypes()
        {
            Dictionary<int, string> availableTypes = 
                new Dictionary<int, string> 
                {
                    { (int) eType.FuelCar, "Fuel Car" },
                    { (int) eType.ElectricCar, "Electric Car" },
                    { (int) eType.FuelMotorcycle, "Fuel Motorcycle" },
                    { (int) eType.ElectricMotorcycle, "Electric Motorcycle" },
                    { (int) eType.Truck, "Truck" }
                };

            return availableTypes;
        }

        public enum eType
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck
        }
    }
}
