using System.Linq;

namespace Ex03.GarageLogic
{
    public static class Validations
    {
        public static bool IsNumeric(string i_Value)
        {
            return i_Value != null && i_Value.All(char.IsDigit);
        }

        public static bool IsInRange(int i_Number, int i_Min, int i_Max)
        {
            return i_Number >= i_Min && i_Number <= i_Max;
        }
    }
}
