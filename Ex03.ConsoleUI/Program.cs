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
            foreach(VehicleFactory.eType type in VehicleFactory.GetAvailableTypes())
            {
                Console.WriteLine(type);
            }


            Vehicle v1 = VehicleFactory.Create(
                (VehicleFactory.eType)VehicleFactory.GetAvailableTypes().GetValue(0),
                "1234",
                "BMW");

            Dictionary<string, List<PropertyInfo>> fields = v1.GetAvailableProperties();


            Dictionary<string, Dictionary<string, object>> properties =
                new Dictionary<string, Dictionary<string, object>>();

            Dictionary<string, object> allfields = new Dictionary<string, object>();
            properties.Add("Fields", allfields);

            allfields.Add("Color", Car.eColor.White);

            v1.UpdateFields(properties);



            //GarageUI garageUI = new GarageUI();
            //garageUI.run();

            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }
    }
}
