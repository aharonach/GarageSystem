using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
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

        public static Dictionary<string, eType> GetAvailableTypes()
        {
            Dictionary<string, eType> availableTypes = 
                new Dictionary<string, eType> 
                {
                    { "Fuel Car", eType.FuelCar },
                    { "ElectricTank Car", eType.ElectricCar },
                    { "Fuel Motorcycle", eType.FuelMotorcycle },
                    { "ElectricTank Motorcycle", eType.ElectricMotorcycle },
                    { "Truck", eType.Truck }
                };
            return availableTypes;
        }

        public enum eType
        {
            FuelCar,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck
        }
    }
}
