using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace BullsAndCows
{
    public class GameController
    {
        private readonly Game _game;


        public GameController(Game game)
        {
            _game = game;
        }


        public void Start()
        {
            var openingText =  File.OpenText("opening.txt").ReadToEnd();

            Console.WriteLine(openingText);
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Enter a four-digit number. Each digit can only be used once.");
            _game.UserNumber = GetUserInputNumber();
            Console.Clear();

            while (true)
            {
                Console.WriteLine("Your number: " + _game.UserNumber);
                Console.WriteLine("\nComputer's attempts:");
                WriteComputerAttempts();
                Console.WriteLine("\nYour attempts:");
                WriteUserAttempts();

                if (_game.ComputerAttempts.LastOrDefault()?.Cows == 4
                    || _game.UserAttempts.LastOrDefault()?.Cows == 4)
                {
                    break;
                }

                Console.WriteLine("\nYour turn. Enter a number:");
                var userNumber = GetUserInputNumber();
                _game.StartUserAttempt(userNumber);
                _game.StartComputerAttempt();
                Console.Write("\nComputer's turn. Choosing a number ");
                WriteLoadingDots();
                Console.Clear();
            }

            Console.WriteLine("\nThe game is finished. Press any key to exit.");
            Console.ReadKey();
        }


        private void WriteComputerAttempts()
        {
            if (_game.ComputerAttempts.Any())
            {
                foreach (var attempt in _game.ComputerAttempts)
                {
                    Console.WriteLine(attempt);
                }
            }
            else
            {
                Console.WriteLine("none");
            }
        }

        private void WriteUserAttempts()
        {
            if (_game.UserAttempts.Any())
            {
                foreach (var attempt in _game.UserAttempts)
                {
                    Console.WriteLine(attempt);
                }
            }
            else
            {
                Console.WriteLine("none");
            }
        }

        private static Number GetUserInputNumber()
        {
            while (true)
            {
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

        private static void WriteLoadingDots()
        {
            Thread.Sleep(700);
            Console.Write(".");
            Thread.Sleep(700);
            Console.Write(".");
            Thread.Sleep(700);
            Console.Write(".");
            Thread.Sleep(700);
        }
    }
}
