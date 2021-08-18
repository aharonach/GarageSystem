using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;
using System.Reflection;

namespace Ex03.ConsoleUI
{
    class Program
    {
        
        public static void Main()
        {
            Garage garage = new Garage();
            garage.AddVehicle("gal", "0542832447", VehicleFactory.eType.ElectricCar, "777777", "Tesla");

            //Dictionary<string, Type> fields = garage.GetVehicleFieldsToUpdate("777777");

            Dictionary<string, object> res = garage.GetVehicleFieldsAndValues("777777");





            ////foreach (VehicleFactory.eType type in VehicleFactory.GetAvailableTypes())
            ////{
            ////    Console.WriteLine(type);
            ////}


            ////Vehicle v1 = VehicleFactory.Create(
            ////    (VehicleFactory.eType)VehicleFactory.GetAvailableTypes().GetValue(0),
            ////    "1234",
            ////    "BMW");

            //// Logic asks this from vehicle
            //Dictionary<string, PropertyInfo> fieldsToUpdate = v1.GetFieldsToUpdate();

            //// UI Gets back a dictionary:
            //// key represents human name of the property
            //// value represents the type of the property
            //Dictionary<string, Type> fields = new Dictionary<string, Type>();

            //// UI should pass to the logic a dictionary:
            //// key represents the human name of the property
            //// value represents the value of the property from the user
            //Dictionary<string, object> valuesToReturn = new Dictionary<string, object>();

            //foreach(KeyValuePair<string, Type> kvp in fields)
            //{
            //    Console.WriteLine(@"Please enter {0}", kvp.Key);
            //    valuesToReturn.Add(kvp.Key, GetParameterByType(kvp.Value));
            //}

            //Dictionary<string, object> res = v1.GetFieldsWithValues();

            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }

        public static object GetParameterByType(Type i_FieldType)
        {
            string typeName = i_FieldType.Name;
            object valueToReturn = null;

            switch(typeName)
            {
                case "String":
                    valueToReturn = GetValueOfString();
                    break;

                case "Int32":
                    valueToReturn = GetValueOfInt();
                    break;

                case "Boolean":
                    valueToReturn = GetValueOfBool();
                    break;

                case "Single":
                    valueToReturn = GetValueOfFloat();
                    break;

                case "Enum":
                    valueToReturn = GetValueOfEnum(i_FieldType);
                    break;
            }

            return valueToReturn;
        }

        private static object GetValueOfBool()
        {
            bool validInput;
            bool valueToReturn = false;

            do
            {
                string stringFromUser = Console.ReadLine();

                validInput = true;
                stringFromUser = stringFromUser.ToLower();

                switch(stringFromUser)
                {
                    case "y":
                        valueToReturn = true;
                        break;

                    case "n":
                        valueToReturn = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input.");
                        validInput = false;
                        break;
                }
            }
            while (!validInput);

            return valueToReturn;
        }

        private static int GetValueOfEnum(Type i_EnumType)
        {
            Array enumValues = Enum.GetValues(i_EnumType);
            string[] enumNames = i_EnumType.GetEnumNames();

            for (int i = 1; i <= enumValues.Length; i++)
            {
                Console.WriteLine(@"{0}. {1}", i, enumNames[i - 1]);
            }

            int enumValueFromUser = GetNumberFromUserInRange(1, enumValues.Length);

            return (int) enumValues.GetValue(enumValueFromUser - 1);
        }

        private static float GetValueOfFloat()
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

        private static int GetValueOfInt()
        {
            int valueToReturn;
            bool validInput;

            do
            {
                string userInput = Console.ReadLine();
                validInput = int.TryParse(userInput, out valueToReturn);
                if (!validInput)
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            while (!validInput);

            return valueToReturn;
        }

        private static string GetValueOfString()
        {
            string userInput;
            bool validInput;

            do
            {
                userInput = Console.ReadLine();
                validInput = userInput != null && userInput.Equals(string.Empty);
                if (!validInput)
                {
                    Console.WriteLine("Invalid Input.");
                }
            }
            while (!validInput);

            return userInput;
        }

        public static int GetNumberFromUserInRange(int i_Min, int i_Max)
        {
            bool inputIsValid;
            int numberFromUser;

            do
            {
                inputIsValid = true;
                numberFromUser = GetValueOfInt();

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
    }
}
