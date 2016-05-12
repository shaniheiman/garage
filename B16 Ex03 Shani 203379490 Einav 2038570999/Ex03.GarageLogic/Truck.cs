using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsCarryingDangerousMaterials;
        private float m_MaxLegitCarryingWeight;

        public Truck(string i_ModelName, string i_LicenseID, List<Wheel> i_ListOfWheel, float i_RemainingPower, 
            bool i_IsCarryingDangerousMaterials, float i_MaxLegitCarryingWeight)
 : base(i_ModelName, i_LicenseID, i_RemainingPower, i_ListOfWheel, eTypeOfEnergy.Gas)
        {

            m_IsCarryingDangerousMaterials = i_IsCarryingDangerousMaterials;
            SetMaxPreasureInAllWheels(28f);
            (Power as Gas).CurrentAmountOfGasInLitter = i_RemainingPower;
            (Power as Gas).TypeOfGas = Gas.eTypeOfGas.Solar;
            (Power as Gas).MaxAmountOfGasInLitter = 135f;
            RemainingPowerInPercent = (Power as Gas).CalculatePercentOfEnergy();
        }
        public bool IsCarryingDangerousMaterials
        {
            get { return m_IsCarryingDangerousMaterials; }
            set { m_IsCarryingDangerousMaterials = value; }
        }

        public float MaxLegitCarryingWeight
        {
            get { return m_MaxLegitCarryingWeight; }
            set { m_MaxLegitCarryingWeight = value; }
        }

        public override float GetMaxAirPressure()
        {
            return 28f;
        }
    }
}