using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal static class InputUtils
    {
        public static int GetNumberFromUserInRange(int i_Min, int i_Max)
        {
            int numberFromUser = getNumberFromUser();

            if (!isNumberInRange(numberFromUser, i_Min, i_Max))
            {
                throw new ValueOutOfRangeException("Number is not in range.", i_Min, i_Max);
            }

            return numberFromUser;
        }

        public static bool GetYesOrNoFromUser()
        {
            bool answer;

            Console.Write("(y - Yes, n - No): ");
            string stringFromUser = GetStringFromUser().ToLower();

            switch(stringFromUser)
            {
                case "y":
                    answer = true;
                    break;
                case "n":
                    answer = false;
                    break;
                default:
                    throw new FormatException("Invalid answer.");
            }

            return answer;
        }

        public static Garage.eVehicleStatus ChooseVehicleStatus()
        {
            Console.WriteLine("\nChoose status: ");
            return (Garage.eVehicleStatus) GetEnumValueFromUser(typeof(Garage.eVehicleStatus));
        }

        public static string GetLicenseNumberFromUser()
        {
            Console.WriteLine("Enter license number: ");
            string stringFromUser = GetStringFromUser();

            if (!Validations.IsNumeric(stringFromUser))
            {
                throw new FormatException("License number should contain only digits.");
            }

            return stringFromUser;
        }

        public static object GetParameterValueByType(Type i_FieldType)
        {
            string typeName = i_FieldType.Name;
            object valueToReturn = null;

            if(i_FieldType.IsEnum)
            {
                valueToReturn = GetEnumValueFromUser(i_FieldType);
            }
            else
            {
                switch (typeName)
                {
                    case "String":
                        valueToReturn = GetStringFromUser();
                        break;

                    case "Int32":
                        valueToReturn = getNumberFromUser();
                        break;

                    case "Boolean":
                        valueToReturn = GetYesOrNoFromUser();
                        break;

                    case "Single":
                        valueToReturn = GetFloatFromUser();
                        break;
                }
            }

            return valueToReturn;
        }

        public static int GetEnumValueFromUser(Type i_EnumType)
        {
            Array enumValues = Enum.GetValues(i_EnumType);
            string[] enumNames = i_EnumType.GetEnumNames();

            // Print enum values to the user to choose from
            for (int i = 1; i <= enumValues.Length; i++)
            {
                Console.WriteLine(@"{0}. {1}", i, enumNames[i - 1]);
            }

            Console.Write("Your choose: ");
            int enumValueFromUser = GetNumberFromUserInRange(1, enumValues.Length);

            return (int)enumValues.GetValue(enumValueFromUser - 1);
        }

        public static int GetChooseFromList(Dictionary<int, string> i_ListToChooseFrom)
        {
            foreach(KeyValuePair<int, string> keyValuePair in i_ListToChooseFrom)
            {
                Console.WriteLine(@"{0}. {1}", keyValuePair.Key, keyValuePair.Value);
            }

            Console.Write("Your choose: ");
            int chosenOption = getNumberFromUser();

            if(!i_ListToChooseFrom.ContainsKey(chosenOption))
            {
                throw new FormatException("Option doesn't exists.");
            }

            return chosenOption;
        }
        
        public static float GetFloatFromUser()
        {
            string userInput = GetStringFromUser();

            if (!float.TryParse(userInput, out float valueToReturn))
            {
                throw new FormatException("Invalid number");
            }

            return valueToReturn;
        }

        public static string GetStringFromUser()
        {
            string userInput = Console.ReadLine();

            if (!(userInput != null && !userInput.Equals(string.Empty)))
            {
                throw new FormatException("Invalid string input.");
            }
            
            return userInput;
        }

        private static int getNumberFromUser()
        {
            string stringFromUser = GetStringFromUser();

            if (!int.TryParse(stringFromUser, out int numberFromUser))
            {
                throw new FormatException("Invalid number input.");
            }

            return numberFromUser;
        }

        private static bool isNumberInRange(int i_Number, int i_Min, int i_Max)
        {
            return i_Min <= i_Number && i_Number <= i_Max;
        }
    }
}
