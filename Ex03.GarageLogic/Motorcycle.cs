using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private readonly int r_EngineCapacity;
        private eLicenseType m_LicenseType;

        public enum eLicenseType
        {
            A,
            B1,
            AA,
            BB
        }
    }
}
