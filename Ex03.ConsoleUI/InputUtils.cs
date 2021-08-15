using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    class InputUtils
    {
        public static int getNumberFromUser()
        {
            bool successIntParse;
            int numberFromUser;

            do
            {
                string stringFromUser = Console.ReadLine();
                successIntParse = int.TryParse(stringFromUser, out numberFromUser);

                if (!successIntParse)
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            while (!successIntParse);

            return numberFromUser;
        }

        public static int GetNumberFromUserInRange(int i_Min, int i_Max)
        {
            bool inputIsValid = true;
            int numberFromUser;

            do
            {
                inputIsValid = true;
                numberFromUser = getNumberFromUser();

                if (!isNumberInRange(numberFromUser, i_Min, i_Max))
                {
                    Console.WriteLine($"Input must be between {i_Min} and {i_Max}.");
                    inputIsValid = false;
                }
            }
            while (!inputIsValid);

            return numberFromUser;
        }

        private static bool isNumberInRange(int i_Number, int i_Min, int i_Max)
        {
            return i_Min <= i_Number && i_Number <= i_Max;
        }

        public static bool GetYesOrNoFromUser()
        {
            bool validInput;
            bool answer = false;

            do
            {
                string stringFromUser = Console.ReadLine();

                validInput = true;
                stringFromUser = stringFromUser.ToLower();

                if (stringFromUser.Equals("y"))
                {
                    answer = true;
                }
                else if (stringFromUser.Equals("n"))
                {
                    answer = false;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                    validInput = false;
                }
            }
            while (!validInput);

            return answer;
        }
    }
}
