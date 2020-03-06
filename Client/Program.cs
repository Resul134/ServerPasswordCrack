using System;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker work = new Worker();
            work.Start();


            Task.Run(() => work.Start());
        }
    }
}
