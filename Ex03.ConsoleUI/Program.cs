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
            GarageUI ui = new GarageUI();
            //ui.run();

            //Dictionary<string, string> test = new Dictionary<string, string>();

            //test.Add("1", "2");

            //Console.WriteLine(test.ContainsKey("1"));

            Garage garage = new Garage();
            garage.AddVehicle("gal", "0542832447", VehicleFactory.eType.ElectricCar, "123", "Tesla");

            // To update
            Dictionary<string, KeyValuePair<string, Type>> fieldsToUpdate;
            Dictionary<string, object> additionalFields;

            fieldsToUpdate = garage.GetVehicleFieldsToUpdate("123", Garage.eFieldGroup.Additional);
            additionalFields = new Dictionary<string, object>
                                                          {
                                                              { "Color", 1 },
                                                              { "NumOfDoors", 4 }
                                                          };
            garage.UpdateVehicleFields("123", additionalFields, Garage.eFieldGroup.Additional);


            fieldsToUpdate = garage.GetVehicleFieldsToUpdate("123", Garage.eFieldGroup.Wheel);
            additionalFields = new Dictionary<string, object>
                                                          {
                                                              { "Manufacturer", "Mishlen1231" },
                                                              { "AirPressure", 19 }
                                                          };
            garage.UpdateVehicleFields("123", additionalFields, Garage.eFieldGroup.Wheel);


            fieldsToUpdate = garage.GetVehicleFieldsToUpdate("123", Garage.eFieldGroup.Tank);
            additionalFields = new Dictionary<string, object>
                               {
                                   { "BatteryTime", 2 }
                               };
            garage.UpdateVehicleFields("123", additionalFields, Garage.eFieldGroup.Tank);

            Dictionary<string, object> res = garage.GetVehicleFieldsAndValues("123");

            Console.WriteLine(res.ToString());


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
