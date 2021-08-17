using System;

namespace Ex03.GarageLogic
{
    public class Fuel : Vehicle.Tank
    {
        private readonly eFuelType r_FuelType;
        private readonly float r_MaxFuelAmount;
        private float m_FuelAmount;

        public Fuel(eFuelType i_FuelType, float i_MaxFuelAmount, float i_FuelAmount)
        {
            r_FuelType = i_FuelType;
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

        public float FuelAmount
        {
            get { return m_FuelAmount; }
            set { Refuel(value, r_FuelType); }
        }

        public void Refuel(float i_FuelToAdd, eFuelType i_FuelType)
        {
            if (i_FuelToAdd < 0)
            {
                throw new Exception("Invalid fuel amount.");
            }

            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException("Fuel type is not valid.");
            }

            float tempFuelAmount = m_FuelAmount + i_FuelToAdd;

            if (tempFuelAmount > r_MaxFuelAmount)
            {
                throw new ValueOutOfRangeException("Max amount reached.", 0, r_MaxFuelAmount - m_FuelAmount);
            }

            m_FuelAmount = tempFuelAmount;
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
