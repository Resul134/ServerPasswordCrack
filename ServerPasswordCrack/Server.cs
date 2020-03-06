using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Client.model;

namespace ServerPasswordCrack
{
    public class Server
    {

        private static int count = 0; 
        private List<UserInfo> userInfos;

        public void Start(int port)
        {
            TcpListener socket = new TcpListener(IPAddress.Parse("192.168.24.136"), port);
            userInfos = ReadPasswordFile("passwords.txt");
            


            try
            {
                socket.Start();
                Console.WriteLine("Server is listening");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            while (true)
            {
                TcpClient client = socket.AcceptTcpClient();

                
                   
            
                Task.Run(() =>
                {

                    
                    TcpClient tempsocket = client;
                    DoClient(tempsocket);

                    
                });

            }



        }


        public static List<UserInfo> ReadPasswordFile(String filename)
        {
            List<UserInfo> result = new List<UserInfo>();

            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (StreamReader sr = new StreamReader(fs))
            {

                while (!sr.EndOfStream)
                {
                    String line = sr.ReadLine();
                    String[] parts = line.Split(":".ToCharArray());
                    UserInfo userInfo = new UserInfo(parts[0], parts[1]);
                    result.Add(userInfo);
                }
                return result;
            }
        }


        public async void DoClient(TcpClient tempsocket)
        {

            using (StreamReader reader = new StreamReader(tempsocket.GetStream()))
            using (StreamWriter writer = new StreamWriter(tempsocket.GetStream()))
            {

                Stopwatch watch = Stopwatch.StartNew();

                TimeSpan passedTime = watch.Elapsed;


                while (true)
                {
                    string hello = reader.ReadLine();
                    Console.WriteLine(hello);
                    if (hello != null && hello == "Hej")
                    {
                        

                        

                        writer.WriteLine(userInfos[count].ToString());
                        writer.Flush();

                        count++;

                        string dznuts = await reader.ReadLineAsync();

                        Console.WriteLine(dznuts);


                        Thread.Sleep(100);
                        Console.WriteLine(passedTime);

                    }

                    


                }





            }
        }
    }
}

    
