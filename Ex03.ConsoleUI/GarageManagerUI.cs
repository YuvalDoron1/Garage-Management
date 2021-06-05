using System;
using Ex03.GarageLogic;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    class GarageManagerUI
    {
        private static Garage garage = new Garage();

        public static void MenuDisplay()
        {
            string menu = string.Format(
@"Please choose an action:
1. Insert a new vehicle to the garage
2. Display all license numbers of vehicles in the garage
3. Display license number of specific vehicles in the garage (by their status)
4. Change a certain vehicle's status
5. Inflate wheel air pressure of a vehicle to it's capacity
6. Refuel a fuel energy based vehicle
7. Charge an electric energy based vehicle
8. Display information on a vehicle");
            int action;
            bool exited = false;
            string exitStr;

            while (!exited)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine(menu);
                    action = IOUtils.getOptionInRange(1, 8);
                    switch (action)
                    {
                        case 1:
                            insertVehicleToGarage();
                            break;
                        case 2:
                            displayAllLicenses();
                            break;
                        case 3:
                            displayLicensesByStatus();
                            break;
                        case 4:
                            changeVehicleStatus();
                            break;
                        case 5:
                            inflateTiresToMax();
                            break;
                        case 6:
                            refulAFuelBasedVehicle();
                            break;
                        case 7:
                            chargeAnElectricBasedVehicle();
                            break;
                        case 8:
                            displayVehicleInfo();
                            break;
                    }
                    Console.WriteLine($"{Environment.NewLine}To exit press 'X', for another action press any other key");
                    exitStr = Console.ReadLine();
                    if (exitStr.Equals("X"))
                    {
                        exited = true;
                    }
                }

                catch (FormatException fe)
                {
                    Console.WriteLine($"{fe.Message}{Environment.NewLine}Press any key to go back to menu..");
                    IOUtils.readAndClear();
                }
            }
        }

        private static void changeVehicleStatus()
        {
            string licenseNumber = IOUtils.getLicenseNumber();

            int vehicleStatusInt = 0;
            VehicleOwner.eVehicleStatus vehicleStatus;
            string msgStatus = "Please choose your required vehicle status:";
            string[] statusOptions = Garage.GetVehicleStatusOptions();

            vehicleStatusInt = IOUtils.getEnumChoice(msgStatus, statusOptions);
            vehicleStatus = (VehicleOwner.eVehicleStatus)vehicleStatusInt;

            try
            {
                garage.ChangeStatus(licenseNumber, vehicleStatus);
                Console.WriteLine("Vehicle's status was changed!");
            }

            catch (ValueOutOfRangeException vore)
            {
                Console.WriteLine(vore.Message);
            }

            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        private static void chargeAnElectricBasedVehicle()
        {
            string licenseNumber = IOUtils.getLicenseNumber();
            bool validMinToCharge = false;
            int minutesToCharge = 0;

            while (!validMinToCharge)
            {
                Console.WriteLine("Please enter amount of minutes to fill:");
                try
                {
                   minutesToCharge = IOUtils.getPositiveInt();
                   validMinToCharge = true;

                }

                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }

            try
            {
                garage.ChargeElectricBasedVehicle(licenseNumber, minutesToCharge);
                Console.WriteLine("Vehicle was successfully charged!");
            }

            catch (ValueOutOfRangeException vore)
            {
                Console.WriteLine(vore.Message);
            }

            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        private static void refulAFuelBasedVehicle()
        {
            string licenseNumber = IOUtils.getLicenseNumber();
            int fuelTypeInt = 0;
            FuelEnergy.eFuelType fuelType;
            bool vaildAmountToFill = false;
            float amountToFill = 0;
            string msgFuelType = "Please enter your vehicle's fuel type:";
            string[] vehicleFuleTypes = FuelEnergy.GetFuelTypes();
            fuelTypeInt = IOUtils.getEnumChoice(msgFuelType, vehicleFuleTypes);
            fuelType = (FuelEnergy.eFuelType)fuelTypeInt;

            while (!vaildAmountToFill)
            {
                Console.WriteLine("Please enter the desired amount of fuel to fill:");
                try
                {
                    amountToFill = IOUtils.getPositiveFloat();
                    vaildAmountToFill = true;

                }

                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }

            try
            {
                garage.RefuelFuelBasedVehicle(licenseNumber, fuelType, amountToFill);
                Console.WriteLine("Vehicle was successfully fueld!");
            }

            catch (ValueOutOfRangeException vore)
            {
                Console.WriteLine(vore.Message);
            }

            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            } 
        }

        private static void inflateTiresToMax()
        {
            string licenseNumber = IOUtils.getLicenseNumber();

            try
            {  
                 garage.InflateWheelsToMaximum(licenseNumber);
                Console.WriteLine("Tires are inflated to max!");

            }

            catch (ValueOutOfRangeException vore)
            {
                Console.WriteLine(vore.Message);
            }

            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        private static void insertVehicleToGarage()
        {
            string licenseNumber = IOUtils.getLicenseNumber();

            try
            {
                if (garage.IsVehicleInGarage(licenseNumber))
                {
                    garage.ChangeStatus(licenseNumber, VehicleOwner.eVehicleStatus.InRepair);
                    Console.WriteLine("Car was already registered in garage, current status: 'In Repair'");
                }
                else
                {
                    Vehicle vehicleToInsert = createVehicleByUser(licenseNumber);
                    setEnergyByUser(vehicleToInsert);
                    setWheelsByUser(vehicleToInsert);
                    registerOwner(vehicleToInsert);
                    garage.AddVehicleToGarage(vehicleToInsert);
                    Console.WriteLine("Vehicle was successfully inserted to garage!");
                }
            }

            catch (FormatException fe) 
            {
                Console.WriteLine(fe.Message);
                insertVehicleToGarage();
            }
        }

        private static Vehicle createVehicleByUser(string i_LicenseNumber)
        {
            Vehicle newVehicle = null;
            string modelName;

            Console.WriteLine("Please enter your vehicle model name:");
            modelName = IOUtils.readAndClear();
            string msgVehicleType = "Please choose Vehicle type:";
            string[] vehicleTypes = Vehicle.GetVehicleTypes();
            int vehicleType = IOUtils.getEnumChoice(msgVehicleType, vehicleTypes);
            switch (vehicleType)
            {
                case 1:
                    newVehicle = createCarByUser(i_LicenseNumber, modelName);
                    break;
                case 2:
                    newVehicle = createTruckByUser(i_LicenseNumber, modelName);
                    break;
                case 3:
                    newVehicle = createMotorcycleByUser(i_LicenseNumber, modelName);
                    break;
            }

            return newVehicle;
        }

        private static Vehicle createCarByUser(string i_LicenseNumber, string i_ModelName)
        {
            int colorInt = 0;
            Car.eColor color;
            string msgColor = "Please enter car's color:";
            string[] colorChoices = Car.GetCarColors();
            colorInt = IOUtils.getEnumChoice(msgColor, colorChoices);
            color = (Car.eColor)colorInt;
            int numOfDoorsInt = 0;
            Car.eNumOfDoors numOfDoors;
            string msgNumOfDoors = "Please enter car's number of doors:";
            string[] doorNumberChoices = Car.GetCarNumberOfDoors();
            numOfDoorsInt = IOUtils.getEnumChoice(msgNumOfDoors, doorNumberChoices);
            numOfDoors = (Car.eNumOfDoors)numOfDoorsInt;

            return SupportedVehiclesCreator.CreateCar(i_ModelName, i_LicenseNumber, color, numOfDoors);
        }

        private static Vehicle createTruckByUser(string i_LicenseNumber, string i_ModelName)
        {
            string drivesDangerousMaterialsStr;
            bool drivesDangerousMaterials = false;
            bool validCargo = false;
            bool validMateriasInput = false;
            float maxCargo = 0;

            while (!validMateriasInput)
            {
                try
                {
                    Console.WriteLine("If the truck contains dangerous materials, please type true, otherwise type false:");
                    drivesDangerousMaterialsStr = IOUtils.readAndClear();
                    drivesDangerousMaterials = bool.Parse(drivesDangerousMaterialsStr);
                    validMateriasInput = true;
                }

                catch (FormatException)
                {
                    Console.WriteLine("You didn't typed true or false.");
                }
            }

            Console.WriteLine("Please enter the truck max cargo weight:");
            while (!validCargo)
            {
                try
                {
                    maxCargo = IOUtils.getPositiveFloat();
                    validCargo = true;
                }

                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }

            return SupportedVehiclesCreator.CreateTruck(i_ModelName, i_LicenseNumber, drivesDangerousMaterials, maxCargo);
        }

        private static Vehicle createMotorcycleByUser(string i_LicenseNumber, string i_ModelName)
        {
            string msgLicenseType = "Please enter license type(1 - 4):";
            string[] licenseTypes = Motorcycle.GetLicenseTypes();
            int engineVolume = 0;
            bool validEngineVolume = false;
            int licenseTypeInt = IOUtils.getEnumChoice(msgLicenseType, licenseTypes);
            Motorcycle.eLicenseType licenseType = (Motorcycle.eLicenseType)licenseTypeInt;

            while (!validEngineVolume)
            {
                try
                {
                    Console.WriteLine("Please enter motorcycle's engine volume:");
                    engineVolume = IOUtils.getPositiveInt();
                    validEngineVolume = true;
                }

                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }

            return SupportedVehiclesCreator.CreateMotorcycle(i_ModelName, i_LicenseNumber, licenseType, engineVolume);
        }

        private static void displayAllLicenses()
        {
            List<string> licensesList = garage.GetAllLicenseNumbers();

            printLicensesList(licensesList);
        }

        private static void displayLicensesByStatus()
        {
            string msgRequiredStatus = "Choose the required status: (press 1-3 digit)";
            string[] vehicleStatus = Garage.GetVehicleStatusOptions();
            int statusInt = IOUtils.getEnumChoice(msgRequiredStatus, vehicleStatus);
            VehicleOwner.eVehicleStatus status = (VehicleOwner.eVehicleStatus)statusInt;
            List<string> licensesList = garage.GetLicenseNumbersByStatus(status);

            printLicensesList(licensesList);
        }

        private static void printLicensesList(List<string> i_LicensesList)
        {
            if (i_LicensesList.Count == 0)
            {
                Console.WriteLine("There are no vehicles in the garage that matches your search.");
            }
            else
            {
                Console.WriteLine("Vehicle license numbers that are currently in garage:");
                foreach (string license in i_LicensesList)
                {
                    Console.WriteLine(license);
                }
            }
        }

        private static void setEnergyByUser(Vehicle i_Vehicle)
        {
            bool validEnergyAmount = false;
            int energyTypeInt = 1;
            float energyAmount;
            EnergySource.eEnergyType energyType;

            if (!(i_Vehicle is Truck))
            {
                string msgEnergySrc = "What is the vehicle energy source:";
                string[] energySrcOptions = EnergySource.GetEnergySrcOptions();
                energyTypeInt = IOUtils.getEnumChoice(msgEnergySrc, energySrcOptions);
            }

            energyType = (EnergySource.eEnergyType)energyTypeInt;
            while (!validEnergyAmount)
            {
                try
                {
                    if (energyType.Equals(EnergySource.eEnergyType.Fuel))
                    {
                        Console.WriteLine("Enter the current fuel amount of the vehicle:");
                    }
                    else if (energyType.Equals(EnergySource.eEnergyType.Electric))
                    {
                        Console.WriteLine("Enter the current electric energy hours left:");
                    }

                    energyAmount = IOUtils.getPositiveFloat();
                    i_Vehicle.InitializeEnergySource(energyType, energyAmount);
                    validEnergyAmount = true;
                }

                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }

                catch (ValueOutOfRangeException vore)
                {
                    Console.WriteLine($"{vore.Message}, Try setup energy details again:");
                }
            }
        }

        private static void setWheelsByUser(Vehicle i_Vehicle)
        {
            string manufacturer;
            float airPressure;
            bool validAirPressureInput = false;

            Console.WriteLine("Enter the wheels manufacturer name:");
            manufacturer = IOUtils.readAndClear();
            while (!validAirPressureInput)
            {          
                try
                {
                    Console.WriteLine("Enter current wheels air pressure:");
                    airPressure = IOUtils.getPositiveFloat();
                    i_Vehicle.InitializeWheels(manufacturer, airPressure);
                    validAirPressureInput = true;
                }

                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }

                catch (ValueOutOfRangeException vore)
                {
                    Console.WriteLine(vore.Message);
                }
            }
        }

        private static void registerOwner(Vehicle i_Vehicle)
        {
            string ownerName;
            string ownerPhone;
            bool validOwner = false;

            while (!validOwner) 
            {
                try
                {
                    Console.WriteLine("Please enter the vehicle owner name:");
                    ownerName = IOUtils.getValidAlphaString();
                    Console.WriteLine("Please enter the vehicle owner phone number:");
                    ownerPhone = IOUtils.getValidNumericString();
                    i_Vehicle.RegisterOwnerToCar(ownerName, ownerPhone);
                    validOwner = true;
                }

                catch (FormatException fe)
                {
                    Console.WriteLine(fe.Message);
                }
            }
        }

        private static void displayVehicleInfo()
        {
            string licenseNumber = IOUtils.getLicenseNumber();

            try
            {
                Console.WriteLine(garage.DisplayVehicleInformation(licenseNumber));
            }
                
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    }
}