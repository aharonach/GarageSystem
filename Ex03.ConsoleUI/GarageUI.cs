using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class GarageUI
    {
        private Garage garage = new Garage();

        private const int k_minLicenseNumberLength = 7;
        private const int k_maxLicenseNumberLength = 8;



        private Menu menu = new Menu();
        // create engine

        public GarageUI()
        {
            initMenu();
        }

        public void run()
        {
            Menu.eActionType optionActionType;
            do
            {
                menu.printMenu();

                int userInput = getMenuChoice(menu.Length);
                optionActionType = menu.getActionOfOption(userInput);

                doAction(optionActionType);

            } while (optionActionType != Menu.eActionType.Exit);
        }

        private void initMenu()
        {
            menu.addOption("Insert new car", Menu.eActionType.InsertCar);
            menu.addOption("Show cars", Menu.eActionType.ShowCars);
            menu.addOption("Inflate air to max", Menu.eActionType.InflateAir);
            menu.addOption("Refuel a car", Menu.eActionType.Refuel);
            menu.addOption("Charge a car", Menu.eActionType.Charge);
            menu.addOption("View car details", Menu.eActionType.ViewCar);
            menu.addOption("Exit", Menu.eActionType.Exit);
        }

        private int getMenuChoice(int i_numberOfOptions)
        {
            int inputFromUser;

            Console.Write("Enter your choice: ");
            inputFromUser = InputUtils.GetNumberFromUserInRange(1, i_numberOfOptions);
        
            return inputFromUser;
        }

        private void doAction(Menu.eActionType option)
        {
            switch (option)
            {
                case Menu.eActionType.InsertCar:
                    insertCar();
                    break;

                case Menu.eActionType.ShowCars:
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
                    Console.WriteLine("ViewCar");
                    break;

                case Menu.eActionType.Exit:
                    Console.WriteLine("Bye");
                    break;
            }
        }

        private void insertCar()
        {
            Console.WriteLine("Enter license number:");
            string number = InputUtils.getLicenseNumberFromUser(k_minLicenseNumberLength, k_maxLicenseNumberLength);

            if(garage.IsVehicleExistsInGarage(number))
            {
                garage.UpdateVehicleInGarageStatus(number, Garage.eVehicleStatus.InRepair);
                Console.WriteLine("Vehicle already exists in the garage. change of vehicle changed to 'In Repair'.");
            }
            else
            {
                Dictionary<string, KeyValuePair<string, Type>> fieldsToUpdate;
                Dictionary<string, object> additionalFields;
                string name, phone, model;


                Console.WriteLine("Enter your name:");
                name = Console.ReadLine();

                Console.WriteLine("Enter your phone number:");
                phone = Console.ReadLine();

                Console.WriteLine("Choose vehicle type:");
                //phone = Console.ReadLine();

                Console.WriteLine("Enter Model name:");
                model = Console.ReadLine();

                garage.AddVehicle(name, phone, VehicleFactory.eType.ElectricCar, number, model);


                fieldsToUpdate = garage.GetVehicleFieldsToUpdate(number, Garage.eFieldGroup.Additional);
                additionalFields = new Dictionary<string, object>();

                foreach (KeyValuePair<string, KeyValuePair<string, Type>> field in fieldsToUpdate)
                {
                    KeyValuePair<string, Type> propertyInfo = field.Value;
                    Console.WriteLine($"Enter value for {field.Key}:");
                    // get the value for
                    object value = propertyInfo.Value; // here needs to get value
                    additionalFields.Add(propertyInfo.Key, value);
                }
                garage.UpdateVehicleFields(number, additionalFields, Garage.eFieldGroup.Additional);



            }
        }



    }
}
