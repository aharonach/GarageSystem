using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        //private static readonly Dictionary<eType, string> sr_Types = 
        //    new Dictionary<eType, string>()
        //     {
        //         { eType.FuelCar, "Fuel Car" },
        //         { eType.ElectricCar, "Electric Car" },
        //         { eType.FuelMotorcycle, "Fuel Motorcycle" },
        //         { eType.ElectricMotorcycle, "Electric Motorcycle" },
        //         { eType.Truck, "Truck" }
        //     };

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

        public static Array GetAvailableTypes()
        {
            return Enum.GetValues(typeof(eType));
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
