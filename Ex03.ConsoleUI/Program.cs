using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class Program
    {
        
        public static void Main()
        {
            foreach(VehicleFactory.eType type in VehicleFactory.GetTypes())
            {
                Console.WriteLine(type);
            }


            Vehicle v1 = VehicleFactory.Create(
                (VehicleFactory.eType)VehicleFactory.GetTypes().GetValue(0),
                "1234",
                "BMW");




            //GarageUI garageUI = new GarageUI();
            //garageUI.run();

            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }
    }
}
