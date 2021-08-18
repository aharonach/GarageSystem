using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly int r_NumOfWheels;
        private readonly string r_License;
        private readonly List<Wheel> r_Wheels;
        private readonly string r_ModelName;
        private Tank m_Tank;

        protected Vehicle(string i_License, string i_ModelName, int i_NumOfWheels, float i_MaxWheelAirPressure)
        {
            r_License = i_License;
            r_ModelName = i_ModelName;
            r_NumOfWheels = i_NumOfWheels;
            r_Wheels = new List<Wheel>(r_NumOfWheels);
            initWheels(i_MaxWheelAirPressure);
        }

        public string License
        {
            get { return r_License; }
        }

        public string ModelName
        {
            get { return r_ModelName; }
        }

        public List<Wheel> Wheels
        {
            get { return r_Wheels; }
        }
        
        public Tank VehicleTank
        {
            get { return m_Tank; }
            protected set { m_Tank = value; }
        }

        protected void AddWheel(string i_Manufacturer, float i_MaxAirPressure, float i_AirPressure)
        {
            if(r_Wheels.Count < r_NumOfWheels)
            {
                r_Wheels.Add(new Wheel(i_Manufacturer, i_MaxAirPressure, i_AirPressure));
            }
        }

        private void initWheels(float i_MaxAirPressure)
        {
            for (int i = 0; i < r_NumOfWheels; i++)
            {
                AddWheel(string.Empty, i_MaxAirPressure, 0);
            }
        }

        public override int GetHashCode()
        {
            return License.GetHashCode();
        }

        public abstract Dictionary<string, PropertyInfo> GetFieldsToUpdate();

        public Dictionary<string, PropertyInfo> GetFieldsToUpdateOfWheels()
        {
            Dictionary<string, PropertyInfo> fields = new Dictionary<string, PropertyInfo>
            {
                { $"Wheels Manufacturer", Wheels[0].GetType().GetProperty("Manufacturer") },
                { $"Wheels Current Air Pressure (Max is {Wheels[0].MaxAirPressure})", Wheels[0].GetType().GetProperty("AirPressure") }
            };

            return fields;
        }

        public Dictionary<string, PropertyInfo> GetFieldsToUpdateOfTank()
        {
            return VehicleTank.GetFieldsToUpdate();
        }

        public Dictionary<string, object> GetFieldsWithValues()
        {
            Dictionary<string, object> fields = new Dictionary<string, object>
            {
                // Add basic properties values
                { "License number", License },
                { "Model name", ModelName }
            };

            // Add wheels properties values
            for (int i = 0; i < r_NumOfWheels; i++)
            {
                fields.Add($"Wheel {i + 1} Manufacturer", Wheels[i].Manufacturer);
                fields.Add($"Wheel {i + 1} Maximum Air Pressure", Wheels[i].MaxAirPressure);
                fields.Add($"Wheel {i + 1} Current Air Pressure", Wheels[i].AirPressure);
            }

            // Add tank properties values
            foreach (KeyValuePair<string, object> kvp in VehicleTank.GetFieldsValues())
            {
                fields.Add(kvp.Key, kvp.Value);
            }

            // Add custom properties values of child classes
            foreach (KeyValuePair<string, PropertyInfo> kvp in GetFieldsToUpdate())
            {
                fields.Add(kvp.Key, kvp.Value.GetValue(this, null));
            }

            return fields;
        }

        public void UpdatePropertyValue(KeyValuePair<string, object> i_PropertyValuePair)
        {
            PropertyInfo property = GetType().GetProperty(i_PropertyValuePair.Key);
            
            if(property != null)
            {
                property.SetValue(this, i_PropertyValuePair.Value, null);
            }
        }
    }
}