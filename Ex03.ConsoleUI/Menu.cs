using System;
using System.Collections.Generic;

namespace Ex03.ConsoleUI
{
    internal class Menu
    {
        private readonly List<KeyValuePair<string, eActionType>> r_Options = new List<KeyValuePair<string, eActionType>>(10);

        public Menu()
        {
            initMenu();
        }

        private void initMenu()
        {
            addOption("Insert new car", Menu.eActionType.InsertCar);
            addOption("Show cars", Menu.eActionType.ShowVehicles);
            addOption("Inflate air to max", Menu.eActionType.InflateAir);
            addOption("Refuel a car", Menu.eActionType.Refuel);
            addOption("Charge a car", Menu.eActionType.Charge);
            addOption("View car details", Menu.eActionType.ViewVehicle);
            addOption("Exit", Menu.eActionType.Exit);
        }

        public int Length
        {
            get { return r_Options.Count; }
        }

        public void addOption(string i_title, eActionType i_ActionType)
        {
            r_Options.Add(new KeyValuePair<string, eActionType>(i_title, i_ActionType));

        }

        public eActionType getActionOfOption(int optionNumber)
        {
            return r_Options[optionNumber - 1].Value;
        }

        public void printMenu() {
            Console.WriteLine("Menu:");
            for(int i=1; i <= Length; i++)
            {
                Console.WriteLine(i + ". " + r_Options[i - 1].Key);
            }
            Console.WriteLine();
        }

        public enum eActionType
        {
            InsertCar,
            ShowVehicles,
            ChangeStatus,
            InflateAir,
            Refuel,
            Charge,
            ViewVehicle,
            Exit
        }
    }
} 
