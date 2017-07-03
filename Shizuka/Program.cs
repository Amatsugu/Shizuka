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
			string line;
			ulong channel = 200113511232307200;
			while ((line = Console.ReadLine()) != "")
			{
				if (line == "//end")
					break;
				if(line[0] == '/')
				{
					string[] data = line.Split(' ');
					if(data.Length >= 2)
					{
						if(data[0] == "/channel")
						{
							if (ulong.TryParse(data[1], out channel))
								Console.WriteLine("Channel Set");
							else
								Console.WriteLine("Parse fail");
						}
					}
				}else
				Shizuka.SendMessage(channel, line).GetAwaiter().GetResult();
			}
			Shizuka.Close().GetAwaiter().GetResult();
        }

		
    }
}