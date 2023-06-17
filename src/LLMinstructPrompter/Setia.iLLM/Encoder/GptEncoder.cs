using Setia.iLLM.Abstract.Encoder;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net;

namespace Setia.iLLM.Encoder
{
    public class GptEncoder : IGptEncoder
    {
        const string pattern = @"'s|'t|'re|'ve|'m|'ll|'d| ?\p{L}+| ?\p{N}+| ?[^\s\p{L}\p{N}]+|\s+(?!\S)|\s+";

        private static readonly Dictionary<string, int> encoderCorpus;
        private static readonly Dictionary<string, int> bpeCorpus;

        private readonly Dictionary<int, char> encodeByte = new Dictionary<int, char>();
        private readonly Dictionary<char, int> decodeByte = new Dictionary<char, int>();
        private readonly Dictionary<string,int> bpeRank = new Dictionary<string, int>();

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

        public static Dictionary<string, int> ReadEncoderFile(string filepath)
        {
            var dataRaw = File.ReadAllText(filepath);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(dataRaw);
            return dictionary;
        }

        public static List<List<string>> ReadBpeFile(string filepath)
        {
            var lines = File.ReadAllText(filepath).Split("\n");
            List<List<string>> bpeMerges = lines
            .Skip(1)
            .Take(lines.Count() - 2)
            .Select(x => x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(e => e.Trim().Length > 0)
                .ToList())
            .ToList();

            return bpeMerges;

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

            Dictionary<int, char> result = bs.Select((i) => new { b = bs[i], c = cs2[i] })
                .ToDictionary(pair => pair.b, pair => pair.c);

            return result;
        }

        private List<char[]> GetPairs(char[] word)
        {
            var pairs = new List<char[]>();
            var prev_char = word[0];
            for (var i = 1; i < word.Length; i++)
            {
                var @char = word[i];
                pairs.Add(new char[] { prev_char, @char });
                prev_char = @char;
            }
            return pairs;
        }

        private string Bpe(string token)
        {
            var word = token.Split("").Select(c=>Convert.ToChar(c)).ToArray();
            var wordPairs = GetPairs(word);
            while (true)
            {

            }
            return string.Empty;
        }

        public int[] Encode(string text)
        {
            List<int> bpeTokens = new List<int>();
            List<int> encoding = new List<int>();
            var regexMatches = Regex.Matches(text, pattern);
            foreach (Match regexMatch in regexMatches)
            {
                byte[] encodedBytes = Encoding.UTF8.GetBytes(regexMatch.Value);
                var encodedText = string.Join("", encodedBytes.Select(x => encodeByte[x]).ToArray());
            }
            throw new NotImplementedException();
            return bpeTokens.ToArray();
        }
    }
}
