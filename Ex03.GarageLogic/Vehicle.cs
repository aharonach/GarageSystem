using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        private readonly string r_License;
        private readonly List<Wheel> m_Wheels;
        private string m_Name;
        private float m_EnergyPercent;

        public class Wheel
        {
            private readonly float r_MaxAirPressure;
            private string m_Manufacturer;
            private float m_AirPressure;

            public void Inflate(float i_AirToAdd)
            {
                if (i_AirToAdd < 0)
                {
                    throw new Exception("Invalid air amount.");
                }

                m_AirPressure = Math.Min(m_AirPressure + i_AirToAdd, r_MaxAirPressure);
            }
        }
    }
}