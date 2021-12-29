using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MyAgarIoServer
{
    class Server
    {
        public static readonly string IP = "127.0.0.1";
        public static readonly int PORT = 8080;

        public Socket Socket { get; }

        public Server()
        {
            EndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), PORT);
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Socket.Bind(endPoint);
        }

        public void Listen(object o)
        {
            GameController gameController = o as GameController;
            while (gameController.CurrentRound == null) ;

            while (true)
            {
                byte[] buffer = new byte[2048];
                StringBuilder data = new StringBuilder();
                EndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);

                try
                {
                    if (Socket.Available > 1)
                    {

                        int size = Socket.ReceiveFrom(buffer, ref clientEndPoint);
                        data.Append(Encoding.UTF8.GetString(buffer, 0, size));



                        Command command = Command.FromRequest(data.ToString());

                        //Console.WriteLine(data);
                        for (int i = 0; i < gameController.Players.Count; i++)
                        {
                            if (gameController.Players[i].EndPoint.Equals(clientEndPoint))
                            {
                                gameController.Players[i].LastRequestTimer.Restart();
                            }
                        }

                        switch (command.GetCommandType())
                        {
                            case Command.CREATE:
                                CreateCommand createCommand = (CreateCommand)command;
                                Player player = new Player(clientEndPoint, createCommand.Name, Goo.Create(createCommand.Name, gameController.CurrentRound.PetriCup));
                                if (!gameController.CurrentRound.Players.Contains(player))
                                {
                                    gameController.CurrentRound.Players.Add(player);
                                }
                                break;
                            case Command.MOVE:
                                MoveCommand moveCommand = (MoveCommand)command;
                                List<Player> players = gameController.CurrentRound.Players;
                                for (int i = 0; i < players.Count; i++)
                                {
                                    if (players[i].EndPoint.Equals(clientEndPoint))
                                    {
                                        players[i].Goo.CurrentMoveCommand = moveCommand;
                                        break;
                                    }
                                }
                                break;
                        }
                    }

                }
                catch (Exception ex)
                {
                    //Console.WriteLine(data);
                }
            }
        }

        public void Send(EndPoint endPoint, byte[] bytes)
        {
            Socket.SendTo(bytes, endPoint);
        }

        public void Stop()
        {
            Socket.Shutdown(SocketShutdown.Both);
            Socket.Close();
        }
    }
}
