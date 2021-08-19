using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private readonly int r_WheelId;
        private string m_Manufacturer;
        private float m_AirPressure;

        public Wheel(int i_WheelId, string i_Manufacturer, float i_MaxAirPressure, float i_AirPressure)
        {
            r_WheelId = i_WheelId;
            r_MaxAirPressure = i_MaxAirPressure;
            m_Manufacturer = i_Manufacturer;
            m_AirPressure = i_AirPressure;
        }

        public int Id
        {
            get
            {
                return r_WheelId;
            }
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
            protected set
            {
                m_Manufacturer = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
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

        public float MaxAirPressureToInflate
        {
            get
            {
                return r_MaxAirPressure - m_AirPressure;
            }
        }

        public Dictionary<string, PropertyInfo> GetFieldsToUpdate()
        {
            return new Dictionary<string, PropertyInfo>
               {
                   { $"Wheels {Id} Manufacturer", GetType().GetProperty("Manufacturer") },
                   { $"Wheels {Id} Current Air Pressure (Max is {MaxAirPressure})", GetType().GetProperty("AirPressure") }
               };
        }

        public void Inflate(float i_AirToAdd)
        {
            if (i_AirToAdd < 0 || i_AirToAdd > r_MaxAirPressure)
            {
                throw new ValueOutOfRangeException("Invalid air amount.", 0, MaxAirPressureToInflate);
            }

            float tempAirPressure = m_AirPressure + i_AirToAdd;

            if (tempAirPressure > MaxAirPressure)
            {
                throw new ValueOutOfRangeException("Air pressure reached the maximum amount.", 0, MaxAirPressureToInflate);
            }

            m_AirPressure = tempAirPressure;
        }
    }
}
