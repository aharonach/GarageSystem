using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class FuelTank : Tank
    {
        private readonly eFuelType r_FuelType;
        private readonly float r_MaxFuelAmount;
        private float m_FuelAmount;

        public FuelTank(eFuelType i_FuelType, float i_MaxFuelAmount, float i_FuelAmount)
        {
            r_FuelType = i_FuelType;
            r_MaxFuelAmount = i_MaxFuelAmount;
            m_FuelAmount = i_FuelAmount;
        }

        public override eType Type
        {
            get
            {
                return eType.Fuel;
            }
        }

        public override float EnergyPercent
        {
            get
            {
                return m_FuelAmount / r_MaxFuelAmount * 100;
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public float FuelAmount
        {
            get
            {
                return m_FuelAmount;
            }
            set
            {
                Refuel(r_FuelType, value);
            }
        }

        public void Refuel(eFuelType i_FuelType, float i_FuelToAdd)
        {
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException("Fuel type is not valid.");
            }

            float maxAmountPossible = r_MaxFuelAmount - m_FuelAmount;

            if (i_FuelToAdd < 0 || i_FuelToAdd > r_MaxFuelAmount)
            {
                throw new ValueOutOfRangeException("Invalid fuel amount.", 0, maxAmountPossible);
            }

            float tempFuelAmount = m_FuelAmount + i_FuelToAdd;

            if (tempFuelAmount > r_MaxFuelAmount)
            {
                throw new ValueOutOfRangeException("Max amount reached.", 0, maxAmountPossible);
            }

            m_FuelAmount = tempFuelAmount;
        }

        public override Dictionary<string, object> GetFieldsValues()
        {
            Dictionary<string, object> fields = base.GetFieldsValues();
            fields.Add("Fuel type", FuelType);
            fields.Add("Max fuel amount", r_MaxFuelAmount);
            fields.Add("Current fuel amount", FuelAmount);
            return fields;
        }

        public override Dictionary<string, PropertyInfo> GetFieldsToUpdate()
        {
            return new Dictionary<string, PropertyInfo>
                   {
                       { $"Fuel amount (Max is {r_MaxFuelAmount})", GetType().GetProperty("FuelAmount") }
                   };
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
