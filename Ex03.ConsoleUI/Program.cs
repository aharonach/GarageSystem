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
                    valueToReturn = GetValueOfString(i_FieldType);
                    break;

                case "Int32":
                    valueToReturn = GetValueOfInt(i_FieldType);
                    break;

                case "Boolean":
                    valueToReturn = GetValueOfBool(i_FieldType);
                    break;

                case "Single":
                    valueToReturn = GetValueOfFloat(i_FieldType);
                    break;

                case "Enum":
                    valueToReturn = GetValueOfEnum(i_FieldType);
                    break;
            }

            return valueToReturn;
        }

        private static object GetValueOfBool(Type i_FieldType)
        {
            throw new NotImplementedException();
        }

        private static object GetValueOfEnum(Type i_FieldType)
        {
            throw new NotImplementedException();
        }

        private static object GetValueOfFloat(Type i_FieldType)
        {
            throw new NotImplementedException();
        }

        private static object GetValueOfInt(Type i_FieldType)
        {
            throw new NotImplementedException();
        }

        private static object GetValueOfString(Type i_FieldType)
        {
            throw new NotImplementedException();
        }
    }
}
