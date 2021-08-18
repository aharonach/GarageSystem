using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class GarageUI
    {
        private const int k_MinLicenseNumberLength = 7;
        private const int k_MaxLicenseNumberLength = 8;
        private readonly Garage r_Garage = new Garage();
        private readonly Menu r_Menu = new Menu();

        public GarageUI()
        {
            initMenu();
        }

        public Garage Garage
        {
            get { return r_Garage; }
        }

        public void Run()
        {
            Menu.eActionType optionActionType;
            do
            {
                r_Menu.printMenu();

                int userInput = getMenuChoice(r_Menu.Length);
                optionActionType = r_Menu.getActionOfOption(userInput);

                doAction(optionActionType);
            } 
            while (optionActionType != Menu.eActionType.Exit);
        }

        private void initMenu()
        {
            r_Menu.addOption("Insert new car", Menu.eActionType.InsertCar);
            r_Menu.addOption("Show cars", Menu.eActionType.ShowCars);
            r_Menu.addOption("Inflate air to max", Menu.eActionType.InflateAir);
            r_Menu.addOption("Refuel a car", Menu.eActionType.Refuel);
            r_Menu.addOption("Charge a car", Menu.eActionType.Charge);
            r_Menu.addOption("View car details", Menu.eActionType.ViewCar);
            r_Menu.addOption("Exit", Menu.eActionType.Exit);
        }

        private int getMenuChoice(int i_NumberOfOptions)
        {
            Console.Write("Enter your choice: ");
            return InputUtils.GetNumberFromUserInRange(1, i_NumberOfOptions);
        }

        private void doAction(Menu.eActionType option)
        {
            switch (option)
            {
                case Menu.eActionType.InsertCar:
                    insertCar();
                    Console.WriteLine("Finish InsertCar");
                    break;

                case Menu.eActionType.ShowCars:
                    showCars();
                    Console.WriteLine("ShowCars");
                    break;

                case Menu.eActionType.InflateAir:
                    Console.WriteLine("InflateAir");
                    break;

                case Menu.eActionType.Refuel:
                    Console.WriteLine("Refuel");
                    break;

                case Menu.eActionType.Charge:
                    Console.WriteLine("Charge");
                    break;

                case Menu.eActionType.ViewCar:
                    viewCar();
                    Console.WriteLine("viewCar");
                    break;

                case Menu.eActionType.Exit:
                    Console.WriteLine("Bye");
                    break;
            }
        }

        private void insertCar()
        {
            string license = InputUtils.GetLicenseNumberFromUser(k_MinLicenseNumberLength, k_MaxLicenseNumberLength);

            if(Garage.IsVehicleExistsInGarage(license))
            {
                Garage.UpdateVehicleInGarageStatus(license, Garage.eVehicleStatus.InRepair);
                Console.WriteLine("Vehicle already exists in the garage. status of vehicle changed to 'In Repair'.");
            }
            else
            {
                Dictionary<string, KeyValuePair<string, Type>> fieldsToUpdate;
                Dictionary<string, object> additionalFields;
                string name, phone, model;

                Console.WriteLine("Enter your name:");
                name = InputUtils.GetValueOfString();

                Console.WriteLine("Enter your phone number:");
                phone = InputUtils.GetValueOfString();

                Console.WriteLine("Choose vehicle type:");
                VehicleFactory.eType vehicleType = (VehicleFactory.eType)InputUtils.GetValueOfEnum(VehicleFactory.eType.ElectricCar.GetType());

                Console.WriteLine("Enter Model name:");
                model = InputUtils.GetValueOfString();

                Garage.AddVehicle(name, phone, vehicleType, license, model);

                askAndUpdateVehicleFieldsByCategory(license, Garage.eFieldGroup.Additional);
                askAndUpdateVehicleFieldsByCategory(license, Garage.eFieldGroup.Wheel);
                askAndUpdateVehicleFieldsByCategory(license, Garage.eFieldGroup.Tank);
            }
        }

        private void askAndUpdateVehicleFieldsByCategory(string i_License, Garage.eFieldGroup i_Category)
        {
            Dictionary<string, KeyValuePair<string, Type>> fieldsToUpdate = Garage.GetVehicleFieldsToUpdate(i_License, i_Category);
            Dictionary<string, object> additionalFields = new Dictionary<string, object>();
            
            foreach (KeyValuePair<string, KeyValuePair<string, Type>> field in fieldsToUpdate)
            {
                KeyValuePair<string, Type> propertyInfo = field.Value;
                Console.WriteLine($"Enter value for {field.Key}:");
                object value = InputUtils.GetParameterByType(propertyInfo.Value);
                additionalFields.Add(propertyInfo.Key, value);
            }
            Garage.UpdateVehicleFields(i_License, additionalFields, i_Category);
        }

        private void viewCar()
        {
            string license = InputUtils.GetLicenseNumberFromUser(k_MinLicenseNumberLength, k_MaxLicenseNumberLength);

            try
            {
                Dictionary<string, object> values = Garage.GetVehicleFieldsAndValues(license);

                foreach (KeyValuePair<string, object> kvp in values)
                {
                    Console.WriteLine($"{kvp.Key} : {kvp.Value}");
                }
            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void showCars()
        {
            Dictionary<string, Garage.eVehicleStatus> vehicleStatus;

            Console.WriteLine("Do you want to filter by status?");
            bool wantsToFilter = InputUtils.GetYesOrNoFromUser();

            if(wantsToFilter)
            {
                vehicleStatus = Garage.GetVehiclesLicenses(InputUtils.ChooseVehicleStatus());
            }
            else
            {
                vehicleStatus = Garage.GetVehiclesLicenses();
            }

            foreach(KeyValuePair<string, Garage.eVehicleStatus> kvp in vehicleStatus)
            {
                Console.WriteLine($"Vehicle {kvp.Key}, Status: {kvp.Value}");
            }
        }

        private void updateVehicleStatus()
        {
            string license = InputUtils.GetLicenseNumberFromUser(k_MinLicenseNumberLength, k_MaxLicenseNumberLength);
            Garage.UpdateStatusOfVehicle(license, InputUtils.ChooseVehicleStatus());
        }
    }
}
