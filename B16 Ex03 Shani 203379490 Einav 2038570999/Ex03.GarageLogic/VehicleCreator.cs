using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleCreator
    {
        public static Car CreateNewCar(string i_ModelName, string i_LicenseID, List<Vehicle.Wheel> i_ListOfWheel, Vehicle.eTypeOfEnergy i_EnergyType, float i_RemainingPower, Car.eCarColor i_Color, int i_numOfDoors)
        {
            return new Car(i_ModelName, i_LicenseID, i_ListOfWheel, i_EnergyType, i_RemainingPower, i_Color, i_numOfDoors);
        }
        public static Motorbike CreateNewMotorbike(string i_ModelName, string i_LicenseID, float i_RemainingPower, List<Vehicle.Wheel> i_ListOfWheel, Vehicle.eTypeOfEnergy i_EnergyType,
            Motorbike.eTypeOfLicense i_TypeOfLicense, int i_VolumeOFEngineCC)
        {
            return new Motorbike(i_ModelName, i_LicenseID, i_RemainingPower, i_ListOfWheel, i_EnergyType, i_TypeOfLicense, i_VolumeOFEngineCC);
        }
        public static Truck CreateNewTruck(string i_ModelName, string i_LicenseID, List<Vehicle.Wheel> i_ListOfWheel,
            float i_RemainingPower, bool i_IsCarryingDangerousMaterials, float i_MaxLegitCarryingWeight)
        {

            return new Truck(i_ModelName, i_LicenseID, i_ListOfWheel, i_RemainingPower, i_IsCarryingDangerousMaterials, i_MaxLegitCarryingWeight);
        }
    }
}
