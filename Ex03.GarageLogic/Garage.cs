using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInGarage> r_Vehicles = new Dictionary<string, VehicleInGarage>();

        public void AddVehicle(
            string i_PersonName,
            string i_Phone,
            VehicleCreator.eType i_VehicleType,
            string i_License,
            string i_ModelName)
        {
            Vehicle vehicle = VehicleCreator.Create(i_VehicleType, i_License, i_ModelName);
            VehicleInGarage vehicleInGarage = new VehicleInGarage(i_PersonName, i_Phone, vehicle);
            r_Vehicles.Add(i_License, vehicleInGarage);
        }

        public bool IsVehicleExistsInGarage(string i_License)
        {
            return r_Vehicles.ContainsKey(i_License);
        }

        public void UpdateVehicleInGarageStatus(string i_License, eVehicleStatus i_VehicleStatus)
        {
            if (IsVehicleExistsInGarage(i_License))
            {
                VehicleInGarage vehicleInGarage = getVehicleInGarage(i_License);
                vehicleInGarage.Status = i_VehicleStatus;
            }
        }

        public void InflateAirInVehicleWheels(string i_License)
        {
            VehicleInGarage vehicleInGarage = getVehicleInGarage(i_License);
            Vehicle vehicle = vehicleInGarage.Vehicle;

            foreach (Wheel wheel in vehicle.Wheels)
            {
                float amountToFill = wheel.MaxAirPressure - wheel.AirPressure;
                wheel.Inflate(amountToFill);
            }
            
        }

        public Dictionary<string, KeyValuePair<string, Type>> GetVehicleFieldsToUpdate(string i_License, eFieldGroup i_GroupType)
        {
            Dictionary<string, KeyValuePair<string, Type>> vehicleFields = new Dictionary<string, KeyValuePair<string, Type>>();
            Vehicle vehicle = r_Vehicles[i_License].Vehicle;

            switch(i_GroupType)
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
                    updatePropertiesOfVehicle(vehicle, i_FieldsWithValues);
                    break;

                case eFieldGroup.Wheel:
                    foreach(Wheel wheel in vehicle.Wheels)
                    {
                        updatePropertiesOfVehicle(wheel, i_FieldsWithValues);
                    }
                    break;

                case eFieldGroup.Tank:
                    updatePropertiesOfVehicle(vehicle.Tank, i_FieldsWithValues);
                    break;
            }
        }

        private void updatePropertiesOfVehicle(object objectToUpdate, Dictionary<string, object> i_FieldsWithValues)
        {
            foreach (KeyValuePair<string, object> fieldWithValue in i_FieldsWithValues)
            {
                PropertyInfo property = objectToUpdate.GetType().GetProperty(fieldWithValue.Key);

                if (property != null)
                {
                    try
                    {
                        property.SetValue(objectToUpdate, fieldWithValue.Value, null);
                    }
                    catch (TargetInvocationException targetInvocationException)
                    {
                        if(targetInvocationException.InnerException != null)
                        {
                            throw targetInvocationException.InnerException;
                        }
                    }
                }
            }
        }

        public Dictionary<string, object> GetVehicleFieldsAndValues(string i_License)
        {
            VehicleInGarage vehicleInGarage = getVehicleInGarage(i_License);
            Dictionary<string, object> fieldsWithValues = 
                new Dictionary<string, object>
                    {
                        { "Person name", vehicleInGarage.PersonName }, 
                        { "Status", vehicleInGarage.Status },
                    };

            foreach(KeyValuePair<string, object> fieldWithValue in vehicleInGarage.Vehicle.GetFieldsWithValues())
            {
                fieldsWithValues.Add(fieldWithValue.Key, fieldWithValue.Value);
            }

            return fieldsWithValues;
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

        private VehicleInGarage getVehicleInGarage(string i_License)
        {
            if(!IsVehicleExistsInGarage(i_License))
            {
                throw new ArgumentException($"Vehicle with license {i_License} doesn't exists.");
            }

            return r_Vehicles[i_License];
        }

        public void RefuelVehicle(string i_License, FuelTank.eFuelType i_FuelType, float i_FuelAmountToAdd)
        {
            Vehicle vehicle = getVehicleInGarage(i_License).Vehicle;

            if(!(vehicle.Tank is FuelTank tank))
            {
                throw new ArgumentException("This vehicle doesn't have fuel tank.");
            }

            tank.Refuel(i_FuelType, i_FuelAmountToAdd);
        }

        public void RechargeVehicle(string i_License, float i_BatteryTimeToAdd)
        {
            Vehicle vehicle = getVehicleInGarage(i_License).Vehicle;

            if(!(vehicle.Tank is ElectricTank tank))
            {
                throw new ArgumentException("This vehicle doesn't have fuel tank.");
            }

            tank.Recharge(i_BatteryTimeToAdd);
        }

        public Dictionary<string, VehicleCreator.eType> GetVehicleTypes()
        {
            return VehicleCreator.GetAvailableTypes();
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
