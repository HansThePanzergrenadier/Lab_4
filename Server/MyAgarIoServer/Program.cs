using System.Threading;

namespace MyAgarIoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            GameController gameController = new GameController(server, 10000);
            new Thread(new ParameterizedThreadStart(server.Listen)).Start(gameController);

            while (true)
            {
                gameController.StartNewGame();
            }
        }
    }
}
