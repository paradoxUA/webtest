using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web.Services.Description;
using WebSocketSharp;
using WebSocketSharp.Server;


namespace Rentix
{
    class MultiServer
    {
        public static TcpListener serverSocket = new TcpListener(8888);
        public static TcpClient clientSocket = default(TcpClient);
        public static Socket socketWeb = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        public static WebSocketServer SocketServer = new WebSocketServer(System.Net.IPAddress.Any, 4649);

        public void startMultiServer()
        {
            Task.Factory.StartNew(startServer);
        }

        //private void startServer()
        //{

        //    int counter = 0;
        //   // serverSocket.LocalEndpoint
        //    serverSocket.Start();
        //    Console.WriteLine(" >> " + "Server Started"); 

        //    counter = 0;
        //    while (true)
        //    {
        //        counter += 1;
        //        clientSocket = serverSocket.AcceptTcpClient();
        //        Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
        //        handleClinet client = new handleClinet();
        //        client.startClient(clientSocket, Convert.ToString(counter));
        //    }

        //    clientSocket.Close();
        //    serverSocket.Stop();
        //    Console.WriteLine(" >> " + "exit");
        //    Console.ReadLine();
        //}
        private void startServer()
        {
            SocketServer = new WebSocketServer(System.Net.IPAddress.Any, 4649);
            //SocketServer.AddWebSocketService<Echo>("/Echo");
            SocketServer.AddWebSocketService<AnonserBroadcast>("/Anonser");

            SocketServer.Start();
            if (SocketServer.IsListening)
            {
                Console.WriteLine("Listening on port {0}, and providing WebSocket services:", SocketServer.Port);
                foreach (var path in SocketServer.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }


        }
    }
    public class AnonserBroadcast : WebSocketBehavior
    {
        private string _suffix;

        public AnonserBroadcast()
            : this(null)
        {
        }

        public AnonserBroadcast(string suffix)
        {
            _suffix = suffix ?? String.Empty;
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Sessions.Broadcast(e.Data + _suffix);
        }
    }

    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Send(e.Data);
        }
    }

    //Class to handle each client request separatly
    public class handleClinet
    {
        private TcpClient clientSocket;
        private string clNo;
        // public IPAddress ListenOn { get; set; }

        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }

        //private void doChat()
        //{
        //    int requestCount = 0;
        //    byte[] bytesFrom = new byte[64];
        //    string dataFromClient = null;
        //    Byte[] sendBytes = null;
        //    string serverResponse = null;
        //    string rCount = null;
        //    requestCount = 0;

        //    while ((true))
        //    {
        //        try
        //        {
        //            requestCount = requestCount + 1;
        //            NetworkStream networkStream = clientSocket.GetStream();
        //            networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
        //            dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
        //            dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
        //            Console.WriteLine(" >> " + "From client-" + clNo + dataFromClient);

        //            rCount = Convert.ToString(requestCount);
        //            serverResponse = "Server to clinet(" + clNo + ") " + rCount;
        //            sendBytes = Encoding.ASCII.GetBytes(serverResponse);
        //            networkStream.Write(sendBytes, 0, sendBytes.Length);
        //            networkStream.Flush();
        //            Console.WriteLine(" >> " + serverResponse);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(" >> " + ex.ToString());
        //        }
        //    }
        //}
        //private void doChat()
        //{
        //    NetworkStream stream = null;
        //    try
        //    {
        //        stream = clientSocket.GetStream();
        //        byte[] data = new byte[64]; // буфер для получаемых данных
        //        while (true)
        //        {
        //            // получаем сообщение
        //            StringBuilder builder = new StringBuilder();
        //            int bytes = 0;
        //            do
        //            {
        //                bytes = stream.Read(data, 0, data.Length);
        //                builder.Append(System.Text.Encoding.ASCII.GetString(data, 0, bytes));
        //            } while (stream.DataAvailable);

        //            string message = builder.ToString();
        //            if (message != "")
        //            {
        //                Console.WriteLine(message);
        //            message = message.Substring(message.IndexOf(':') + 1).Trim().ToUpper();
        //            data = Encoding.Unicode.GetBytes(message);
        //            stream.Write(data, 0, data.Length);
        //            }
        //           // Console.WriteLine(message);
        //            // отправляем обратно сообщение в верхнем регистре
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        if (stream != null)
        //            stream.Close();
        //        if (clientSocket != null)
        //            clientSocket.Close();
        //    }
        //}
        private void doChat()
        {
            //ListenOn = aListenOn ?? IPAddress.Loopback; Port = aPort;
            MultiServer.socketWeb = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 8181);

            MultiServer.socketWeb.Bind(endPoint);

            MultiServer.socketWeb.Listen(10);

            string guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

            while (true)
            {
                try
                {
                    //Log.Add(" -1- ");

                    Socket clientWeb = MultiServer.socketWeb.Accept();

                    //Log.Add(" -2- ");

                    NetworkStream stream = new NetworkStream(clientWeb);
                    StreamWriter streamWriter = new StreamWriter(stream);
                    StreamReader streamReader = new StreamReader(stream);

                    //Log.Add(" -3- ");

                    string readed = "";
                    string key = "";
                    while (!string.IsNullOrEmpty(readed = streamReader.ReadLine()))
                    {
                        //Log.Add("< " + readed);
                        if (readed.Length > 20 && readed.Substring(0, 19) == "Sec-WebSocket-Key: ")
                        {
                            key = readed.Substring(19);
                        }
                    }

                    SHA1 sha = new SHA1CryptoServiceProvider();
                    byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(key + guid));
                    string acceptKey = Convert.ToBase64String(hash);

                    //Log.Add(" -4- ");

                    List<string> handshakeLines = new List<string>();

                    handshakeLines.Add("HTTP/1.1 101 Switching Protocols");
                    handshakeLines.Add("Upgrade: websocket");
                    handshakeLines.Add("Connection: Upgrade");
                    handshakeLines.Add("Sec-WebSocket-Accept: " + acceptKey);

                    foreach (string line in handshakeLines)
                    {
                        //Log.Add("> " + line);
                        streamWriter.WriteLine(line);
                    }

                    //Log.Add(" -5- ");
                }
                catch (Exception ex)
                {
                    //Log.Add(ex.ToString());
                }
            }
        }
    }


}
