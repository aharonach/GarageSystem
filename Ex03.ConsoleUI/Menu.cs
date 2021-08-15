using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{

    class Menu
    {
        private List<MenuOption> m_options = new List<MenuOption>(10);

        public int Length
        {
            get { return m_options.Count; }
        }

        public void addOption(string i_title, eActionType i_ActionType)
        {
            m_options.Add(new MenuOption(i_title, i_ActionType));

        }

        public eActionType getActionOfOption(int optionNumber)
        {
            return m_options[optionNumber - 1].ActionType;
        }

        public void printMenu() {
            Console.WriteLine("Menu:");
            for(int i=1; i <= Length; i++)
            {
                Console.WriteLine(i + ". " + m_options[i - 1].Title);
            }
            Console.WriteLine();
        }

        private class MenuOption
        {
            private string m_title;
            private eActionType eActionType;

            public string Title
            {
                get { return m_title; }
            }
            public eActionType ActionType
            {
                get { return eActionType; }
            }
            public MenuOption(string i_title, eActionType i_ActionType)
            {
                m_title = i_title;
                eActionType = i_ActionType;
            }
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
