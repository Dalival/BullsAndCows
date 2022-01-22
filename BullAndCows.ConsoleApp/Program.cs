using BullAndCows.ConsoleApp;
using BullsAndCows;

var game = new Game();
var controller = new GameController(game);
controller.Start();
