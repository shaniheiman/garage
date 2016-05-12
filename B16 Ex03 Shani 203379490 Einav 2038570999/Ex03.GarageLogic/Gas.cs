using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Gas : Power
    {
        public enum eTypeOfGas//cahnge
        {
            Solar,
            Octan95,
            Octan96,
            Octan98,
            Unknown,
        }
        private eTypeOfGas m_TypeOfGas;
        private float m_CurrentAmountOfGasInLitter;
        private float m_MaxAmountOfGasInLitter;
        public float CurrentAmountOfGasInLitter
        {
            get { return m_CurrentAmountOfGasInLitter; }
            set { m_CurrentAmountOfGasInLitter = value; }
        }

        public Gas(float i_RemainingPower) : base(i_RemainingPower)
        {

        }
        public eTypeOfGas TypeOfGas
        {
            get { return m_TypeOfGas; }
            set { m_TypeOfGas = value; }
        }
        public float MaxAmountOfGasInLitter
        {
            get { return m_MaxAmountOfGasInLitter; }
            set { m_MaxAmountOfGasInLitter = value; }
        }

        public void RefuelVehicle(float i_NumOfLitterToAdd)//change
        {
            if (m_CurrentAmountOfGasInLitter + i_NumOfLitterToAdd <= m_MaxAmountOfGasInLitter)
            {

                m_CurrentAmountOfGasInLitter += i_NumOfLitterToAdd;
            }
            else
            {
                m_CurrentAmountOfGasInLitter = m_MaxAmountOfGasInLitter;
            }
        }

        public float CalculatePercentOfEnergy()
        {
            return (m_CurrentAmountOfGasInLitter / m_MaxAmountOfGasInLitter) * 100;
        }
    }
}
