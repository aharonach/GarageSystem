using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(string i_Message, float i_MinValue, float i_MaxValue)
            : base(i_Message)
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }

        public float MinValue { get; }

        public float MaxValue { get; }
    }
}
