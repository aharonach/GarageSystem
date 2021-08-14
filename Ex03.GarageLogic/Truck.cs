using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Truck : FuelVehicle
    {
        private readonly float r_MaxCarryingCapacity;
        private bool m_DrivesDangerousMaterials;
    }
}
