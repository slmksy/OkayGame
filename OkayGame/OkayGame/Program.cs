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
            gameController.WriteStones(0);
        }
    }
}
