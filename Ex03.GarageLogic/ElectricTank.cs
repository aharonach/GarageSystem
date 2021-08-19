using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ex03.GarageLogic
{
    public class ElectricTank : Tank
    {
        private readonly float r_MaxBatteryTime;
        private float m_BatteryTime;

        public ElectricTank(float i_MaxBatteryTime, float i_BatteryTime)
        {
            r_MaxBatteryTime = i_MaxBatteryTime;
            m_BatteryTime = i_BatteryTime;
        }

        public override eType Type
        {
            get
            {
                return eType.Electric;
            }
        }

        public override float EnergyPercent
        {
            get
            {
                return m_BatteryTime / r_MaxBatteryTime * 100;
            }
        }

        public float BatteryTime
        {
            get
            {
                return m_BatteryTime;
            }
            set
            {
                Recharge(value);
            }
        }

        public void Recharge(float i_BatteryTimeToAdd)
        {
            float maxAmountPossible = r_MaxBatteryTime - m_BatteryTime;

            if (i_BatteryTimeToAdd < 0 || i_BatteryTimeToAdd > r_MaxBatteryTime)
            {
                throw new ValueOutOfRangeException("Invalid battery time amount.", 0, maxAmountPossible);
            }

            float tempBatteryTimeAmount = m_BatteryTime + i_BatteryTimeToAdd;

            if (tempBatteryTimeAmount > r_MaxBatteryTime)
            {
                throw new ValueOutOfRangeException("Max amount reached.", 0, maxAmountPossible);
            }

            m_BatteryTime = tempBatteryTimeAmount;
        }

        public override Dictionary<string, object> GetFieldsValues()
        {
            Dictionary<string, object> fields = base.GetFieldsValues();
            fields.Add("Max battery time", r_MaxBatteryTime);
            fields.Add("Current battery time", BatteryTime);
            return fields;
        }

        public override Dictionary<string, PropertyInfo> GetFieldsToUpdate()
        {
            return new Dictionary<string, PropertyInfo>
                   {
                       { $"Battery time (Max is {r_MaxBatteryTime})", GetType().GetProperty("BatteryTime") }
                   };
        }
    }
}
