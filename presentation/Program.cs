using System;
using System.IO;

namespace sulmar_redis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine(File.ReadAllText("Intro.txt"));

        }
    }
}
