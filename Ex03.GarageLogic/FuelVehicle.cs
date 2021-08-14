using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class FuelVehicle : Vehicle
    {
        private readonly float r_MaxFuelAmount;
        private float m_FuelAmount;
        private eFuelType m_FuelType;

        public void Refuel(float i_FuelToAdd, eFuelType i_FuelType)
        {
            if (i_FuelToAdd < 0)
            {
                throw new Exception("Invalid fuel amount.");
            }

            if (i_FuelType != m_FuelType)
            {
                throw new Exception("Fuel type is not valid.");
            }

            m_FuelAmount = Math.Min(m_FuelAmount + i_FuelToAdd, r_MaxFuelAmount);
        }

        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }
    }
}
