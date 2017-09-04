using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace TCPClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter Message:");
                String msg = Console.ReadLine();
                if (!string.IsNullOrEmpty(msg))
                {
                    Connections.Connect("169.254.247.82", msg);
                }
            }
            
        }
        
        
    }
    
    public static partial class Connections
    {
        public static void Connect(string server, string message)
        {
            try
            {
                
                Int32 port = 845;
                TcpClient client = new TcpClient(server, port);
                
                Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
                
                
                
                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                data = new byte[256];

                String responseData = String.Empty;

                Int32 bytes = stream.Read(data, 0, data.Length);

                responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);

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
        public static void AuthenticateWithServer(string server, string uName, string pw)
        {
            
        }
        
        public static void 
    }

    public static class Sessions
    {
        public static void CloseSession()
        {
            
        }
    }
}

