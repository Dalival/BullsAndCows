using System.Text;

namespace BullsAndCows
{
    public class Attempt
    {
        public Number Number { get; set; }

        public int Bulls { get; set; }

        public int Cows { get; set; }


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

            if (Bulls != 0)
            {
                str.Append(Bulls + "B ");
            }

            if (Cows != 0)
            {
                str.Append(Cows + "C");
            }

            if (Bulls == 0 && Cows == 0)
            {
                str.Append("nothing");
            }

            return str.ToString();
        }
    }
}
