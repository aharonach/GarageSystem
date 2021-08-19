using System;
using System.Collections.Generic;
using System.Reflection;

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
            if(!Validations.IsNumeric(i_License))
            {
                throw new ArgumentException("License number should only contain digits.");
            }

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
        
        public Tank Tank
        {
            get { return m_Tank; }
            protected set { m_Tank = value; }
        }

        private void addWheel(int i_WheelId, string i_Manufacturer, float i_MaxAirPressure, float i_AirPressure)
        {
            if(r_Wheels.Count < r_NumOfWheels)
            {
                r_Wheels.Add(new Wheel(i_WheelId, i_Manufacturer, i_MaxAirPressure, i_AirPressure));
            }
        }

        private void initWheels(float i_MaxAirPressure)
        {
            for (int i = 0; i < r_NumOfWheels; i++)
            {
                addWheel(i + 1, string.Empty, i_MaxAirPressure, 0);
            }
        }

        public override int GetHashCode()
        {
            return License.GetHashCode();
        }

        public abstract Dictionary<string, PropertyInfo> GetFieldsToUpdate();

        public List<Dictionary<string, PropertyInfo>> GetFieldsToUpdateOfWheels()
        {
            List<Dictionary<string, PropertyInfo>> fields = new List<Dictionary<string, PropertyInfo>>(Wheels.Count);

            for(int i = 0; i < Wheels.Count; i++)
            {
                fields.Add(new Dictionary<string, PropertyInfo>());

                foreach (KeyValuePair<string, PropertyInfo> propertyInfo in Wheels[i].GetFieldsToUpdate())
                {
                    fields[i].Add(propertyInfo.Key, propertyInfo.Value);
                }
            }

            return fields;
        }

        public Dictionary<string, PropertyInfo> GetFieldsToUpdateOfTank()
        {
            return Tank.GetFieldsToUpdate();
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
            foreach (Wheel wheel in Wheels)
            {
                fields.Add($"Wheel {wheel.Id}: Manufacturer", wheel.Manufacturer);
                fields.Add($"Wheel {wheel.Id}: Maximum Air Pressure", wheel.MaxAirPressure);
                fields.Add($"Wheel {wheel.Id}: Current Air Pressure", wheel.AirPressure);
            }

            // Add tank properties values
            foreach (KeyValuePair<string, object> keyValuePair in Tank.GetFieldsValues())
            {
                fields.Add(keyValuePair.Key, keyValuePair.Value);
            }

            // Add custom properties values of child classes
            foreach (KeyValuePair<string, PropertyInfo> keyValuePair in GetFieldsToUpdate())
            {
                fields.Add(keyValuePair.Key, keyValuePair.Value.GetValue(this, null));
            }

            return fields;
        }
    }
}