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

        public bool IsVehicleExistsInGarage(string i_License)
        {
            return r_Vehicles.ContainsKey(i_License);
        }

        public void UpdateVehicleInGarageStatus(string i_License, eVehicleStatus i_vehicleStatus)
        {
            VehicleInGarage vehicleInGarage;
            if(IsVehicleExistsInGarage(i_License))
            {
                vehicleInGarage = getVehicleInGarage(i_License);
                vehicleInGarage.Status = i_vehicleStatus;
            }
        }


        public Dictionary<string, KeyValuePair<string, Type>> GetVehicleFieldsToUpdate(string i_License, eFieldGroup groupType)
        {
            Dictionary<string, KeyValuePair<string, Type>> vehicleFields = new Dictionary<string, KeyValuePair<string, Type>>();
            Vehicle vehicle = r_Vehicles[i_License].Vehicle;

            switch(groupType)
            {
                case eFieldGroup.Additional:
                    collectFieldsToUpdate(vehicle.GetFieldsToUpdate(), vehicleFields);
                    break;

                case eFieldGroup.Wheel:
                    collectFieldsToUpdate(vehicle.GetFieldsToUpdateOfWheels(), vehicleFields);
                    break;

                case eFieldGroup.Tank:
                    collectFieldsToUpdate(vehicle.GetFieldsToUpdateOfTank(), vehicleFields);
                    break;
            }

            return vehicleFields;
        }

        private void collectFieldsToUpdate(
            Dictionary<string, PropertyInfo> i_FieldsToCollect,
            Dictionary<string, KeyValuePair<string, Type>> o_FieldsToUpdate)
        {
            foreach (KeyValuePair<string, PropertyInfo> kvp in i_FieldsToCollect)
            {
                o_FieldsToUpdate.Add(kvp.Key, new KeyValuePair<string, Type>(kvp.Value.Name, kvp.Value.PropertyType));
            }
        }

        public void UpdateVehicleFields(string i_License, Dictionary<string, object> i_FieldsWithValues, eFieldGroup groupType)
        {
            Vehicle vehicle = r_Vehicles[i_License].Vehicle;
            object objectToUpdate = vehicle;

            switch (groupType)
            {
                case eFieldGroup.Additional:
                    updateFieldsOfObject(vehicle, i_FieldsWithValues);
                    break;

                case eFieldGroup.Wheel:
                    foreach(Wheel wheel in vehicle.Wheels)
                    {
                        updateFieldsOfObject(wheel, i_FieldsWithValues);
                    }
                    break;

                case eFieldGroup.Tank:
                    updateFieldsOfObject(vehicle.VehicleTank, i_FieldsWithValues);
                    break;
            }
        }

        private void updateFieldsOfObject(object objectToUpdate, Dictionary<string, object> i_FieldsWithValues)
        {
            foreach (KeyValuePair<string, object> kvp in i_FieldsWithValues)
            {
                PropertyInfo property = objectToUpdate.GetType().GetProperty(kvp.Key);

                if (property != null)
                {
                    property.SetValue(objectToUpdate, kvp.Value, null);
                }
            }
        }
        public Dictionary<string, object> GetVehicleFieldsAndValues(string i_License)
        {
            Vehicle vehicle = getVehicleInGarage(i_License).Vehicle;
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

        public void UpdateStatusOfVehicle(string i_License, eVehicleStatus i_StatusToUpdate)
        {
            VehicleInGarage vehicle = getVehicleInGarage(i_License);
            vehicle.Status = i_StatusToUpdate;
        }

        private VehicleInGarage getVehicleInGarage(string i_License)
        {
            if(!IsVehicleExistsInGarage(i_License))
            {
                throw new ArgumentException(string.Format("Vehicle with license {0} doesn't exists.", i_License));
            }

            return r_Vehicles[i_License];
        }

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

        public enum eFieldGroup
        {
            Additional,
            Wheel,
            Tank
        }
    }
}
