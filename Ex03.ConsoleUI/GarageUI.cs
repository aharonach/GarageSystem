using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class GarageUI
    {
        private readonly Garage r_Garage = new Garage();
        private readonly Menu r_Menu = new Menu();
        
        private Garage Garage
        {
            get { return r_Garage; }
        }

        private Menu Menu
        {
            get { return r_Menu; }
        }

        public void Run()
        {
            Menu.eActionType optionActionType;

            do
            {
                Menu.printMenu();
                int userInput = getMenuChoice(Menu.Length);
                optionActionType = Menu.getActionOfOption(userInput);
                doAction(optionActionType);
            } 
            while (optionActionType != Menu.eActionType.Exit);
        }

        private int getMenuChoice(int i_NumberOfOptions)
        {
            do
            {
                try
                {
                    Console.Write("\nEnter your choice: ");
                    return InputUtils.GetNumberFromUserInRange(1, i_NumberOfOptions);
                }
                catch (ValueOutOfRangeException exception)
                {
                    outOfRangeMessage(exception);
                }
            }
            while(true);
        }

        private void doAction(Menu.eActionType option)
        {
            switch (option)
            {
                case Menu.eActionType.InsertCar:
                    printHeader("Insert New Vehicle");
                    insertVehicle();
                    break;

                case Menu.eActionType.ShowVehicles:
                    printHeader("Show All Vehicles");
                    showAllVehicles();
                    break;

                case Menu.eActionType.ChangeStatus:
                    printHeader("Change Vehicle Status");
                    updateVehicleStatus();
                    break;

                case Menu.eActionType.InflateAir:
                    printHeader("Inflate Air in Vehicle's Wheels");
                    break;

                case Menu.eActionType.Refuel:
                    printHeader("Refuel Vehicle");
                    refuelVehicle();
                    break;

                case Menu.eActionType.Charge:
                    printHeader("Charge Electric Vehicle");
                    rechargeVehicle();
                    break;

                case Menu.eActionType.ViewVehicle:
                    printHeader("View Vehicle");
                    viewSingleVehicle();
                    break;

                case Menu.eActionType.Exit:
                    Console.WriteLine("Goodbye!");
                    break;
            }
        }

        private void insertVehicle()
        {
            try
            {
                string license = InputUtils.GetLicenseNumberFromUser();

                if(Garage.IsVehicleExistsInGarage(license))
                {
                    Garage.UpdateVehicleInGarageStatus(license, Garage.eVehicleStatus.InRepair);
                    Console.WriteLine(
                        @"Vehicle already exists in the garage. It's status changed to {0}.",
                        Garage.eVehicleStatus.InRepair);
                }
                else
                {
                    Console.WriteLine("\nEnter your name:");
                    string personName = InputUtils.GetStringFromUser();

                    Console.WriteLine("\nEnter your phone number:");
                    string phone = InputUtils.GetStringFromUser();

                    Console.WriteLine("\nChoose vehicle's type:");
                    VehicleCreator.eType vehicleType =
                        (VehicleCreator.eType)InputUtils.GetEnumValueFromUser(typeof(VehicleCreator.eType));

                    Console.WriteLine("\nEnter Model name:");
                    string modelName = InputUtils.GetStringFromUser();

                    Garage.AddVehicle(personName, phone, vehicleType, license, modelName);

                    foreach (Garage.eFieldGroup fieldGroup in Enum.GetValues(typeof(Garage.eFieldGroup)))
                    {
                        askAndUpdateVehicleFieldsByCategory(license, fieldGroup);
                    }
                }
            }
            catch(FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch(ValueOutOfRangeException exception)
            {
                outOfRangeMessage(exception);
            }
            catch(ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void askAndUpdateVehicleFieldsByCategory(string i_License, Garage.eFieldGroup i_FieldGroup)
        {
            Dictionary<string, KeyValuePair<string, Type>> fieldsToUpdate =
                Garage.GetVehicleFieldsToUpdate(i_License, i_FieldGroup);

            printFieldGroupHeader(i_FieldGroup);

            do
            {
                try
                {
                    Dictionary<string, object> additionalFields = new Dictionary<string, object>();

                    foreach(KeyValuePair<string, KeyValuePair<string, Type>> field in fieldsToUpdate)
                    {
                        KeyValuePair<string, Type> propertyInfo = field.Value;
                        Console.WriteLine($"\nEnter {field.Key}:");
                        object valueToUpdate = InputUtils.GetParameterValueByType(propertyInfo.Value);
                        additionalFields.Add(propertyInfo.Key, valueToUpdate);
                    }

                    Garage.UpdateVehicleFields(i_License, additionalFields, i_FieldGroup);
                    break;
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ValueOutOfRangeException exception)
                {
                    outOfRangeMessage(exception);
                }
                catch(ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while(true);
        }

        private void viewSingleVehicle()
        {
            try
            {
                string license = InputUtils.GetLicenseNumberFromUser();
                Dictionary<string, object> values = Garage.GetVehicleFieldsAndValues(license);

                foreach(KeyValuePair<string, object> kvp in values)
                {
                    Console.WriteLine($"{kvp.Key} : {kvp.Value}");
                }
            }
            catch(FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch(ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void showAllVehicles()
        {
            try
            {
                Console.WriteLine("Do you want to filter by status?");
                bool wantsToFilter = InputUtils.GetYesOrNoFromUser();

                Dictionary<string, Garage.eVehicleStatus> vehicleStatus =
                    wantsToFilter
                        ? Garage.GetVehiclesLicenses(InputUtils.ChooseVehicleStatus())
                        : Garage.GetVehiclesLicenses();

                foreach(KeyValuePair<string, Garage.eVehicleStatus> kvp in vehicleStatus)
                {
                    Console.WriteLine($"Vehicle {kvp.Key}, Status: {kvp.Value}");
                }
            }
            catch(FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void updateVehicleStatus()
        {
            try
            {
                string license = InputUtils.GetLicenseNumberFromUser();
                Garage.UpdateVehicleInGarageStatus(license, InputUtils.ChooseVehicleStatus());
            }
            catch(ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void refuelVehicle()
        {
            try
            {
                string license = InputUtils.GetLicenseNumberFromUser();

                Console.WriteLine("\nChoose fuel type: ");
                FuelTank.eFuelType fuelType = (FuelTank.eFuelType)InputUtils.GetEnumValueFromUser(typeof(FuelTank.eFuelType));

                Console.WriteLine("\nHow much fuel to add? ");
                float amount = InputUtils.GetFloatFromUser();

                Garage.RefuelVehicle(license, fuelType, amount);
            }
            catch(ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch(ValueOutOfRangeException exception)
            {
                outOfRangeMessage(exception);
            }
        }

        private void rechargeVehicle()
        {
            try
            {
                string license = InputUtils.GetLicenseNumberFromUser();

                Console.WriteLine("How much battery time to add? ");
                float amount = InputUtils.GetFloatFromUser();

                Garage.RechargeVehicle(license, amount);
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
            catch (ValueOutOfRangeException exception)
            {
                outOfRangeMessage(exception);
            }
        }

        private void outOfRangeMessage(ValueOutOfRangeException i_ValueOutOfRangeException)
        {
            Console.WriteLine(i_ValueOutOfRangeException.Message);
            Console.WriteLine(
                $"Min value: {i_ValueOutOfRangeException.MinValue}, Max value: {i_ValueOutOfRangeException.MaxValue}.");
        }

        private void printHeader(string i_HeaderMessage)
        {
            Console.WriteLine("\n===========");
            Console.WriteLine(i_HeaderMessage);
            Console.WriteLine("===========\n");
        }

        private void printFieldGroupHeader(Garage.eFieldGroup i_FieldGroup)
        {
            Console.WriteLine("\n-----------");
            switch(i_FieldGroup)
            {
                case Garage.eFieldGroup.Additional:
                    Console.WriteLine("Additional fields");
                    break;
                case Garage.eFieldGroup.Wheel:
                    Console.WriteLine("Wheel fields");
                    break;
                case Garage.eFieldGroup.Tank:
                    Console.WriteLine("Tank fields");
                    break;
            }
            Console.WriteLine("-----------\n");
        }
    }
}
