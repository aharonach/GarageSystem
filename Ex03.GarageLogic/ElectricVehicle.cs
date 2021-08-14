using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class ElectricVehicle : Vehicle
    {
        private readonly float r_MaxBatteryTime;
        private float m_BatteryTime;

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
