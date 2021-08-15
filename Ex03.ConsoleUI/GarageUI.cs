using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    class GarageUI
    {

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
                    Console.WriteLine("InsertCar");
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
            bool m_carIsAlreadyInGarage = false;

            Console.WriteLine("Enter license number:");
            string number = InputUtils.getLicenseNumberFromUser(k_minLicenseNumberLength, k_maxLicenseNumberLength);

            Console.WriteLine(number);

        }



    }
}
