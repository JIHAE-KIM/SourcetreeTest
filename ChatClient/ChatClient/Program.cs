using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace ChatClient
{
    class Program
    {
        static Socket sck;
        //TcpClient socketForServer;

        static void Main(string[] args)
        {

            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1900);
            

            try
            {
                sck.Connect(localEndPoint);
            }
            catch
            {
                Console.Write("Unable to connect to remote end point!\r\n");
                Main(args);
            }
            //NetworkStream networkStream = sck.GetStream();
            //System.IO.StreamReader streamReader = new System.IO.StreamReader(networkStream);
            //System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(networkStream);
            
            while (true)
            {
                Console.Write("Enter Text: ");
                String text = Console.ReadLine();
                byte[] data = Encoding.UTF8.GetBytes(text);
                //byte[] data = Encoding.UTF8.GetBytes(text);
                
                //StreamReader sr = new StreamReader("C:/Users/kimjihae/Documents/Visual Studio 2015/Projects/ChatClient/RawData.csv", Encoding.GetEncoding("euc-kr"));
                
                /*while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    string[] temp = s.Split(',');        // Split() 메서드를 이용하여 ',' 구분하여 잘라냄
                    Console.Write("{0},{1},{2}", temp[0], temp[1], temp[2]);
                    string text = s;
                    byte[] data = Encoding.UTF8.GetBytes(text);
                    sck.Send(data);
                    Console.Write("Data Sent!\r\n");
                }*/
                sck.Send(data);
                Console.Write("Data Sent!\r\n");
                //Console.Write("Press any key To continue...");
                //Console.Read();
                Console.Write("[Waiting for response...]\r\n");
                byte[] bytes = new byte[1024];
                int revdata = sck.Receive(bytes);
                Console.WriteLine("The Server says : {0}",  Encoding.ASCII.GetString(bytes, 0, revdata));
            
            
            }
            sck.Close();
        }
    }
}