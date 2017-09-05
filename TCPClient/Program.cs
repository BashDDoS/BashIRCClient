using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;


namespace TCPClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Connection con = new Connection("46.101.137.45");
            while (con.connected)
            {
                Console.WriteLine("Enter Message:");
                String msg = MessageHandler.ParseMessage(Console.ReadLine());
                if (!string.IsNullOrEmpty(msg))
                {
                    con.SendMessage(msg);
                }
            }
            
        } 
    }
    
    public partial class Connection
    {
        public string serverIP;

        public byte[] pKey;

        public byte[] sharedKey;

        public bool connected = false;

        public bool CheckConnection()
        {
            try
            {
                TcpClient client = new TcpClient(serverIP, 845);
                client.Close();
                return true;
            }
            catch
            {
                Console.WriteLine("Error connecting to host:'" + serverIP + ":845" + "'");
                return false;
            }
        }
        
        public bool CheckConnection(string ip)
        {
            try
            {
                TcpClient client = new TcpClient(ip, 845);
                client.Close();
                return true;
            }
            catch
            {
                Console.WriteLine("Error connecting to host:'" + ip + ":845" + "'");
                return false;
            }
        }

        public Connection(string ip)
        {
            serverIP = ip;
            connected = CheckConnection(ip);
        }

        public void SendMessage(string message)
        {
            if (!connected)
            {
                Console.WriteLine("Not Connected, sent nothing");
                return;
            }
            try
            {
                
                Int32 port = 845;
                TcpClient client = new TcpClient(serverIP, port);
                
                Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);
                
                
                
                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                data = new byte[256];


                Int32 bytes = stream.Read(data, 0, data.Length);
                
                
                string responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
            
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
        }

        public void AuthenticateWithServer(string uName, string pw)
        {
            this.SendMessage("authenticate|"+uName+":"+pw);
        } 
    }

    public static class MessageHandler
    {
        public static string ParseMessage(string msg)
        {
            string finished = "";

            if (msg[0] == '!')
            {
                finished = "cmd|{";
                for (int i = 1; i < msg.Length; i++)
                {
                    finished += msg[i];
                    finished += "}";
                }
            }
            else
            {
                finished = "msg|{"+"(aut key)" +","+ msg +"}";
                
            }
            return finished;
        }
    }
}

