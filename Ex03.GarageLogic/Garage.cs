using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInGarage> r_Vehicles = new Dictionary<string, VehicleInGarage>();

        public Garage()
        {
        }

        public void AddVehicle(
            string i_PersonName,
            string i_Phone,
            VehicleFactory.eType i_VehicleType,
            string i_License,
            string i_ModelName)
        {
            Vehicle vehicle = VehicleFactory.Create(i_VehicleType, i_License, i_ModelName);

            VehicleInGarage vehicleInGarage = new VehicleInGarage(i_PersonName, i_Phone, vehicle);

            r_Vehicles.Add(i_License, vehicleInGarage);
        }

        protected Dictionary<string, PropertyInfo> GetVehiclePropertiesToUpdate(Vehicle i_Vehicle)
        {
            Dictionary<string, PropertyInfo> vehicleProperties = new Dictionary<string, PropertyInfo>();

            // Get fields of vehicle
            foreach (KeyValuePair<string, PropertyInfo> kvp in i_Vehicle.GetFieldsToUpdate())
            {
                vehicleProperties.Add(kvp.Key, kvp.Value);
            }

            // Get wheels fields
            foreach (KeyValuePair<string, PropertyInfo> kvp in i_Vehicle.GetFieldsToUpdateOfWheels())
            {
                vehicleProperties.Add(kvp.Key, kvp.Value);
            }

            // Get tank fields
            foreach (KeyValuePair<string, PropertyInfo> kvp in i_Vehicle.GetFieldsToUpdateOfTank())
            {
                vehicleProperties.Add(kvp.Key, kvp.Value);
            }

            return vehicleProperties;
        }

        public Dictionary<string, KeyValuePair<string, Type>> GetVehicleFieldsToUpdate(string i_License)
        {
            Dictionary<string, KeyValuePair<string, Type>> vehicleFields = new Dictionary<string, KeyValuePair<string, Type>>();
            Vehicle vehicle = r_Vehicles[i_License].Vehicle;

            // Get vehicle fields
            foreach(KeyValuePair<string, PropertyInfo> kvp in vehicle.GetFieldsToUpdate())
            {
                vehicleFields.Add(kvp.Key, new KeyValuePair<string, Type>(kvp.Value.Name, kvp.Value.PropertyType));
            }

            // Get wheels fields
            foreach(KeyValuePair<string, PropertyInfo> kvp in vehicle.GetFieldsToUpdateOfWheels())
            {
                vehicleFields.Add(kvp.Key, new KeyValuePair<string, Type>(kvp.Value.Name, kvp.Value.PropertyType));
            }

            // Get tank fields
            foreach(KeyValuePair<string, PropertyInfo> kvp in vehicle.GetFieldsToUpdateOfTank())
            {
                vehicleFields.Add(kvp.Key, new KeyValuePair<string, Type>(kvp.Value.Name, kvp.Value.PropertyType));
            }

            return vehicleFields;
        }

        private void CollectFieldsToUpdate(Dictionary<string, KeyValuePair<string, Type>> i_FieldsToCollect)
        {

        }

        public void UpdateVehicleFields(string i_License, Dictionary<string, object> i_FieldsWithValues)
        {
            // VehicleInGarage vehicleInGarage = GetVehicleFieldsToUpdate(i_License);
        }

        public Dictionary<string, object> GetVehicleFieldsAndValues(string i_License)
        {
            Vehicle vehicle = r_Vehicles[i_License].Vehicle;
            return vehicle.GetFieldsWithValues();
        }

        public Dictionary<string, eVehicleStatus> GetVehiclesLicenses()
        {
            Dictionary<string, eVehicleStatus> vehiclesLicenses = new Dictionary<string, eVehicleStatus>();

            foreach(VehicleInGarage vehicleInGarage in r_Vehicles.Values)
            {
                vehiclesLicenses.Add(vehicleInGarage.Vehicle.License, vehicleInGarage.Status);
            }

            return vehiclesLicenses;
        }

        public Dictionary<string, eVehicleStatus> GetVehiclesLicenses(eVehicleStatus i_Status)
        {
            Dictionary<string, eVehicleStatus> vehiclesLicenses = new Dictionary<string, eVehicleStatus>();

            foreach(KeyValuePair<string, eVehicleStatus> kvp in GetVehiclesLicenses())
            {
                if(kvp.Value == i_Status)
                {
                    vehiclesLicenses.Add(kvp.Key, kvp.Value);
                }
            }

            return vehiclesLicenses;
        }


        //public VehicleInGarage GetVehicle(string i_License)
        //{
        //    return r_Vehicles[i_License];
        //}

        //public VehicleInGarage GetVehicleByStatus(eVehicleStatus i_Status)
        //{
        //    return null;
        //}

        //public void UpdateVehicle(string i_License, Dictionary<string, object> i_PropertiesToUpdate)
        //{

        //}

        public Dictionary<string, VehicleFactory.eType> GetVehicleTypes()
        {
            return VehicleFactory.GetAvailableTypes();
        }

        public enum eVehicleStatus
        {
            InRepair,
            Fixed,
            Payed
        }
    }
}
