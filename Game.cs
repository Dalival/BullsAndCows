using System;
using System.Collections.Generic;

namespace BikyAndKorovy
{
    public class Game
    {
        public List<int> ComputerNumbers { get; set; }

        public List<int> HumanNumbers { get; set; }

        public List<Turn> ComputerTurns { get; set; }

        public List<Turn> HumanTurns { get; set; }


        public Game()
        {
            ComputerTurns = new List<Turn>();
            HumanTurns = new List<Turn>();
        }


        public void StartTheGame()
        {
            ComputerNumbers = GenerateNumber();
            HumanNumbers = GetUserInputNumber();
        }


        private List<int> GenerateNumber()
        {
            var targetNumber = new List<int>();
            var allowedSymbols = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            var random = new Random(826);

            var allowedSymbolsAmount = 10;
            for (var i = 0; i < 4; i++)
            {
                var randomIterator = random.Next(0, allowedSymbolsAmount - 1);
                var randomNumber = allowedSymbols[randomIterator];
                targetNumber.Add(randomNumber);
                allowedSymbols.Remove(randomNumber);
                allowedSymbolsAmount--;
            }

            return targetNumber;
        }

        private List<int> GetUserInputNumber()
        {
            while (true)
            {
                Console.WriteLine("Введите четырёхзначное число. Каждую цифру можно использовать только один раз.\n");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out var result))
                {
                    Console.WriteLine("Неверный ввод. Попробуйте еще раз.\n");
                    continue;
                }

                if (!CheckIsNumberValid(result))
                {
                    Console.WriteLine("Недопустимое число. Попробуйте еще раз.\n");
                    continue;
                }

                var numberString = result.ToString();
                Console.WriteLine("Замечательно. Ваше число: " + numberString);

                var humanNumber = new List<int>
                {
                    numberString[0],
                    numberString[1],
                    numberString[2],
                    numberString[3]
                };

                return humanNumber;
            }
        }

        private bool CheckIsNumberValid(int number)
        {
            if (number is < 123 or > 9876)
            {
                return false;
            }

            var numberString = number.ToString();

            var firstSymbol = numberString[0];
            var secondSymbol = numberString[1];
            var thirdSymbol = numberString[2];
            var forthSymbol = numberString[3];

            if (firstSymbol == secondSymbol
                || firstSymbol == thirdSymbol
                || firstSymbol == forthSymbol
                || secondSymbol == thirdSymbol
                || secondSymbol == forthSymbol
                || thirdSymbol == forthSymbol)
            {
                return false;
            }

            return true;
        }
    }
}
