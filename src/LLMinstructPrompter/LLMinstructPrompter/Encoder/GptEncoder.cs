using LLMinstructPrompter.Abstract.Encoder;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Linq;

namespace LLMinstructPrompter.Encoder
{
    public class GptEncoder : IGptEncoder
    {
        const string pattern = @"'s|'t|'re|'ve|'m|'ll|'d| ?\p{L}+| ?\p{N}+| ?[^\s\p{L}\p{N}]+|\s+(?!\S)|\s+";

        private static readonly Dictionary<string, int> encoderCorpus;
        private static readonly Dictionary<string, int> bpeCorpus;

        private readonly Dictionary<int,char> encodeByte = new Dictionary<int,char>();
        private readonly Dictionary<char, int> decodeByte = new Dictionary<char, int>();

        static GptEncoder()
        {
            encoderCorpus = ReadEncoderFile(@"LangFiles/encoder.json");
        }

        public GptEncoder()
        {
            encodeByte = BytesToUnicode();
            foreach (var enc in encodeByte)
            {
                decodeByte.Add(enc.Value, enc.Key);
            }
        }

        public static Dictionary<string,int> ReadEncoderFile(string filepath)
        {
            var dataRaw = File.ReadAllText(filepath);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string,int>>(dataRaw);
            return dictionary;
        }

        public string Decode(int[] encodedData)
        {
            throw new NotImplementedException();
        }

        static Dictionary<int, char> BytesToUnicode()
        {
            List<int> bs = Enumerable.Range((int)'!', (int)'~' - (int)'!' + 1)
                .Concat(Enumerable.Range((int)'¡', (int)'¬' - (int)'¡' + 1))
                .Concat(Enumerable.Range((int)'®', (int)'ÿ' - (int)'®' + 1))
                .ToList();

            List<int> cs = new List<int>(bs);
            int n = 0;
            for (int b = 0; b < Math.Pow(2, 8); b++)
            {
                if (!bs.Contains(b))
                {
                    bs.Add(b);
                    cs.Add((int)Math.Pow(2, 8) + n);
                    n++;
                }
            }

            var cs2 = cs.Select(x => (char)x).ToList();

            Dictionary<int, char> result = bs.Select(( i) => new { b = bs[i], c = cs2[i] })
                .ToDictionary(pair => pair.b, pair => pair.c);

            return result;
        }

        public int[] Encode(string text)
        {
            List<int> bpeTokens = new List<int>();
            List<int> encoding = new List<int>();
            var regexMatches = Regex.Matches(text, pattern);
            foreach (Match regexMatch in regexMatches)
            {
                byte[] encodedBytes = Encoding.UTF8.GetBytes(regexMatch.Value);
                var encodedText = string.Join("",encodedBytes.Select(x => encodeByte[x]).ToArray());
            }
            throw new NotImplementedException();
            return bpeTokens.ToArray();
        }
    }
}
