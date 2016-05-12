using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Power
    {
        private float m_RemainingPower;
        public Power(float i_RemainingPower)
        {
            m_RemainingPower = i_RemainingPower;
        }
        public float Remaining
        {
            get { return m_RemainingPower; }
            set { m_RemainingPower = value; }

        }
    }
}
