using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class Validations
    {
        public static bool IsNumeric(string i_Value)
        {
            return i_Value != null && i_Value.All(char.IsDigit);
        }

        public static bool IsAlphaNumeric(string i_Value)
        {
            return i_Value != null && i_Value.All(char.IsLetterOrDigit);
        }

        public static bool IsAlphabetic(string i_Value)
        {
            return i_Value != null && i_Value.All(char.IsLetter);
        }
    }
}
