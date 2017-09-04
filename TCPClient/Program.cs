﻿using System;
using System.Net.Sockets;
using System.Text;

namespace TCPClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Connection con = new Connection("169.254.247.82");
            while (true)
            {
                Console.WriteLine("Enter Message:");
                string msg = Console.ReadLine();
                if (!string.IsNullOrEmpty(msg))
                {
                    con.SendMessage(msg);
                }
            }
            
            // ReSharper disable once FunctionNeverReturns
        }
        
        
    }
    
    public class Connection
    {
        public string ServerIp;

        public Connection(string ip)
        {
            ServerIp = ip;
        }

        public void SendMessage(string message)
        {
            try
            {
                
                Int32 port = 845;
                TcpClient client = new TcpClient(ServerIp, port);
                
                Byte[] data = Encoding.UTF8.GetBytes(message);
                
                
                
                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                data = new byte[256];

                String responseData;

                Int32 bytes = stream.Read(data, 0, data.Length);

                responseData = Encoding.UTF8.GetString(data, 0, bytes);

                Console.WriteLine("Recieved: {0}", responseData);

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("RIP: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("EMIL FUCKEDE UP: {0}", e);
            }
            Console.WriteLine("End of Send");
        }
    }

    public static class Authentication
    {
        public static void AuthenticateWithServer(Connection con, string uName, string pw)
        {
            con.SendMessage("lol");
       }        
    }
}

