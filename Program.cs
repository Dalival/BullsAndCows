using System;

namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("On a sheet of paper, the players each write a 4-digit secret number." +
                              "\nThe digits must be all different. Then, in turn, the players try to" +
                              "\nguess their opponent's number who gives the number of matches. If the" +
                              "\nmatching digits are in their right positions, they are cows, if in" +
                              "\ndifferent positions, they are bulls. Example:" +
                              "\n\nSecret number: 4271" +
                              "\nOpponent's try: 1234" +
                              "\nAnswer: 1C 2B – one cow ('2') and two bulls ('1' and '4')" +
                              "\n\nPress any key to play the game");
            Console.ReadKey();
            Console.Clear();

            var game = new Game();
            game.Start();

            Console.ReadKey();
        }
    }
}
