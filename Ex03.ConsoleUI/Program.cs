﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    class Program
    {
        
        public static void Main()
        {
            GarageUI garageUI = new GarageUI();
            garageUI.run();

            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }
    }
}
