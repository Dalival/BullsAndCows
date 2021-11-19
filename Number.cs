using System.Collections.Generic;

namespace BullsAndCows
{
    public class Number
    {
        public List<int> Digits { get; set; }


        public Number(List<int> digits)
        {
            Digits = digits;
        }

        public Number(int first, int second, int third, int fourth)
        {
            Digits = new List<int> { first, second, third, fourth };
        }

        public override string ToString() => $"{Digits[0]}{Digits[1]}{Digits[2]}{Digits[3]}";
    }
}
