using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BullsAndCows
{
    public class Game
    {
        public Number ComputerNumber { get; set; }

        public Number UserNumber { get; set; }

        public List<Attempt> ComputerAttempts { get; set; }

        public List<Attempt> UserAttempts { get; set; }

        public List<Number> PossibleVariants { get; set; }


        public Game()
        {
            ComputerAttempts = new List<Attempt>();
            UserAttempts = new List<Attempt>();
            ComputerNumber = Number.CreateRandomNumber();
            InitializePossibleVariants();
        }


        public void Start()
        {
            UserNumber = GetUserInputNumber();
            Console.WriteLine($"Your number: {UserNumber}");
            Console.WriteLine($"PC number: {ComputerNumber}");
            while (true)
            {
                //var userNumber = GetUserInputNumber();
                //StartUserAttempt(userNumber);
                StartComputerAttempt();

                foreach (var attempt in ComputerAttempts)
                {
                    Console.WriteLine(attempt);
                }

                if (ComputerAttempts.Last().Cows == 4)
                {
                    break;
                }
            }
        }


        private void InitializePossibleVariants()
        {
            var variants = new List<int>();
            for (var i = 0123; i < 9877; i++)
            {
                variants.Add(i);
            }

            PossibleVariants = variants
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
            UpdatePossibleVariants();
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
                Console.WriteLine("Enter a four-digit number. Each digit can only be used once.");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out var numberInt))
                {
                    Console.WriteLine("Wrong input. Try again.");
                    continue;
                }

                if (!Number.IsIntAllowed(numberInt))
                {
                    Console.WriteLine("Invalid number. Try again.");
                    continue;
                }

                var number = new Number(numberInt);

                return number;
            }
        }

        private Number GuessNumber()
        {
            var random = new Random();
            var index = random.Next(0, PossibleVariants.Count);
            var number = PossibleVariants[index];

            return number;
        }

        // Must be called after each computer's attempt
        private void UpdatePossibleVariants()
        {
            var lastAttempt = ComputerAttempts.Last();

            // Убрать варианты, где более или менее чем Б+K цифр данного числа (покрывает вариант, когда у нас НИЧЕГО)
            PossibleVariants.RemoveAll(n => n.Digits.Intersect(lastAttempt.Number.Digits).Count() != lastAttempt.Bulls + lastAttempt.Cows);

            // Если толькo быки, то убрать все цифро-позиции
            if (lastAttempt.Cows == 0)
            {
                PossibleVariants.RemoveAll(number =>
                    number.Digits[0] == lastAttempt.Number.Digits[0]
                    || number.Digits[1] == lastAttempt.Number.Digits[1]
                    || number.Digits[2] == lastAttempt.Number.Digits[2]
                    || number.Digits[3] == lastAttempt.Number.Digits[3]);
            }
            // Если коровы есть, то убрать варианты, где нет хотя бы К цифро-позиций
            else
            {
                var numbersToRemove = new List<Number>();

                foreach (var number in PossibleVariants)
                {
                    var digitOnPositionMatches = 0;

                    for (var i = 0; i < 4; i++)
                    {
                        if (number.Digits[i] == lastAttempt.Number.Digits[i])
                        {
                            digitOnPositionMatches++;
                        }
                    }

                    if (digitOnPositionMatches < lastAttempt.Cows)
                    {
                        numbersToRemove.Add(number);
                    }
                }

                PossibleVariants = PossibleVariants.Except(numbersToRemove).ToList();
            }
        }
    }
}
