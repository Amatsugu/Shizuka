using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shizuka.Modules.Converse
{
    class SentimentResult
    {
		public string Message { get; private set; }
		public float Sentiment { get; private set; }
		public string[] Keywords { get; private set; }

		public SentimentResult(string message, float sentiment, string[] keywords)
		{
			Message = message;
			Sentiment = sentiment;
			Keywords = keywords;
		}
    }
}
