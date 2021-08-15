using System;

namespace Ex03.GarageLogic
{
    public class Fuel : Vehicle.Tank
    {
        private readonly eFuelType m_FuelType;
        private readonly float r_MaxFuelAmount;
        private float m_FuelAmount;

        public Fuel(eFuelType i_FuelType, float i_MaxFuelAmount, float i_FuelAmount)
        {
            m_FuelType = i_FuelType;
            r_MaxFuelAmount = i_MaxFuelAmount;
            m_FuelAmount = i_FuelAmount;
        }

        public override eType Type
        {
            get { return eType.Fuel; }
        }

        public override float EnergyPercent
        {
            get { return m_FuelAmount / r_MaxFuelAmount; }
        }

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
