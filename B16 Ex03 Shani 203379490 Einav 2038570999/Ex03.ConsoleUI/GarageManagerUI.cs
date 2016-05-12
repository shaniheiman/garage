using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;
namespace Ex03.ConsoleUI
{
    public class GarageManagerUI
    {
        public Ex03.GarageLogic.Garage m_GarageManager;

        public GarageManagerUI()
        {
            m_GarageManager = new Garage();
        }

        public void RunningGarageManager()
        {
            char userSelection = 'p';
            while (userSelection != 'q')
            {
                printMainMenu();
                userSelection = executeUserCommand(readUserSelection());
            }
        }

        private static bool isValidSelectionMainMenu(char i_userSelection)
        {
            return (i_userSelection >= '1' && i_userSelection <= '7') || i_userSelection == 'q';
        }

        private char readUserSelection()
        {
            char userSelection = Console.ReadKey().KeyChar;
            while (!isValidSelectionMainMenu(userSelection))
            {
                Console.WriteLine();
                Console.WriteLine(@"Invalid selection. Please choose from the options above.
For EXIT press q.");
                userSelection = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }
            return userSelection;

        }

        private static void printSubMenu(string i_LicenseID, string i_MsgOptionForLoadEnergy)
        {
            Console.WriteLine(@"The available options for vehicle whose number is: {0} are:

[1] Change State
[2] Inflate air presure to the maximum
[3] {1}
[4] View full details
", i_LicenseID, i_MsgOptionForLoadEnergy);


        }

        private void subMenuForVehicleAlreadyExists(string i_LicenseID)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            VehicleCard currentVehicleCard = m_GarageManager.GetVehicleCardByLicenseID(i_LicenseID);
            string optionForLoadEnergy;

            if (currentVehicleCard.Veichle.Power is Gas)
            {
                optionForLoadEnergy = "Refuel";
            }
            else
            {
                optionForLoadEnergy = "Cherge";
            }

            printSubMenu(i_LicenseID, optionForLoadEnergy);
            char selection = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (selection)
            {
                case '1':
                    {
                        changeStateOfVehicle(i_LicenseID);
                        break;
                    }

                case '2':
                    {
                        m_GarageManager.WheelInflation(i_LicenseID);
                        Console.WriteLine("Set wheels' air pressure to maximum: {0}.", currentVehicleCard.Veichle.GetMaxAirPressure().ToString());
                        Console.ReadLine();
                        break;
                    }

                case '3':
                    {
                        if (currentVehicleCard.Veichle.Power is Gas)
                        {
                            refuelGasVehicleByLicenseID(i_LicenseID);
                        }
                        else
                        {
                            chargingElectricVehicleByLicenseID(i_LicenseID);
                        }
                        break;
                    }

                case '4':
                    {
                        //currently unavailable
                        //TODO
                        break;
                    }
            }
        }

        private char executeUserCommand(char i_UserSelection)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            switch (i_UserSelection)
            {
                case '1':
                    {
                        bool isVehicleExist;
                        string licenseId = insertNewVehicle(out isVehicleExist);
                        if (isVehicleExist)
                        {
                            subMenuForVehicleAlreadyExists(licenseId);
                        }
                        break;
                    }
                case '2':
                    {
                        printListOfVehicle();
                        break;
                    }
                case '3':
                    {
                        ChangeStateOfVehicle();
                        break;
                    }
                case '4':
                    {
                        InflatingWheels();
                        break;
                    }
                case '5':
                    {
                        refuelGasVehicle();
                        break;
                    }
                case '6':
                    {
                        ChargingElectricVehicle();
                        break;
                    }
                case '7':
                    {
                        ViewVehicleFullData();//todo
                        break;
                    }
                default:
                    break;
            }
            return i_UserSelection;//TODO change
        }

