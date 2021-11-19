using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCows
{
    public class Game
    {
        public Number ComputerNumber { get; set; }

        public Number UserNumber { get; set; }

        public List<Attempt> ComputerTurns { get; set; }

        public List<Attempt> UserTurns { get; set; }

        public List<int> ProbablyVariants { get; set; }


        public Game()
        {
            ComputerTurns = new List<Attempt>();
            UserTurns = new List<Attempt>();
            InitializeComputerNumber();
            InitializeProbablyVariants();
        }


        public void Start()
        {
            UserNumber = GetUserInputNumber();
            Console.WriteLine($"Ваше число: {UserNumber}");
            Console.WriteLine($"Число соперника: {ComputerNumber}");
            while (true)
            {
                UserTurns.Add(GiveUserTurn());

                foreach (var attempt in UserTurns)
                {
                    Console.WriteLine(attempt);
                }

                if (UserTurns.Last().Cows == 4)
                {
                    break;
                }
            }
        }


        private Number GetUserInputNumber()
        {
            while (true)
            {
                Console.WriteLine("Введите четырёхзначное число. Каждую цифру можно использовать только один раз.\n");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out var numberInt))
                {
                    Console.WriteLine("Неверный ввод. Попробуйте еще раз.\n");
                    continue;
                }

                if (!IsNumberValid(numberInt))
                {
                    Console.WriteLine("Недопустимое число. Попробуйте еще раз.\n");
                    continue;
                }

                var numberString = numberInt.ToString();
                if (numberInt < 1000)
                {
                    numberString = numberString.Insert(0, "0");
                }

                var number = new Number(
                    int.Parse(numberString[0].ToString()),
                    int.Parse(numberString[1].ToString()),
                    int.Parse(numberString[2].ToString()),
                    int.Parse(numberString[3].ToString()));

                return number;
            }
        }

        private void InitializeProbablyVariants()
        {
            var variants = new List<int>();

            for (var i = 0123; i < 9877; i++)
            {
                variants.Add(i);
            }

            ProbablyVariants = variants.Where(IsNumberValid).ToList();
        }

        private void InitializeComputerNumber()
        {
            var digits = new List<int>();
            var allowedSymbols = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var random = new Random();

            var allowedSymbolsCount = 10;
            for (var i = 0; i < 4; i++)
            {
                var index = random.Next(0, allowedSymbolsCount - 1);
                var digit = allowedSymbols[index];
                digits.Add(digit);
                allowedSymbols.Remove(digit);
                allowedSymbolsCount--;
            }

            ComputerNumber = new Number(digits);
        }

        private bool IsNumberValid(int number)
        {
            if (number is < 123 or > 9876)
            {
                return false;
            }

            var numberString = number.ToString();
            if (number < 1000)
            {
                numberString = numberString.Insert(0, "0");
            }

            var uniqDigitsCount = numberString.Distinct().Count();

            return uniqDigitsCount == numberString.Length;

            // var firstSymbol = numberString[0];
            // var secondSymbol = numberString[1];
            // var thirdSymbol = numberString[2];
            // var forthSymbol = numberString[3];
            //
            // return firstSymbol != secondSymbol
            //        && firstSymbol != thirdSymbol
            //        && firstSymbol != forthSymbol
            //        && secondSymbol != thirdSymbol
            //        && secondSymbol != forthSymbol
            //        && thirdSymbol != forthSymbol;
        }

        private Attempt GiveUserTurn()
        {
            var number = GetUserInputNumber();
            var bulls = 0;
            var cows = 0;
            foreach (var digit in number.Digits)
            {
                var indexInEnteredNumber = number.Digits.IndexOf(digit);
                var indexInComputerNumber = ComputerNumber.Digits.IndexOf(digit);
                if (indexInComputerNumber == indexInEnteredNumber)
                {
                    cows++;
                }
                else if (indexInComputerNumber != -1)
                {
                    bulls++;
                }
            }

            var attempt = new Attempt(number, bulls, cows);

            return attempt;
        }
    }
}
