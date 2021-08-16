using System;

namespace Ex03.GarageLogic
{
    public class Electric : Vehicle.Tank
    {
        private readonly float r_MaxBatteryTime;
        private float m_BatteryTime;

        public Electric(float i_MaxBatteryTime, float i_BatteryTime)
        {
            r_MaxBatteryTime = i_MaxBatteryTime;
            m_BatteryTime = i_BatteryTime;
        }

        public override eType Type
        {
            get { return eType.Electric; }
        }

        public override float EnergyPercent
        {
            get { return m_BatteryTime / r_MaxBatteryTime * 100; }
        }

        public void Recharge(float i_BatteryTimeToAdd)
        {
            if (i_BatteryTimeToAdd < 0)
            {
                throw new Exception("Invalid battery time amount.");
            }

            m_BatteryTime = Math.Min(m_BatteryTime + i_BatteryTimeToAdd, r_MaxBatteryTime);
        }
    }
}
