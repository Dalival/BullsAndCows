using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCows
{
    public class Game
    {
        public Number ComputerNumber { get; set; }

        public Number HumanNumber { get; set; }

        public List<Number> ComputerTurns { get; set; }

        public List<Number> HumanTurns { get; set; }

        public List<int> ProbablyVariants { get; set; }


        public Game()
        {
            ComputerTurns = new List<Number>();
            HumanTurns = new List<Number>();
            InitializeComputerNumber();
            InitializeProbablyVariants();
        }


        public void Start()
        {
            HumanNumber = GetUserInputNumber();
            Console.WriteLine($"Ваше число: {HumanNumber.Digits[0]}{HumanNumber.Digits[1]}{HumanNumber.Digits[2]}{HumanNumber.Digits[3]}");
            GiveUserTurn();
        }


        private Number GetUserInputNumber()
        {
            while (true)
            {
                Console.WriteLine("Введите четырёхзначное число. Каждую цифру можно использовать только один раз.\n");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out var userNumber))
                {
                    Console.WriteLine("Неверный ввод. Попробуйте еще раз.\n");
                    continue;
                }

                if (!IsNumberValid(userNumber))
                {
                    Console.WriteLine("Недопустимое число. Попробуйте еще раз.\n");
                    continue;
                }

                var numberString = userNumber.ToString();
                if (userNumber < 1000)
                {
                    numberString = numberString.Insert(0, "0");
                }

                var humanNumber = new Number(
                    int.Parse(numberString[0].ToString()),
                    int.Parse(numberString[1].ToString()),
                    int.Parse(numberString[2].ToString()),
                    int.Parse(numberString[3].ToString()));

                return humanNumber;
            }
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

        private void GiveUserTurn()
        {
        }
    }
}
