using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    internal static class InputUtils
    {
        public static int GetNumberFromUser()
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
                numberFromUser = GetNumberFromUser();

                if (!isNumberInRange(numberFromUser, i_Min, i_Max))
                {
                    Console.WriteLine($"Input must be between {i_Min} and {i_Max}.");
                    inputIsValid = false;
                }
            }
            while (!inputIsValid);

            return numberFromUser;
        }

        public static bool GetYesOrNoFromUser()
        {
            bool validInput;
            bool answer = false;

            Console.Write("(y - Yes, n - No): ");

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

        public static Garage.eVehicleStatus ChooseVehicleStatus()
        {
            Console.WriteLine("Choose status: ");
            return (Garage.eVehicleStatus) GetValueOfEnum(typeof(Garage.eVehicleStatus));
        }

        public static string GetLicenseNumberFromUser(int minLength, int maxLength)
        {
            bool inputIsValid;
            string stringFromUser;

            Console.WriteLine("Enter license number: ");

            do
            {
                inputIsValid = true;

                stringFromUser = Console.ReadLine();
                bool allCharsAreDigits = stringFromUser.All(Char.IsDigit);
                bool lengthIsCorrect = isNumberInRange(stringFromUser.Length, minLength, maxLength);

                inputIsValid = allCharsAreDigits && lengthIsCorrect;

                if (!inputIsValid)
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            while (!inputIsValid);

            return stringFromUser;
        }

        public static object GetParameterByType(Type i_FieldType)
        {
            string typeName = i_FieldType.Name;
            object valueToReturn = null;

            if(i_FieldType.IsEnum)
            {
                valueToReturn = GetValueOfEnum(i_FieldType);
            }
            else
            {
                switch (typeName)
                {
                    case "String":
                        valueToReturn = GetValueOfString();
                        break;

                    case "Int32":
                        valueToReturn = GetNumberFromUser();
                        break;

                    case "Boolean":
                        valueToReturn = GetYesOrNoFromUser();
                        break;

                    case "Single":
                        valueToReturn = GetValueOfFloat();
                        break;
                }
            }

            return valueToReturn;
        }

        public static int GetValueOfEnum(Type i_EnumType)
        {
            Array enumValues = Enum.GetValues(i_EnumType);
            string[] enumNames = i_EnumType.GetEnumNames();

            for (int i = 1; i <= enumValues.Length; i++)
            {
                Console.WriteLine(@"{0}. {1}", i, enumNames[i - 1]);
            }

            int enumValueFromUser = GetNumberFromUserInRange(1, enumValues.Length);

            return (int)enumValues.GetValue(enumValueFromUser - 1);
        }

        public static float GetValueOfFloat()
        {
            float valueToReturn;
            bool validInput;

            do
            {
                string userInput = Console.ReadLine();
                validInput = float.TryParse(userInput, out valueToReturn);
                if (!validInput)
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            while (!validInput);

            return valueToReturn;
        }

        public static string GetValueOfString()
        {
            string userInput;
            bool validInput;

            do
            {
                userInput = Console.ReadLine();
                validInput = userInput != null && !userInput.Equals(string.Empty);
                if (!validInput)
                {
                    Console.WriteLine("Invalid Input.");
                }
            }
            while (!validInput);

            return userInput;
        }

        private static bool isNumberInRange(int i_Number, int i_Min, int i_Max)
        {
            return i_Min <= i_Number && i_Number <= i_Max;
        }
    }
}
