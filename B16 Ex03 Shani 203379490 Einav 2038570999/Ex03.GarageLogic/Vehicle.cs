using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eTypeOfEnergy
        {
            Gas,
            Electric,
        }
        private string m_ModelName;
        private string m_LicenseNumber;
        private List<Wheel> m_ListOfWheel;
        private Power m_Power;

        public Vehicle(string i_ModelName, string i_LicenseID, float i_RemainingPower, List<Wheel> i_ListOfWheel, eTypeOfEnergy i_EnergyType)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseID;
            m_ListOfWheel = i_ListOfWheel;

            if (i_EnergyType == eTypeOfEnergy.Gas)
            {
                m_Power = new Gas(i_RemainingPower);
            }
            else
            {
                m_Power = new Electric(i_RemainingPower);
            }
        }
        public abstract float GetMaxAirPressure();

        public struct Wheel
        {
            private string m_ManufacturerName;
            private float m_CurrentAirPreasure;
            private float m_MaxAirPreasure;

            public string ManufacturerName
            {
                get { return m_ManufacturerName; }
                set { m_ManufacturerName = value; }
            }
            public float CurrentAirPreasure
            {
                get { return m_CurrentAirPreasure; }
                set { m_CurrentAirPreasure = value; }
            }
            public float MaxAirPreasure
            {
                get { return m_MaxAirPreasure; }
                set { m_MaxAirPreasure = value; }
            }

            public Wheel(string i_ManufacturerName, float i_CurrentAirPreasure, float i_MaxAirPreasure)
            {
                m_ManufacturerName = i_ManufacturerName;
                m_CurrentAirPreasure = i_CurrentAirPreasure;
                m_MaxAirPreasure = i_MaxAirPreasure;
            }
            public void SetMaxPreasure(float i_maxPreasue)
            {
                m_MaxAirPreasure = i_maxPreasue;
            }
            public void WheelInflationToMax()
            {
                m_CurrentAirPreasure = m_MaxAirPreasure;
            }
        }

        public object Power
        {
            get
            {
                if (m_Power is Gas)
                {
                    return m_Power as Gas;
                }
                else
                {
                    return m_Power as Electric;
                }
            }
        }
        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }
        public void SetMaxPreasureInAllWheels(float i_maxPreasue)
        {
            foreach (Wheel wheel in m_ListOfWheel)
            {
                wheel.SetMaxPreasure(i_maxPreasue);
            }
        }
        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public float RemainingPowerInPercent
        {
            get { return m_Power.Remaining; }
            set { m_Power.Remaining= value; }
        }

        public void WheelInflationToMax()
        {
            foreach (Wheel w in m_ListOfWheel)
            {
                w.WheelInflationToMax();
            }
        }
    }
}
