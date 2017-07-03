using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Shizuka
{
    public static class LSTM
    {
		public static void Test()
		{
			int height = (int)Math.Pow(2, 20), width = 20;
			var rand = new Random();
			int[][] dataIn = new int[height][];
			for (int y = 0; y < height; y++)
			{
				dataIn[y] = new int[width];
				for (int x = 0; x < width; x++)
					dataIn[y][x] = rand.Next(2);
			}

			int[][] dataOut = new int[height][];
			for (int y = 0; y < height; y++)
			{
				dataOut[y] = new int[width + 1];
				dataOut[y][dataIn[y].Count(x => x == 1)] = 1;
			}

			int[][] trainIn = dataIn.Take(10_000).ToArray(), trainOut = dataOut.Take(10_000).ToArray();

			int[][] testIn = dataIn.Skip(10_000).ToArray(), testOut = dataOut.Skip(10_000).ToArray();



		}
	}
}
