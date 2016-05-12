using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Electric : Power
    {
        private float m_RemainingEngergyInHours;
        private float m_MaxEngergyCapacity;

        public Electric(float i_RemainingPower) : base(i_RemainingPower)
        {

        }
        public float RemainingEngergyInHours
        {
            get
            {
                return m_RemainingEngergyInHours;
            }
            set
            {
                m_RemainingEngergyInHours = value;
            }
        }

        public float MaxEngergyCapacity
        {
            get
            {
                return m_MaxEngergyCapacity;
            }
            set
            {
                m_MaxEngergyCapacity = value;
            }
        }

        public float CalculatePercentOfEnergy()
        {
            return (m_RemainingEngergyInHours / m_MaxEngergyCapacity) * 100;
        }
        public void ChargingEnergy(float i_NumOfHoursToCharge)//change
        {
            if (m_RemainingEngergyInHours + i_NumOfHoursToCharge <= m_MaxEngergyCapacity)
            {

                m_RemainingEngergyInHours += i_NumOfHoursToCharge;
            }
            else
            {
                m_RemainingEngergyInHours = m_MaxEngergyCapacity;
            }

        }

    }
}