        private static void printMainMenu()
        {

            Ex02.ConsoleUtils.Screen.Clear();
            string menuToPrint = string.Format(@"
[1] Insert new vehicle to the garage.
[2] View License Number of current vehicle in the garage.
[3] Change state of vehicle that already in the garage.
[4] Wheel Inflation to maximum capacity.
[5] Refuel Gas-fueled vehicles
[6] Electric Vehicle Charging
[7] View vehicle's data by license number. 
");

            Console.WriteLine(menuToPrint);
        }

        public void printListOfVehicle()
        {
            string menuToPrint = string.Format(@"Please choose:
[1] View all vehicles' license number
[2] View all vehicles' license number sorted by state of repairment
");
            Console.WriteLine(menuToPrint);
            string userInput = Console.ReadLine();
            int userChoice = int.Parse(userInput);//TODO expection
            Ex02.ConsoleUtils.Screen.Clear();
            switch (userChoice)
            {
                case 1:
                    {
                        printListOfLicenseID(m_GarageManager.GetListOfLicenseNumber());
                        printMainMenu();
                        break;
                    }
                case 2:
                    {
                        printSoretedListOfVehicle();
                        printMainMenu();
                        break;
                    }
                    //case default:
                    //    {
                    //        break;
                    //    }
            }
        }

        private void printSoretedListOfVehicle()
        {
            string menuToPrint = string.Format(@"Please choose the criteria you would like to sort by:
[1] InRepair
[2] Repaired
[3] Paid
");
            Console.WriteLine(menuToPrint);
            string userInput = Console.ReadLine();
            int stateOfVehicle = int.Parse(userInput); //todo validaion expection
            printListOfLicenseID(m_GarageManager.GetListOfLicenseNumberSortedByState(getNewStateOfVehicle(stateOfVehicle)));

        }
        private void printListOfLicenseID(List<string> i_ListOfLicenseID)
        {
            if (i_ListOfLicenseID.Capacity == 0)
            {
                Console.WriteLine("There are no records to show.");
            }
            else
            {
                foreach (string record in i_ListOfLicenseID)
                {
                    Console.WriteLine(record);
                }
            }
            Console.WriteLine("press any key to continue.");
            Console.ReadLine();
        }
        private void chargingElectricVehicleByLicenseID(string i_LicenseID)
        {
            Console.WriteLine("Please enter the amount of hours you would like to charge: ");
            string chargeHours = Console.ReadLine();
            float numOfHoursToCharge = float.Parse(chargeHours); //todo expection

            m_GarageManager.FillEnergyByAmount(i_LicenseID, numOfHoursToCharge, Gas.eTypeOfGas.Unknown);

        }

        private void ViewVehicleFullData()//todo
        {
            Console.WriteLine("Please enter your license number: ");
            string licenseNumber = Console.ReadLine();
            VehicleCard vehicle = m_GarageManager.GetVehicleCardByLicenseID(licenseNumber);
            if (vehicle == null)
            {
                //todo expetion
            }
            else
            {//todo instead of write line - buidler? format?
                //Console.WriteLine("License number: {0} \nModel name: {1}\n Owner name: {2}\n Vehicle status: {2}\n Wheel's manufactor: {3}\n Wheel's air pressure: {4}\n Engergy status: {5}\n", 
                //    licenseNumber, vehicle.OwnerName, vehicle.VehicleState, vehicle.Veichle.GetMaxAirPressure(), 
            }
        }

        public void ChargingElectricVehicle()
        {
            Console.WriteLine("Please enter your license number: ");
            string licenseNumber = Console.ReadLine();
            VehicleCard vehicle = m_GarageManager.GetVehicleCardByLicenseID(licenseNumber);
            if (vehicle.Veichle.Power is Electric)
            {
                chargingElectricVehicleByLicenseID(licenseNumber); 
            }
            else
            {
                string menuToPrint = string.Format(@"Your vehicle is gas-fueled. Would you like to refuel it?
[1] Yes
[2] No, return to main menu.
");
                Console.WriteLine(menuToPrint);
                char option = Console.ReadKey().KeyChar;
                switch (option)
                {
                    case '1':
                        {
                            refuelGasVehicleByLicenseID(licenseNumber);
                            break;
                        }
                    case '2':
                        {
                            Ex02.ConsoleUtils.Screen.Clear();
                            printMainMenu();
                            return;//todo are we allowed???

                        }
                }
            }
        }

        private void refuelGasVehicleByLicenseID(string i_LicenseID)
        {
            float amountOfGas;
            string menuToPrint = string.Format(@"Please choose the type of fuel you would like:
[1] Solar
[2] Octan95
[3] Octan96
[4] Octan98
");
            Console.WriteLine(menuToPrint);
            string gasTypeInput = Console.ReadLine();
            int gasType = int.Parse(gasTypeInput);
            Console.WriteLine("Please enter amount of fuel to fill: ");
            string amountToFill = Console.ReadLine();
            if (!int.TryParse(gasTypeInput, out gasType))
            {
                Console.WriteLine("OutOf Range~");//TODO exepction
            }

            if (float.TryParse(amountToFill, out amountOfGas)) //TODO expection
            {
                m_GarageManager.FillEnergyByAmount(i_LicenseID, amountOfGas, getGasType(gasType));
            }
        }
        private void refuelGasVehicle()
        {
            Console.WriteLine("Please enter your license number: ");
            string licenseNumber = Console.ReadLine();
            VehicleCard vehicle = m_GarageManager.GetVehicleCardByLicenseID(licenseNumber);
            if (vehicle.Veichle.Power is Gas)
            {
                refuelGasVehicleByLicenseID(licenseNumber);
            }
            else
            {
                string menuToPrint = string.Format(@"Your vehicle is electric. Would you like to charge it?
[1] Yes
[2] No, return to main menu.
");
                Console.WriteLine(menuToPrint);
                char option = Console.ReadKey().KeyChar;
                switch (option)
                {
                    case '1':
                        {
                            chargingElectricVehicleByLicenseID(licenseNumber);
                            break;
                        }
                    case '2':
                        {
                            Ex02.ConsoleUtils.Screen.Clear();
                            printMainMenu();
                            return;//todo are we allowed???

                        }
                }
            }
        }

        private Gas.eTypeOfGas getGasType(int i_typeOfGas)
        {
            Gas.eTypeOfGas gasType = Gas.eTypeOfGas.Unknown;

            switch (i_typeOfGas)
            {
                case 1:
                    {
                        gasType = Gas.eTypeOfGas.Solar;
                        break;
                    }
                case 2:
                    {
                        gasType = Gas.eTypeOfGas.Octan95;
                        break;
                    }
                case 3:
                    {
                        gasType = Gas.eTypeOfGas.Octan96;
                        break;
                    }
                case 4:
                    {
                        gasType = Gas.eTypeOfGas.Octan98;
                        break;
                    }
                    //case default:
                    //    {
                    //        //TODO expcetion in valid input of choise
                    //        break;
                    //    }
            }
            return gasType;
        }

        public void InflatingWheels()
        {
            Console.WriteLine("Please enter your license number: ");
            string licenseNumber = Console.ReadLine();
            m_GarageManager.WheelInflation(licenseNumber);
        }

        public void ChangeStateOfVehicle()
        {
            Console.WriteLine("Please enter your license number: ");
            string licenseNumber = Console.ReadLine();
            while (m_GarageManager.GetVehicleCardByLicenseID(licenseNumber) == null)
            {
                string menuToPrint = string.Format(@"License number isn't found.
[1] Try again.
[2] Return to main menu.
");
                Console.WriteLine(menuToPrint);
                char option = Console.ReadKey().KeyChar;
                switch (option)
                {
                    case '1':
                        {
                            Console.WriteLine("\nPlease enter your license number: ");
                            licenseNumber = Console.ReadLine();
                            break;
                        }
                    case '2':
                        {
                            Ex02.ConsoleUtils.Screen.Clear();
                            printMainMenu();
                            return;//todo are we allowed???
                        };
                }
            }
            VehicleCard.eVehicleState newStateOfVehicle = getNewStateOfVehicleFromUser();
            m_GarageManager.ChangeStateOfVehicle(licenseNumber, newStateOfVehicle);
        }

        private void changeStateOfVehicle(string i_LicenseID)
        {
            VehicleCard.eVehicleState newStateOfVehicle = getNewStateOfVehicleFromUser();
            m_GarageManager.ChangeStateOfVehicle(i_LicenseID, newStateOfVehicle);
        }

        private VehicleCard.eVehicleState getNewStateOfVehicleFromUser()
        {
            string menuToPrint = string.Format(@"
[1] Change state to in-repair.
[2] Change state to paid.
[3] Change state to repaired. 
");
            Console.WriteLine(menuToPrint);
            string newStateOfVehicle = Console.ReadLine();
            int newStateOfVehicleInput = int.Parse(newStateOfVehicle);

            if (!int.TryParse(newStateOfVehicle, out newStateOfVehicleInput))
            {
                Console.WriteLine("OutOf Range~");//TODO exepction
            }

            return getNewStateOfVehicle(newStateOfVehicleInput);
        }

        private VehicleCard.eVehicleState getNewStateOfVehicle(int i_NewStateOfVehicle)
        {
            VehicleCard.eVehicleState returnValueStateOfVehicle = VehicleCard.eVehicleState.InRepair;

            switch (i_NewStateOfVehicle)
            {
                case 1:
                    {
                        returnValueStateOfVehicle = VehicleCard.eVehicleState.InRepair;
                        break;
                    }
                case 2:
                    {
                        returnValueStateOfVehicle = VehicleCard.eVehicleState.Paid;
                        break;
                    }
                case 3:
                    {
                        returnValueStateOfVehicle = VehicleCard.eVehicleState.Repaired;
                        break;
                    }
                    //case default:
                    //    {
                    //        //TODO expcetion in valid input of choise
                    //        break;
                    //    }
            }
            return returnValueStateOfVehicle;
        }

        private string insertNewVehicle(out bool o_IsAlreadyExist)
        {
            Vehicle vehicleToReturn = null;
            o_IsAlreadyExist = false;
            Console.WriteLine("Please enter your license number: ");//todo validation expection
            string licenseNumber = Console.ReadLine();
            if (m_GarageManager.IsGarageContainsThisVehicle(licenseNumber))
            {
                Console.WriteLine(@"This vehicle is already exist in the system.
Status changed to:Inrepair.");
                m_GarageManager.ChangeStateOfVehicle(licenseNumber, VehicleCard.eVehicleState.InRepair);
                o_IsAlreadyExist = true;
            }
            else
            {

                Console.WriteLine("Please enter your name: ");
                string ownerName = Console.ReadLine();
                Console.WriteLine("Please enter your phone: ");
                string ownerPhone = Console.ReadLine();
                Console.WriteLine("Please enter your type of vehicle: ");
                string types = string.Format(@"[1] Car
[2] MotorBike
[3] Truck");
                Console.WriteLine(types);
                char option = Console.ReadKey().KeyChar;
                Console.WriteLine();
                VehicleCard.eTypeOfVehicle typeOfVehicle;

                if (isValidSelection(option, out typeOfVehicle))
                {
                    vehicleToReturn = GetVehicle(typeOfVehicle, licenseNumber);
                }
                else
                {
                    Console.WriteLine("Error.");
                    //exception
                }
                m_GarageManager.InsertNewVehicle(licenseNumber, ownerName, ownerPhone, vehicleToReturn);
            }
            return licenseNumber;
        }

        public static List<Vehicle.Wheel> getWheelProperies(VehicleCard.eTypeOfVehicle i_vechileType)
        {
            Console.WriteLine("Please enter wheel's manufacturer name: ");
            string manufacturerName = Console.ReadLine();
            Console.WriteLine("Please enter your current air preasure: ");
            string currentAirPreasureAsString = Console.ReadLine();
            float currentAirPreasure = float.Parse(currentAirPreasureAsString);
            float maxAirPreasure = -1;
            List<Vehicle.Wheel> listOfWheels = new List<Vehicle.Wheel>();
            int numOfWheels = VehicleCard.GetNumOfWheels(i_vechileType);
            for (int i = 0; i < numOfWheels; i++)
            {
                Vehicle.Wheel wheel = new Vehicle.Wheel(manufacturerName, currentAirPreasure, maxAirPreasure);
                listOfWheels.Add(wheel);
            }

            return listOfWheels;
        }

        //public Vehicle CreateVehicleByType(string i_ModelName, string i_LicenseID, List<Vehicle.Wheel> i_ListOfWheel, Vehicle.eTypeOfEnergy i_EnergyType, string i_ManufacturerName, float i_CurrentAirPreasure, float i_RemainingPower)
        //{
        //    Vehicle vehicleToReturn = null;

        //    Console.WriteLine("Please enter your vechile type: "); //TODO validation
        //    string VehicleType = Console.ReadLine();
        //    switch (VehicleType)
        //    {
        //        case "car":
        //            {
        //                // ask for color
        //                //ask for doors

        //                // vehicleToReturn=VehicleCreator.CreateNewCar() /all of paprameters
        //                //vehicleToReturn = new Car()
        //                //new Car(i_ModelName, i_LicenseID, 12f, i_ListOfWheel, i_EnergyType);// odo: add Remaining power
        //                break;
        //            }
        //        case "motorbike":
        //            {
        //                //
        //                //      typeOfVechile = VehicleCard.eTypeOfVehicle.Motorbike;
        //                break;
        //            }
        //        case "truck":
        //            {

        //                break;
        //            }
        //    }

        //    return vehicleToReturn;
        //}

        private bool isValidSelection(char i_Option, out VehicleCard.eTypeOfVehicle o_TypeOfVehicle)
        {
            bool isValid;
            switch (i_Option)
            {
                case '1':
                    {
                        o_TypeOfVehicle = VehicleCard.eTypeOfVehicle.Car;
                        isValid = true;
                    }
                    break;
                case '2':
                    {

                        o_TypeOfVehicle = VehicleCard.eTypeOfVehicle.Motorbike;
                        isValid = true;
                    }
                    break;
                case '3':
                    {
                        o_TypeOfVehicle = VehicleCard.eTypeOfVehicle.Truck;
                        isValid = true;
                    }
                    break;
                default:
                    {
                        o_TypeOfVehicle = VehicleCard.eTypeOfVehicle.Unknown;
                        isValid = false;
                    }
                    break;
            }
            return isValid;
        }

        private bool isValidEngineType(char i_Option, out Vehicle.eTypeOfEnergy i_TypeOfEnergy)
        {
            bool isValid;
            switch (i_Option)
            {
                case '1':
                    {
                        i_TypeOfEnergy = Vehicle.eTypeOfEnergy.Gas;
                        isValid = true;
                    }
                    break;
                case '2':
                    {
                        i_TypeOfEnergy = Vehicle.eTypeOfEnergy.Electric;
                        isValid = true;
                    }
                    break;
                default:
                    {
                        i_TypeOfEnergy = 0;
                        isValid = false;
                    }
                    break;
            }
            return isValid;
        }

        public Vehicle GetVehicle(VehicleCard.eTypeOfVehicle i_typeOfVehicle, string i_LicenseNumber)
        {
            Console.WriteLine("\nPlease enter your vehicle's model name: ");
            string modelName = Console.ReadLine();
            string chooseEngine = string.Format(@"\nPlease select the type of motor vehicle's energy power:
[1] Gas
[2] Electric");
            Console.WriteLine(chooseEngine);
            char engineType = Console.ReadKey().KeyChar;
            Vehicle.eTypeOfEnergy typeOfEnergy;
            while (!isValidEngineType(engineType, out typeOfEnergy))
            {
                Console.WriteLine("error");
                engineType = Console.ReadKey().KeyChar;
                //Exception
            }
            Console.WriteLine("\nPlease enter the amount of energy left in your vehicle: ");
            string remainingPowerAsString = Console.ReadLine();
            float remainingPower = float.Parse(remainingPowerAsString);
            List<Vehicle.Wheel> listOfWheel = getListOfWheel(i_typeOfVehicle);
            return CreateVehicleByType(i_typeOfVehicle, modelName, i_LicenseNumber, listOfWheel, typeOfEnergy, remainingPower);
        }

        private List<Vehicle.Wheel> getListOfWheel(VehicleCard.eTypeOfVehicle i_typeOfVehicle)
        {
            float airPreasure;
            string airPreasureString;
            Vehicle.Wheel wheel;
            List<Vehicle.Wheel> listOfWheel = new List<Vehicle.Wheel>();
            int numOfWheels = VehicleCard.GetNumOfWheels(i_typeOfVehicle);

            for (int i = 0; i < numOfWheels; i++)
            {
                wheel = new Vehicle.Wheel();
                Console.WriteLine("Please enter the air presure in your wheel: {0}/{1}", i + 1, numOfWheels);
                airPreasureString = Console.ReadLine();
                float.TryParse(airPreasureString, out airPreasure);
                Console.WriteLine("Please enter the name of the wheel's manufacturer: {0}/{1}", i + 1, numOfWheels);
                string manufacturer = Console.ReadLine();
                wheel.CurrentAirPreasure = airPreasure;
                wheel.ManufacturerName = manufacturer;
                listOfWheel.Add(wheel);
            }
            return listOfWheel;
        }

        private Car.eCarColor getCarColor(char i_Option)
        {
            Car.eCarColor color;
            if (i_Option == '1')
            {
                color = Car.eCarColor.Red;
            }
            else if (i_Option == '2')
            {
                color = Car.eCarColor.White;
            }
            else if (i_Option == '3')
            {
                color = Car.eCarColor.Black;
            }

            else if (i_Option == '4')
            {
                color = Car.eCarColor.Yellow;
            }
            else
            {
                Console.WriteLine("Error");
                color = 0;
                //Eception
            }
            return color;
        }

        private bool isValidLicenseType(char i_Option, out Motorbike.eTypeOfLicense o_TypeOfLicense)
        {
            bool isValid = true;
            switch (i_Option)
            {
                case '1':
                    {
                        o_TypeOfLicense = Motorbike.eTypeOfLicense.A;
                        break;
                    }

                case '2':
                    {
                        o_TypeOfLicense = Motorbike.eTypeOfLicense.A1;
                        break;
                    }

                case '3':
                    {
                        o_TypeOfLicense = Motorbike.eTypeOfLicense.AB;
                        break;
                    }

                case '4':
                    {
                        o_TypeOfLicense = Motorbike.eTypeOfLicense.B1;
                        break;
                    }
                default:
                    {
                        isValid = false;
                        o_TypeOfLicense = 0;
                        break;
                    }
            }
            return isValid;
        }

        public Vehicle CreateVehicleByType(VehicleCard.eTypeOfVehicle i_VehicleType, string i_ModelName, string i_LicenseID, List<Vehicle.Wheel> i_ListOfWheel, Vehicle.eTypeOfEnergy i_EnergyType, float i_RemainingPower)
        {
            Vehicle vehicleToReturn = null;
            switch (i_VehicleType)
            {
                case VehicleCard.eTypeOfVehicle.Car:
                    {
                        string menuToPrint = string.Format(@"Please enter your car's color:
[1] Yellow
[2] White
[3] Red
[4] Black
");
                        Console.WriteLine(menuToPrint);
                        char color = Console.ReadKey().KeyChar;
                        Car.eCarColor carColor = getCarColor(color);
                        string menuDoorsToPrint = string.Format(@"lease enter your car's number of doors::
[1] 2
[2] 3
[3] 4
[4] 5
");
                        Console.WriteLine(menuDoorsToPrint);
                        string doorsString = Console.ReadLine();
                        int numOfDoors;
                        if (int.TryParse(doorsString, out numOfDoors) || numOfDoors < 2 || numOfDoors > 5)
                        {
                            vehicleToReturn = VehicleCreator.CreateNewCar(i_ModelName, i_LicenseID, i_ListOfWheel, i_EnergyType, i_RemainingPower, carColor, numOfDoors);
                        }
                        else
                        {
                            // expcetion TODO
                        }
                        break;
                    }
                case VehicleCard.eTypeOfVehicle.Motorbike:
                    {
                        Motorbike.eTypeOfLicense typeOfLicense;
                        Console.WriteLine(@"Please enter your license type:
[1] A
[2] A1
[3] AB
[4] B1");
                        char option = Console.ReadKey().KeyChar;
                        isValidLicenseType(option, out typeOfLicense);
                        Console.WriteLine("Please enter the volume of the motorbike in CC:");
                        char volume = Console.ReadKey().KeyChar;
                        vehicleToReturn = VehicleCreator.CreateNewMotorbike(i_ModelName, i_LicenseID, i_RemainingPower, i_ListOfWheel, i_EnergyType, typeOfLicense, volume);
                        break;
                    }
                case VehicleCard.eTypeOfVehicle.Truck:
                    {
                        Console.WriteLine(@"Please enter if you carrying dangerous materials:
[1] Yes
[2] No
");
                        string carryngDangerousMaterialInput = Console.ReadLine();
                        bool isCarryngDangerousMaterial;
                        if (bool.TryParse(carryngDangerousMaterialInput, out isCarryngDangerousMaterial))
                        {
                            Console.WriteLine(@"Please enter if your maximum legit carry weight: ");
                            char option = Console.ReadKey().KeyChar;
                            int maxLegitCarry;
                            if (int.TryParse(option.ToString(), out maxLegitCarry))
                            {
                                vehicleToReturn = VehicleCreator.CreateNewTruck(i_ModelName, i_LicenseID, i_ListOfWheel, i_RemainingPower, isCarryngDangerousMaterial, maxLegitCarry);
                            }
                            else
                            {
                                //todo excpetion
                            }
                        }
                        else
                        {
                            //todo excepention
                        }

                        break;
                    }
            }

            return vehicleToReturn;
        }



    }

}