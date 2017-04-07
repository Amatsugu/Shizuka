using Shizuka.Modules.Converse;
using System;
using System.IO;

namespace Shizuka
{
    class Program
    {
        static void Main(string[] args)
        {
			//LSTM.Test();
			Shizuka.Init().GetAwaiter().GetResult();
			Console.WriteLine("Running");
			Console.ReadLine();
			Shizuka.Close().GetAwaiter().GetResult();
        }

		
    }
}