using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    class Menu
    {
        private List<KeyValuePair<string, eActionType>> m_options = new List<KeyValuePair<string, eActionType>>(10);

        public int Length
        {
            get { return m_options.Count; }
        }

        public void addOption(string i_title, eActionType i_ActionType)
        {
            m_options.Add(new KeyValuePair<string, eActionType> (i_title, i_ActionType));
        }

        public eActionType getActionOfOption(int optionNumber)
        {
            return m_options[optionNumber - 1].Value;
        }

        public void printMenu() {
            Console.WriteLine("Menu:");
            for(int i=1; i <= Length; i++)
            {
                Console.WriteLine(i + ". " + m_options[i - 1].Key);
            }
            Console.WriteLine();
        }

        public enum eActionType
        {
            InsertCar,
            ShowCars,
            InflateAir,
            Refuel,
            Charge,
            ViewCar,
            Exit
        }

    }
} 
