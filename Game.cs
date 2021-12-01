using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;

namespace BullsAndCows
{
    public class Game
    {
        private readonly Random _random;


        public Number ComputerNumber { get; }

        public Number UserNumber { get; set; }

        public List<Attempt> ComputerAttempts { get; }

        public List<Attempt> UserAttempts { get; }

        public List<Number> PossibleVariants { get; set; }


        public Game()
        {
            _random = new Random();

            ComputerAttempts = new List<Attempt>();
            UserAttempts = new List<Attempt>();
            ComputerNumber = Number.CreateRandomNumber(_random);
            InitializePossibleVariants();
        }


        public void Start()
        {
            UserNumber = GetUserInputNumber();
            while (true)
            {
                Thread.Sleep(1000);
                Console.Clear();
                Console.WriteLine($"Your number: {UserNumber}");
                var userNumber = GetUserInputNumber();
                StartUserAttempt(userNumber);
                StartComputerAttempt();

                Console.WriteLine("Computer's attempts:");
                foreach (var attempt in ComputerAttempts)
                {
                    Console.WriteLine(attempt);
                }

                Console.WriteLine("Your attempts:");
                foreach (var attempt in UserAttempts)
                {
                    Console.WriteLine(attempt);
                }

                if (ComputerAttempts.Last().Cows == 4
                    || UserAttempts.Last().Cows == 4)
                {
                    break;
                }
            }
        }


        private void InitializePossibleVariants()
        {
            var variants = new List<int>();
            for (var i = 123; i < 9877; i++)
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
            var index = _random.Next(0, PossibleVariants.Count);
            var number = PossibleVariants[index];

            return number;
        }

        // Must be called after each computer's attempt
        private void UpdatePossibleVariants()
        {
            var lastAttempt = ComputerAttempts.Last();
            var numbersToRemove = new List<Number>();
            foreach (var number in PossibleVariants)
            {
                var attempt = MakeAttempt(lastAttempt.Number, number);
                if (!lastAttempt.Equals(attempt))
                {
                    numbersToRemove.Add(number);
                }
            }

            PossibleVariants = PossibleVariants.Except(numbersToRemove).ToList();

            // PossibleVariants = PossibleVariants
            //     .Except(PossibleVariants
            //         .Select(number => MakeAttempt(lastAttempt.Number, number))
            //         .Where(attempt => !attempt.Equals(lastAttempt))
            //         .Select(attempt => attempt.Number))
            //     .ToList();
        }
    }
}
