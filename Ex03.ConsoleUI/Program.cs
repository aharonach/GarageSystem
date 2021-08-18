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
