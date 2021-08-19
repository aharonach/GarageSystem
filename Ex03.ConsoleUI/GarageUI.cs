using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class GarageUI
    {
        private readonly Garage r_Garage = new Garage();
        private readonly Menu r_Menu = new Menu();
        
        private Garage Garage
        {
            get
            {
                return r_Garage;
            }
        }

        private Menu Menu
        {
            get
            {
                return r_Menu;
            }
        }

        public void Run()
        {
            Menu.eActionType optionActionType;

            printHeader("WELCOME TO THE GARAGE!");

            do
            {
                Menu.PrintMenu();
                int userInput = getMenuChoice(Menu.Length);
                optionActionType = Menu.GetActionOfOption(userInput);
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
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while(true);
        }

        private void doAction(Menu.eActionType option)
        {
            Console.Clear();

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
                    inflateAirInWheels();
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

            Console.WriteLine("\n");
        }

        private void insertVehicle()
        {
            try
            {
                string license = InputUtils.GetLicenseNumberFromUser();

                if(Garage.IsVehicleExistsInGarage(license))
                {
                    Garage.UpdateVehicleStatusInGarage(license, Garage.eVehicleStatus.InRepair);
                    Console.WriteLine(
                        @"Vehicle already exists in the garage. It's status changed to {0}.",
                        Garage.eVehicleStatus.InRepair);
                }
                else
                {
                    Console.WriteLine("\nEnter person's name:");
                    string personName = InputUtils.GetStringFromUser();

                    Console.WriteLine("\nEnter person's phone number:");
                    string phone = InputUtils.GetStringFromUser();

                    Console.WriteLine("\nChoose vehicle's type:");
                    int vehicleType = InputUtils.GetChooseFromList(Garage.GetVehicleTypes());

                    Console.WriteLine("\nEnter Model name:");
                    string modelName = InputUtils.GetStringFromUser();

                    Garage.AddVehicle(personName, phone, vehicleType, license, modelName);

                    foreach (Garage.eFieldGroup fieldGroup in Enum.GetValues(typeof(Garage.eFieldGroup)))
                    {
                        askAndUpdateVehicleFieldsByCategory(license, fieldGroup);
                    }

                    Console.WriteLine("\nVehicle added successfully!");
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
            printFieldGroupHeader(i_FieldGroup);

            if(i_FieldGroup == Garage.eFieldGroup.Wheel)
            {
                askAndUpdateWheelsFields(i_License);
            }
            else
            {
                askAndUpdateAdditionalFields(i_License, i_FieldGroup);
            }
        }

        private void askAndUpdateAdditionalFields(string i_License, Garage.eFieldGroup i_FieldGroup)
        {
            Dictionary<string, KeyValuePair<string, Type>> fieldsToUpdate =
                Garage.GetVehicleFieldsToUpdate(i_License, i_FieldGroup);

            do
            {
                try
                {
                    Garage.UpdateVehicleFields(i_License, collectValuesForUpdate(fieldsToUpdate), i_FieldGroup);
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
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (true);
        }

        private void askAndUpdateWheelsFields(string i_License)
        {
            Console.WriteLine("\nDo you want to set the values once for all the wheels?");
            bool applyToAllWheels = InputUtils.GetYesOrNoFromUser();

            do
            {
                try
                {
                    List<Dictionary<string, KeyValuePair<string, Type>>> wheelsFieldsToUpdate =
                        Garage.GetWheelsFieldsToUpdate(i_License);

                    if (applyToAllWheels)
                    {
                        Dictionary<string, object> fieldsToUpdateOnce = collectValuesForUpdate(wheelsFieldsToUpdate[0]);

                        for (int wheelId = 0; wheelId < wheelsFieldsToUpdate.Count; wheelId++)
                        {
                            Garage.UpdateWheelFieldsById(i_License, wheelId, fieldsToUpdateOnce);
                        }
                    }
                    else
                    {
                        List<Dictionary<string, object>> fieldValuePairs = new List<Dictionary<string, object>>();

                        for (int wheelId = 0; wheelId < wheelsFieldsToUpdate.Count; wheelId++)
                        {
                            fieldValuePairs.Add(collectValuesForUpdate(wheelsFieldsToUpdate[wheelId]));
                            Garage.UpdateWheelFieldsById(i_License, wheelId, fieldValuePairs[wheelId]);
                        }
                    }
                    
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
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            while (true);
        }

        private void viewSingleVehicle()
        {
            try
            {
                string license = InputUtils.GetLicenseNumberFromUser();
                Dictionary<string, object> values = Garage.GetVehicleFieldsAndValues(license);
                
                printHeader($"Vehicle {license}");

                foreach (KeyValuePair<string, object> kvp in values)
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
            Dictionary<string, Garage.eVehicleStatus> vehicleStatus = Garage.GetVehiclesLicenses();

            if (vehicleStatus.Count > 0)
            {
                try
                {
                    Console.WriteLine("\nDo you want to filter by status?");
                    bool wantsToFilter = InputUtils.GetYesOrNoFromUser();

                    if (wantsToFilter)
                    {
                        vehicleStatus = Garage.GetVehiclesLicenses(InputUtils.ChooseVehicleStatus());
                    }

                    foreach (KeyValuePair<string, Garage.eVehicleStatus> kvp in vehicleStatus)
                    {
                        Console.WriteLine($"License: {kvp.Key}, Status: {kvp.Value}");
                    }
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ValueOutOfRangeException exception)
                {
                    outOfRangeMessage(exception);
                }
            }
            else
            {
                Console.WriteLine("\nThere are no cars to display.");
            }
        }

        private void updateVehicleStatus()
        {
            try
            {
                string license = InputUtils.GetLicenseNumberFromUser();
                Garage.UpdateVehicleStatusInGarage(license, InputUtils.ChooseVehicleStatus());
                Console.WriteLine("\nVehicle status changed.");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ValueOutOfRangeException exception)
            {
                outOfRangeMessage(exception);
            }
        }

        private void inflateAirInWheels()
        {
            try
            {
                string license = InputUtils.GetLicenseNumberFromUser();
                Garage.InflateAirInVehicleWheels(license);
                Console.WriteLine("\nAir pressure is now maximum.");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (ValueOutOfRangeException exception)
            {
                outOfRangeMessage(exception);
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
                Console.WriteLine("\nVehicle refueled successfully.");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"\nError: {exception.Message}\n");
            }
            catch(ValueOutOfRangeException exception)
            {
                outOfRangeMessage(exception);
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void rechargeVehicle()
        {
            try
            {
                string license = InputUtils.GetLicenseNumberFromUser();

                Console.WriteLine("\nHow much battery time to add? ");
                float amount = InputUtils.GetFloatFromUser();

                Garage.RechargeVehicle(license, amount);
                Console.WriteLine("\nVehicle recharged successfully.");
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
            catch (ValueOutOfRangeException exception)
            {
                outOfRangeMessage(exception);
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private Dictionary<string, object> collectValuesForUpdate(Dictionary<string, KeyValuePair<string, Type>> i_FieldsToUpdate)
        {
            Dictionary<string, object> fieldsWithValues = new Dictionary<string, object>();

            foreach (KeyValuePair<string, KeyValuePair<string, Type>> field in i_FieldsToUpdate)
            {
                KeyValuePair<string, Type> propertyInfo = field.Value;

                Console.WriteLine($"\nEnter {field.Key}:");
                object valueToUpdate = InputUtils.GetParameterValueByType(propertyInfo.Value);

                fieldsWithValues.Add(propertyInfo.Key, valueToUpdate);
            }

            return fieldsWithValues;
        }

        private void outOfRangeMessage(ValueOutOfRangeException i_ValueOutOfRangeException)
        {
            Console.WriteLine($"\nError: {i_ValueOutOfRangeException.Message}");
            Console.WriteLine(
                $"Min value: {i_ValueOutOfRangeException.MinValue}, Max value: {i_ValueOutOfRangeException.MaxValue}.\n");
        }

        private void printHeader(string i_HeaderMessage)
        {
            Console.WriteLine("\n===========\n");
            Console.WriteLine(i_HeaderMessage);
            Console.WriteLine("\n===========\n");
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

            Console.WriteLine("-----------");
        }
    }
}
