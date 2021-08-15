using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    class Garage
    {
        private readonly Dictionary<string, VehicleInGarage> r_Vehicles;

        public Garage()
        {
            r_Vehicles = new Dictionary<string, VehicleInGarage>();
        }

        public void AddVehicle(string i_Person, string i_Phone, Vehicle i_Vehicle)
        {

        }

        public eVehicleType GetVehicleType(Vehicle i_Vehicle)
        {
            eVehicleType type;

            if (i_Vehicle is Truck)
            {
                type = eVehicleType.Truck;
            } 
            else
            {
                type = eVehicleType.ElectricCar;
            }

            return type;
        }

        public enum eVehicleType
        {
            ElectricMotorcycle,
            ElectricCar,
            FuelCar,
            FuelMotorcycle,
            Truck
        }
    }
}
