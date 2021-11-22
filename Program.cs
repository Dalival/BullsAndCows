using System;

namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nThis is a test sample of the \"Bulls and Cows\" game." +
                              "\nPick a number and then try to guess the computer's number." +
                              "\nFor now computer will not try to guess your number." +
                              "\nIt skips it's turn every time. On this step it's important" +
                              "\nto teach it to answer your turns.\n");
            var game = new Game();
            game.Start();
        }
    }
}
