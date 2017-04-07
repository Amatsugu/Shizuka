using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shizuka.Modules.Converse
{
    public class WordModel
    {
		public string Word { get; private set; }
		public int Valence { get; private set; }
		
		public WordModel(string word, int valence)
		{
			Word = word;
			Valence = valence;
		}

		public static List<WordModel> LoadWords(string path) => JsonConvert.DeserializeObject<List<WordModel>>(File.ReadAllText(path));
    }
}
