using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eCarColor//cahnge
        {
            Yellow,
            White,
            Red,
            Black,
        }
        private int m_numOfWheels = 4;
        private eCarColor m_CarColor;//change
        private int m_numOfDoors;

        public Car(string i_ModelName, string i_LicenseID, List<Wheel> i_ListOfWheel, eTypeOfEnergy i_EnergyType, float i_RemainingPower, eCarColor i_Color, int i_numOfDoors)
 : base(i_ModelName, i_LicenseID, i_RemainingPower, i_ListOfWheel, i_EnergyType)
        {
            m_CarColor = i_Color;
            m_numOfDoors = i_numOfDoors;
            SetMaxPreasureInAllWheels(30f);
            if (Power is Gas)
            {
                RemainingPowerInPercent = (Power as Gas).CalculatePercentOfEnergy();
                (Power as Gas).MaxAmountOfGasInLitter = 38f;
                (Power as Gas).TypeOfGas = Gas.eTypeOfGas.Octan98;
                (Power as Gas).CurrentAmountOfGasInLitter = i_RemainingPower;
            }
            else
            {
                RemainingPowerInPercent = (Power as Electric).CalculatePercentOfEnergy();
                (Power as Electric).MaxEngergyCapacity = 3.3f;
                (Power as Electric).RemainingEngergyInHours = i_RemainingPower;
            }
        }


        public eCarColor CarColor//change
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public int NumberOfWheels
        {
            get { return m_numOfWheels; }
            set { m_numOfWheels = value; }
        }
        public int NumOfDoors
        {
            get { return m_numOfDoors; }
            set { m_numOfDoors = value; }
        }
        public override float GetMaxAirPressure()
        {
            return 30f;
        }
    }
}
