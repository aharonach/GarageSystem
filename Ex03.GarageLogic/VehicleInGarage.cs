using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        private string m_PersonName;
        private string m_Phone;
        private Garage.eVehicleStatus m_Status;
        private Vehicle m_Vehicle;

        public VehicleInGarage(string i_PersonName, string i_Phone, Vehicle i_Vehicle)
        {
            if(!Validations.IsNumeric(i_Phone))
            {
                throw new ArgumentException("Phone number should contain only digits.");
            }

            PersonName = i_PersonName;
            Phone = i_Phone;
            Status = Garage.eVehicleStatus.InRepair;
            Vehicle = i_Vehicle;
        }

        public string PersonName
        {
            get { return m_PersonName; }
            set { m_PersonName = value; }
        }

        public string Phone
        {
            get { return m_Phone; }
            set { m_Phone = value; }
        }

        public Garage.eVehicleStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            private set { m_Vehicle = value; }
        }
    }
}
