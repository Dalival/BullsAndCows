namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var controller = new GameController(game);
            controller.Start();
        }
    }
}
