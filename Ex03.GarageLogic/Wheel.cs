using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
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
            get { return m_Manufacturer; }
            protected set { m_Manufacturer = value; }
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
