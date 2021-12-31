using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCows
{
    public class Number
    {
        public List<int> Digits { get; }


        public Number(int number)
        {
            if (!IsIntAllowed(number))
            {
                throw new ArgumentException("Number must contain four different digits.");
            }

            Digits = new List<int>
            {
                number / 1000,
                number % 1000 / 100,
                number % 100 / 10,
                number % 10
            };
        }


        private Number(List<int> digits)
        {
            Digits = digits;
        }


        public static Number CreateRandomNumber(Random random)
        {
            var digits = new List<int>();
            var allowedSymbols = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

            for (var i = 0; i < 4; i++)
            {
                var index = random.Next(0, allowedSymbols.Count);
                var digit = allowedSymbols[index];
                digits.Add(digit);
                allowedSymbols.Remove(digit);
            }

            return new Number(digits);
        }

        public static bool IsIntAllowed(int number)
        {
            if (number is < 123 or > 9876)
            {
                return false;
            }

            var digitsList = new List<int>
            {
                number / 1000,
                number % 1000 / 100,
                number % 100 / 10,
                number % 10
            };

            var uniqDigitsCount = digitsList.Distinct().Count();

            return uniqDigitsCount == digitsList.Count;
        }


        public override string ToString() => $"{Digits[0]}{Digits[1]}{Digits[2]}{Digits[3]}";

        public override bool Equals(object o) => o is Number other && Equals(other);

        public override int GetHashCode() => Digits != null
            ? HashCode.Combine(Digits[0], Digits[1], Digits[2], Digits[3])
            : 0;


        private bool Equals(Number other) => Digits[0] == other.Digits[0]
                                             && Digits[1] == other.Digits[1]
                                             && Digits[2] == other.Digits[2]
                                             && Digits[3] == other.Digits[3];
    }
}
