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
            r_License = i_License;
            r_ModelName = i_ModelName;
            r_NumOfWheels = i_NumOfWheels;
            r_Wheels = new List<Wheel>(r_NumOfWheels);
            InitWheels(i_MaxWheelAirPressure);
        }

        public string License
        {
            get { return r_License; }
        }

        public string ModelName
        {
            get { return r_ModelName; }
        }

        protected List<Wheel> Wheels
        {
            get { return r_Wheels; }
        }
        
        protected Tank VehicleTank
        {
            get { return m_Tank; }
            set { m_Tank = value; }
        }

        protected void AddWheel(string i_Manufacturer, float i_MaxAirPressure, float i_AirPressure)
        {
            if(r_Wheels.Count < r_NumOfWheels)
            {
                r_Wheels.Add(new Wheel(i_Manufacturer, i_MaxAirPressure, i_AirPressure));
            }
        }

        private void InitWheels(float i_MaxAirPressure)
        {
            

            for (int i = 0; i < r_NumOfWheels; i++)
            {
                AddWheel(string.Empty, i_MaxAirPressure, 0);
            }
        }

        public override int GetHashCode()
        {
            return this.License.GetHashCode();
        }

        public PropertyInfo[] GetAvialableProperties()
        {
            Type objectType = this.GetType();



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