using System;

namespace ServerPasswordCrack
{
    class Program
    {
        static void Main(string[] args)
        {
            Server serv = new Server();

            serv.Start(4663);
        }
    }
}
