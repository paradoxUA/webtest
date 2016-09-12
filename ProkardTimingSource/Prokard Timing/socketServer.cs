using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

class socketServer
{
    internal void start()
    {
        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8281);

        server.Start();
        Console.WriteLine("Server has started on 127.0.0.1:8281.{0}Waiting for a connection..." + Environment.NewLine);

        TcpClient client = server.AcceptTcpClient();

        Console.WriteLine("A client connected.");

        NetworkStream stream = client.GetStream();

        //enter to an infinite cycle to be able to handle every change in stream
        while (true)
        {
            while (!stream.DataAvailable) ;

            Byte[] bytes = new Byte[client.Available];

            stream.Read(bytes, 0, bytes.Length);

            //translate bytes of request to string
            String request = Encoding.UTF8.GetString(bytes);

            if (new Regex("^GET").IsMatch(request))
            {
                Byte[] response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                    + "Connection: Upgrade" + Environment.NewLine
                    + "Upgrade: websocket" + Environment.NewLine
                    + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                        SHA1.Create().ComputeHash(
                            Encoding.UTF8.GetBytes(
                                new Regex("Sec-WebSocket-Key: (.*)").Match(request).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                            )
                        )
                    ) + Environment.NewLine
                    + Environment.NewLine);

                stream.Write(response, 0, response.Length);
            }
            else
            {
                Console.WriteLine(request);
                Byte[] response = Encoding.UTF8.GetBytes(" "+ "Data received");
                response[0] = 0x81; // denotes this is the final message and it is in text
               // response[1] = response.Length - 2; // payload size = message - header size
                stream.Write(response, 0, response.Length);
            }
        }
    }

    public void sendData(object arg)
    {
        
    }

    static int CountSpaces(string key)
    {
        return key.Length - key.Replace(" ", string.Empty).Length;
    }

    static string ReadLine(Stream stream)
    {
        var sb = new StringBuilder();
        var buffer = new List<byte>();
        while (true)
        {
            buffer.Add((byte)stream.ReadByte());
            var line = Encoding.ASCII.GetString(buffer.ToArray());
            if (line.EndsWith(Environment.NewLine))
            {
                return line.Substring(0, line.Length - 2);
            }
        }
    }

    static byte[] GetBigEndianBytes(int value)
    {
        var bytes = 4;
        var buffer = new byte[bytes];
        int num = bytes - 1;
        for (int i = 0; i < bytes; i++)
        {
            buffer[num - i] = (byte)(value & 0xffL);
            value = value >> 8;
        }
        return buffer;
    }

}