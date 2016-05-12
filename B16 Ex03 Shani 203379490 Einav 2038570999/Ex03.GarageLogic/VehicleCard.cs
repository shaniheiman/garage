using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleCard
    {
        public enum eVehicleState
        {
            InRepair,
            Repaired,
            Paid,
            Unknown,
        }

        public enum eTypeOfVehicle
        {
            Motorbike,
            Car,
            Truck,
            Unknown,
        }

        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleState m_VehicleState;
        private Vehicle m_Veichle;

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
            set { m_OwnerPhoneNumber = value; }
        }

        public eVehicleState VehicleState
        {
            get { return m_VehicleState; }
            set { m_VehicleState = value; }
        }

        public Vehicle Veichle
        {
            get { return m_Veichle; }
            set { m_Veichle = value; }
        }

        public VehicleCard(string i_OwnerName, string i_OwnerPhoneNumber, VehicleCard.eVehicleState i_VehicleState, Vehicle i_Veichle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleState = i_VehicleState;
            m_Veichle = i_Veichle;
        }

        
        public static int GetNumOfWheels(eTypeOfVehicle i_VechileType)
        {
            int numOfWheels;
            if (i_VechileType == eTypeOfVehicle.Car)
            {
                numOfWheels = 4;
            }
            else if(i_VechileType == eTypeOfVehicle.Motorbike)
            {
                numOfWheels = 2;
            }
            else
            {
                numOfWheels = 16;
            }
            return numOfWheels;
        }

    }
}