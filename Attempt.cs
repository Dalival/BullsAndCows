using System;
using System.Text;

namespace BullsAndCows
{
    public class Attempt
    {
        public Number Number { get; }

        public int Bulls { get; }

        public int Cows { get; }


        public Attempt(Number number, int bulls, int cows)
        {
            Number = number;
            Bulls = bulls;
            Cows = cows;
        }


        public override string ToString()
        {
            if (Cows == 4)
            {
                return "WIN!";
            }

            var str = new StringBuilder();
            str.Append($"{Number} – ");

            if (Cows != 0)
            {
                str.Append(Cows + "C ");
            }

            if (Bulls != 0)
            {
                str.Append(Bulls + "B");
            }

            if (Bulls == 0 && Cows == 0)
            {
                str.Append("nothing");
            }

            return str.ToString().Trim();
        }

        public override bool Equals(object? o)
        {
            return o is Attempt other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number, Bulls, Cows);
        }


        private bool Equals(Attempt other)
        {
            return Equals(Number, other.Number)
                   && Bulls == other.Bulls
                   && Cows == other.Cows;
        }
    }
}
