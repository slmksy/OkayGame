using System;

namespace OkayGame
{
    class Program
    {
        static void Main(string[] args)
        {
            StoneModel.Instance.CreateStones();
            GameController gameController = new GameController();
            gameController.CreateNewGame();

            var winnerId = gameController.GetWinnerPlayer();
            Console.WriteLine("\n\n --- WINNER PLAYER --- PLAYERID : " + winnerId + "\n\n\n");

        }
    }
}
