using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_License;
        private readonly List<Wheel> r_Wheels;
        private string m_Name;
        private Tank m_Tank;

        protected Vehicle(string i_License, List<Wheel> i_Wheels, string i_Name, Tank i_Tank)
        {
            r_License = i_License;
            r_Wheels = i_Wheels;
            m_Name = i_Name;
            VehicleTank = i_Tank;
        }

        public string License
        {
            get { return r_License; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
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

        public override int GetHashCode()
        {
            return this.License.GetHashCode();
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
                Manufacturer = i_Manufacturer;
                AirPressure = i_AirPressure;
            }

            public string Manufacturer
            {
                get { return m_Manufacturer; }
                private set { m_Manufacturer = value; }
            }

            public float MaxAirPressure
            {
                get { return r_MaxAirPressure; }
            }

            public float AirPressure
            {
                get { return m_AirPressure; }
                private set { Inflate(value); }
            }

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