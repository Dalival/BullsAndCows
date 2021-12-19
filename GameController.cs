using System;
using System.Linq;
using System.Threading;

namespace BullsAndCows
{
    public class GameController
    {
        private const string OpeningText = "The players each write a 4-digit secret number." +
                                           "\nThe digits must be all different. Then, in turn, the players try to" +
                                           "\nguess their opponent's number who gives the number of matches. If the" +
                                           "\nmatching digits are in their right positions, they are cows, if in" +
                                           "\ndifferent positions, they are bulls. Example:" +
                                           "\n" +
                                           "\nSecret number: 4271" +
                                           "\nOpponent's try: 1234" +
                                           "\nAnswer: 1C 2B – one cow ('2') and two bulls ('1' and '4')" +
                                           "\n" +
                                           "\nPress any key to play the game";


        private readonly Game _game;


        public GameController(Game game)
        {
            _game = game;
        }


        public void Start()
        {
            Console.WriteLine(OpeningText);
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
