using System;
using System.Collections.Generic;
using System.Linq;

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


        public void StartUserAttempt(Number number)
        {
            var attempt = MakeAttempt(number, ComputerNumber);
            UserAttempts.Add(attempt);
        }

        public void StartComputerAttempt()
        {
            UpdatePossibleVariants();

            var number = GuessNumber();
            var attempt = MakeAttempt(number, UserNumber);
            ComputerAttempts.Add(attempt);
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

        private Number GuessNumber()
        {
            var index = _random.Next(0, PossibleVariants.Count);
            var number = PossibleVariants[index];

            return number;
        }

        private void UpdatePossibleVariants()
        {
            var lastAttempt = ComputerAttempts.LastOrDefault();
            if (lastAttempt == null)
            {
                return;
            }

            var numbersToRemove = PossibleVariants.Where(number =>
                !Equals(lastAttempt, MakeAttempt(lastAttempt.Number, number)));

            PossibleVariants = PossibleVariants.Except(numbersToRemove).ToList();
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
    }
}
