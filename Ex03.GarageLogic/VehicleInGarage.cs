using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class VehicleInGarage
    {
        private string m_PersonName;
        private string m_Phone;
        private eVehicleStatus m_Status;
        private Vehicle m_Vehicle;
        private Garage.eVehicleType m_Type;

        public VehicleInGarage(string i_PersonName, string i_Phone, eVehicleStatus i_Status, Vehicle i_Vehicle)
        {

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

        public eVehicleStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            private set { m_Vehicle = value; }
        }

        public Garage.eVehicleType Type
        {
            get { return m_Type; }
        }
        
        public enum eVehicleStatus
        {
            InRepair,
            Fixed,
            Payed
        }
    }
}
