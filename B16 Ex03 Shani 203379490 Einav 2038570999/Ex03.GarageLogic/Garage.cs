using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleCard> m_GarageManager;

        public Garage()
        {
            m_GarageManager = new Dictionary<string, VehicleCard>();
        }
        public bool IsGarageContainsThisVehicle(string i_VehicleID)
        {
            return m_GarageManager.ContainsKey(i_VehicleID);
        }
        public List<string> GetListOfLicenseNumber()
        {
            List<string> LicenseNumbersList = new List<string>();
            foreach (KeyValuePair<string, VehicleCard> kvp in m_GarageManager)
            {
                LicenseNumbersList.Add(kvp.Key);
            }

            return LicenseNumbersList;
        }

        public List<string> GetListOfLicenseNumberSortedByState(VehicleCard.eVehicleState i_StateOfVehicle)
        {
            List<string> LicenseNumbersList = new List<string>();
            foreach (KeyValuePair<string, VehicleCard> kvp in m_GarageManager)
            {
                VehicleCard carData = kvp.Value;
                if (carData.VehicleState == i_StateOfVehicle)
                {
                    LicenseNumbersList.Add(kvp.Key);
                }
            }

            return LicenseNumbersList;
        }
 
        public VehicleCard GetVehicleCardByLicenseID(string i_LicenseID)
        {
            VehicleCard returnedVehicleCard = null;
            if (m_GarageManager.ContainsKey(i_LicenseID))
            {
                returnedVehicleCard= m_GarageManager[i_LicenseID];
            }
            return returnedVehicleCard;
        }
        public void InsertNewVehicle(string i_VehicleID, string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            if (m_GarageManager.ContainsKey(i_VehicleID.ToString()))
            {
                m_GarageManager[i_VehicleID].VehicleState = VehicleCard.eVehicleState.InRepair;
            }
            else
            {
                m_GarageManager.Add(i_VehicleID, new VehicleCard(i_OwnerName, i_OwnerPhoneNumber, VehicleCard.eVehicleState.InRepair, i_Vehicle));
            }
        }

        public void ChangeStateOfVehicle(string i_VehicleID, VehicleCard.eVehicleState i_NewStateOfVehicle)
        {
            if (m_GarageManager.ContainsKey(i_VehicleID.ToString()))
            {
                m_GarageManager[i_VehicleID].VehicleState = i_NewStateOfVehicle;
            }
            else
            {
                //TODO EXPECTION
            }
        }

       
        public void WheelInflation(string i_VehicleID)
        {
            if (m_GarageManager.ContainsKey(i_VehicleID)) //TODO
            {
                VehicleCard veichleCard = m_GarageManager[i_VehicleID];
                veichleCard.Veichle.WheelInflationToMax();
            }
        }

        public void FillEnergyByAmount(string i_VehicleID, float i_AmountToFill, Gas.eTypeOfGas i_TypeOfGas)//cahnge
        {
            VehicleCard currentVehicle = m_GarageManager[i_VehicleID];
            if (currentVehicle.Veichle.Power is Electric)
            {
                (currentVehicle.Veichle.Power as Electric).ChargingEnergy(i_AmountToFill);
            }
            else
            {
                if (i_TypeOfGas == (currentVehicle.Veichle.Power as Gas).TypeOfGas)
                {
                    (currentVehicle.Veichle.Power as Gas).RefuelVehicle(i_AmountToFill);
                }
                else
                {
                    //TODO EXPECTION can not fuel, incorrect fuel type
                }
            }
        }
    }
}
