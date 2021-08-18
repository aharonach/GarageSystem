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
            Dictionary<string, PropertyInfo> fields = new Dictionary<string, PropertyInfo>();

            for (int i = 0; i < r_NumOfWheels; i++)
            {
                fields.Add($"Wheel {i + 1} Manufacturer", Wheels[i].GetType().GetProperty("Manufacturer"));
                fields.Add($"Wheel {i + 1} Current Air Pressure", Wheels[i].GetType().GetProperty("AirPressure"));
            }

            return fields;
        }

        public Dictionary<string, PropertyInfo> GetFieldsToUpdateOfTank()
        {
            return VehicleTank.GetFieldsToUpdate();
        }

        public Dictionary<string, object> GetFieldsWithValues()
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();

            // Add basic properties values
            fields.Add("License number", License);
            fields.Add("Model name", ModelName);

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
            Type obj = GetType();
            obj.GetProperty(i_PropertyValuePair.Key).SetValue(obj, i_PropertyValuePair.Value, null);
        }

        public abstract class Tank
        {

            public abstract eType Type
            {
                get;
            }

            public abstract float EnergyPercent
            {
                get;
            }

            public virtual Dictionary<string, object> GetFieldsValues()
            {
                Dictionary<string, object> fields = 
                    new Dictionary<string, object>()
                    {
                        {"Tank type", Type},
                        {"Energy (%)", EnergyPercent},
                    };

                return fields;
            }

            public abstract Dictionary<string, PropertyInfo> GetFieldsToUpdate();

            public enum eType
            {
                Electric,
                Fuel
            }
        }

        public class Wheel
        {
            private readonly float r_MaxAirPressure;
            private string m_Manufacturer;
            private float m_AirPressure;

            public Wheel(string i_Manufacturer, float i_MaxAirPressure, float i_AirPressure)
            {
                r_MaxAirPressure = i_MaxAirPressure;
                m_Manufacturer = i_Manufacturer;
                m_AirPressure = i_AirPressure;
            }

            public string Manufacturer
            {
                get;
                protected set;
            }

            public float MaxAirPressure
            {
                get { return r_MaxAirPressure; }
            }

            public float AirPressure
            {
                get
                {
                    return m_AirPressure;
                }
                set
                {
                    Inflate(value);
                }
            }

            public void Inflate(float i_AirToAdd)
            {
                if (i_AirToAdd < 0)
                {
                    throw new Exception("Invalid air amount.");
                }

                float tempAirPressure = m_AirPressure + i_AirToAdd;

                if (tempAirPressure > MaxAirPressure)
                {
                    throw new Exception("Air pressure given is higher.");
                }

                m_AirPressure = tempAirPressure;
            }
        }
    }
}