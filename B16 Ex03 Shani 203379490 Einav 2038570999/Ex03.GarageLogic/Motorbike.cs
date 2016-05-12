using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorbike : Vehicle
    {
        public enum eTypeOfLicense
        {
            A,
            A1,
            AB,
            B1,
        }

        private eTypeOfLicense m_TypeOfLicense;
        private int m_VolumeOFEngineCC;

        public Motorbike(string i_ModelName, string i_LicenseID, float i_RemainingPower, List<Wheel> i_ListOfWheel, eTypeOfEnergy i_EnergyType,
            eTypeOfLicense i_TypeOfLicense, int i_VolumeOFEngineCC)
          : base(i_ModelName, i_LicenseID, i_RemainingPower, i_ListOfWheel, i_EnergyType)
        {
            m_TypeOfLicense = i_TypeOfLicense;
            m_VolumeOFEngineCC = i_VolumeOFEngineCC;
            SetMaxPreasureInAllWheels(31f);
            if (Power is Gas)
            {
                RemainingPowerInPercent = (Power as Gas).CalculatePercentOfEnergy();
                (Power as Gas).MaxAmountOfGasInLitter = 7.2f;
                (Power as Gas).TypeOfGas = Gas.eTypeOfGas.Octan95;
                (Power as Gas).CurrentAmountOfGasInLitter = i_RemainingPower;
            }
            else
            {
                RemainingPowerInPercent = (Power as Electric).CalculatePercentOfEnergy();
                (Power as Electric).MaxEngergyCapacity = 1.9f;
                (Power as Electric).RemainingEngergyInHours = i_RemainingPower;
            }
        }

        public eTypeOfLicense TypeOfLicense
        {
            get { return m_TypeOfLicense; }
            set { m_TypeOfLicense = value; }
        }

        public override float GetMaxAirPressure()
        {
            return 31f;
        }
        public int VolumeOfEngineCC
        {
            get { return m_VolumeOFEngineCC; }
            set { m_VolumeOFEngineCC = value; }
        }
    }
}