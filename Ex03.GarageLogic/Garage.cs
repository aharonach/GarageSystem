﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInGarage> r_Vehicles = new Dictionary<string, VehicleInGarage>();

        public bool IsVehicleExistsInGarage(string i_License)
        {
            return r_Vehicles.ContainsKey(i_License);
        }

        public void AddVehicle(
            string i_PersonName,
            string i_Phone,
            int i_VehicleType,
            string i_License,
            string i_ModelName)
        {
            Vehicle vehicle = VehicleCreator.Create((VehicleCreator.eType) i_VehicleType, i_License, i_ModelName);
            VehicleInGarage vehicleInGarage = new VehicleInGarage(i_PersonName, i_Phone, vehicle);
            r_Vehicles.Add(i_License, vehicleInGarage);
        }

        public Dictionary<string, eVehicleStatus> GetVehiclesLicenses()
        {
            Dictionary<string, eVehicleStatus> vehiclesLicenses = new Dictionary<string, eVehicleStatus>();

            foreach (VehicleInGarage vehicleInGarage in r_Vehicles.Values)
            {
                vehiclesLicenses.Add(vehicleInGarage.Vehicle.License, vehicleInGarage.Status);
            }

            return vehiclesLicenses;
        }

        public Dictionary<string, eVehicleStatus> GetVehiclesLicenses(eVehicleStatus i_Status)
        {
            Dictionary<string, eVehicleStatus> vehiclesLicenses = new Dictionary<string, eVehicleStatus>();

            foreach (KeyValuePair<string, eVehicleStatus> licenseStatusPair in GetVehiclesLicenses())
            {
                if (licenseStatusPair.Value == i_Status)
                {
                    vehiclesLicenses.Add(licenseStatusPair.Key, licenseStatusPair.Value);
                }
            }

            return vehiclesLicenses;
        }

        public void UpdateVehicleStatusInGarage(string i_License, eVehicleStatus i_VehicleStatus)
        {
            VehicleInGarage vehicleInGarage = getVehicleInGarage(i_License);
            vehicleInGarage.Status = i_VehicleStatus;
        }

        public void InflateAirInVehicleWheels(string i_License)
        {
            VehicleInGarage vehicleInGarage = getVehicleInGarage(i_License);
            Vehicle vehicle = vehicleInGarage.Vehicle;

            foreach (Wheel wheel in vehicle.Wheels)
            {
                wheel.Inflate(wheel.MaxAirPressureToInflate);
            }
        }

        public void RefuelVehicle(string i_License, FuelTank.eFuelType i_FuelType, float i_FuelAmountToAdd)
        {
            Vehicle vehicle = getVehicleInGarage(i_License).Vehicle;

            if (!(vehicle.Tank is FuelTank tank))
            {
                throw new ArgumentException("This vehicle doesn't have fuel tank.");
            }

            tank.Refuel(i_FuelType, i_FuelAmountToAdd);
        }

        public void RechargeVehicle(string i_License, float i_BatteryTimeToAdd)
        {
            Vehicle vehicle = getVehicleInGarage(i_License).Vehicle;

            if (!(vehicle.Tank is ElectricTank tank))
            {
                throw new ArgumentException("This vehicle is not electric.");
            }

            tank.Recharge(i_BatteryTimeToAdd);
        }

        public Dictionary<string, object> GetVehicleFieldsAndValues(string i_License)
        {
            VehicleInGarage vehicleInGarage = getVehicleInGarage(i_License);

            // Init with information from the garage only
            Dictionary<string, object> fieldsWithValues =
                new Dictionary<string, object>
                {
                    { "Person name", vehicleInGarage.PersonName },
                    { "Status", vehicleInGarage.Status },
                };

            Vehicle vehicle = vehicleInGarage.Vehicle;

            // Add all the other fields
            foreach (KeyValuePair<string, object> fieldWithValue in vehicle.GetFieldsWithValues())
            {
                fieldsWithValues.Add(fieldWithValue.Key, fieldWithValue.Value);
            }

            return fieldsWithValues;
        }

        public Dictionary<string, KeyValuePair<string, Type>> GetVehicleFieldsToUpdate(string i_License, eFieldGroup i_GroupType)
        {
            Dictionary<string, KeyValuePair<string, Type>> vehicleFields =
                new Dictionary<string, KeyValuePair<string, Type>>();
            Vehicle vehicle = getVehicleInGarage(i_License).Vehicle;

            switch(i_GroupType)
            {
                case eFieldGroup.Additional:
                    collectFieldsToUpdate(vehicle.GetFieldsToUpdate(), vehicleFields);
                    break;

                case eFieldGroup.Tank:
                    collectFieldsToUpdate(vehicle.GetFieldsToUpdateOfTank(), vehicleFields);
                    break;
            }

            return vehicleFields;
        }

        public List<Dictionary<string, KeyValuePair<string, Type>>> GetWheelsFieldsToUpdate(string i_License)
        {
            Vehicle vehicle = getVehicleInGarage(i_License).Vehicle;
            List<Dictionary<string, PropertyInfo>> fieldsOfWheels = vehicle.GetFieldsToUpdateOfWheels();

            // Init new list with hash maps: keys as field label, values as pairs of property name and type.
            List<Dictionary<string, KeyValuePair<string, Type>>> returnWheelsFields =
                new List<Dictionary<string, KeyValuePair<string, Type>>>();

            for(int i = 0; i < fieldsOfWheels.Count; i++)
            {
                returnWheelsFields.Add(new Dictionary<string, KeyValuePair<string, Type>>());
                collectFieldsToUpdate(fieldsOfWheels[i], returnWheelsFields[i]);
            }

            return returnWheelsFields;
        }

        public void UpdateVehicleFields(string i_License, Dictionary<string, object> i_FieldsWithValues, eFieldGroup groupType)
        {
            Vehicle vehicle = r_Vehicles[i_License].Vehicle;

            switch (groupType)
            {
                case eFieldGroup.Additional:
                    updateDynamicPropertiesOfObject(vehicle, i_FieldsWithValues);
                    break;

                case eFieldGroup.Wheel:

                    foreach(Wheel wheel in vehicle.Wheels)
                    {
                        updateDynamicPropertiesOfObject(wheel, i_FieldsWithValues);
                    }

                    break;

                case eFieldGroup.Tank:
                    updateDynamicPropertiesOfObject(vehicle.Tank, i_FieldsWithValues);
                    break;
            }
        }
        
        public void UpdateWheelFieldsById(string i_License, int i_WheelId, Dictionary<string, object> i_FieldValuePairs)
        {
            Vehicle vehicle = getVehicleInGarage(i_License).Vehicle;

            if(!Validations.IsInRange(i_WheelId, 0, vehicle.Wheels.Count - 1))
            {
                throw new ValueOutOfRangeException("Wheel's number provided is out of range.", 0, vehicle.Wheels.Count - 1);
            }

            updateDynamicPropertiesOfObject(vehicle.Wheels[i_WheelId], i_FieldValuePairs);
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

        private void updateDynamicPropertiesOfObject(object objectToUpdate, Dictionary<string, object> i_FieldsWithValues)
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

        private VehicleInGarage getVehicleInGarage(string i_License)
        {
            if (!IsVehicleExistsInGarage(i_License))
            {
                throw new ArgumentException($"Vehicle with license {i_License} doesn't exists.");
            }

            return r_Vehicles[i_License];
        }

        public Dictionary<int, string> GetVehicleTypes()
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
