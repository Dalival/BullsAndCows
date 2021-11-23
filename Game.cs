using System;
using System.Collections.Generic;
using System.Linq;

namespace BullsAndCows
{
    public class Game
    {
        public Number ComputerNumber { get; set; }

        public Number UserNumber { get; set; }

        public List<Attempt> ComputerAttempts { get; set; }

        public List<Attempt> UserAttempts { get; set; }

        public List<Number> ProbablyVariants { get; set; }


        public Game()
        {
            ComputerAttempts = new List<Attempt>();
            UserAttempts = new List<Attempt>();
            ComputerNumber = Number.CreateRandomNumber();
            InitializeProbablyVariants();
        }


        public void Start()
        {
            UserNumber = GetUserInputNumber();
            Console.WriteLine($"Ваше число: {UserNumber}");
            Console.WriteLine($"Число соперника: {ComputerNumber}");
            while (true)
            {
                var userNumber = GetUserInputNumber();
                StartUserAttempt(userNumber);
                StartComputerAttempt();

                foreach (var attempt in UserAttempts)
                {
                    Console.WriteLine(attempt);
                }

                if (UserAttempts.Last().Cows == 4)
                {
                    break;
                }
            }
        }


        private void InitializeProbablyVariants()
        {
            var variants = new List<int>();
            for (var i = 0123; i < 9877; i++)
            {
                variants.Add(i);
            }

            ProbablyVariants = variants
                .Where(Number.IsIntAllowed)
                .Select(x => new Number(x))
                .ToList();
        }

        private void StartUserAttempt(Number number)
        {
            var attempt = MakeAttempt(number, ComputerNumber);

            UserAttempts.Add(attempt);
        }

        private void StartComputerAttempt()
        {
            var number = GuessNumber();
            var attempt = MakeAttempt(number, UserNumber);

            ComputerAttempts.Add(attempt);
        }

        private Attempt MakeAttempt(Number attemptNumber, Number numberToGuess)
        {
            var bulls = 0;
            var cows = 0;
            foreach (var digit in attemptNumber.Digits)
            {
                var indexInEnteredNumber = attemptNumber.Digits.IndexOf(digit);
                var indexInComputerNumber = numberToGuess.Digits.IndexOf(digit);
                if (indexInComputerNumber == indexInEnteredNumber)
                {
                    cows++;
                }
                else if (indexInComputerNumber != -1)
                {
                    bulls++;
                }
            }

            var attempt = new Attempt(attemptNumber, bulls, cows);

            return attempt;
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

                if (!Number.IsIntAllowed(numberInt))
                {
                    Console.WriteLine("Недопустимое число. Попробуйте еще раз.\n");
                    continue;
                }

                var number = new Number(numberInt);

                return number;
            }
        }

        /* 1Б   2Б   3Б   4Б
         * 1К   2К   3К
         * 1Б 1К    1Б 2К
         * 2Б 1К    2Б 2К
         * 3Б 1К
         *
         * 1. Если nБ, убрать все варианты, где более n цифр данного числа. Убрать все цифро-позиции.
         * 2. Если nК, убрать все варианты, где нет хотя бы n цифро-позиций данного числа.
         */
        private Number GuessNumber()
        {
            throw new NotImplementedException();
        }
    }
}
