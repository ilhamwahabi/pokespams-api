using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeSpams.Models
{
    public class Algorithm
    {
        //private string accessToken = "985637925676204032-oZYIX5y0ARwvWEYsj91AwnYUjoe9ISR";
        //private string accessTokenSecret = "qt6tgqWWUsufyCqHREhTdGkS726i1DCVEsULjRXxAmKyP";
        //private string consumerKey = "CMF9kqcVcmESFSB5wxKDd3ifZ";
        //private string consumerKeySecret = " vif3dOiwCSeQPowWvdY1JYN51ciTJrppJ4CeifeIjyyZrJjiYl";

        public List<List<string>> KMP(List<string> pattern)
        {

            var result = new List<List<string>> { };

            return result;
        }

        public List<List<string>> BooyerMoore(List<string> patterns)
        {
            List<List<String>> result = new List<List<String>> { };

            var inspectWord = new List<String>
            {
                "Dijual buku seharga 2000",
                "Test drive segera ditempat anda",
                "Hari yang cerah di pantai",
                "tes",
                "Dijual test",
                "bisa aku test?",
                "atest",
                "Tidak ada tempat yang lebin indah daripada tempat yang sudah dite",
                "Tidak ada tempat yang lebih indah selain tempat ini"
            };
            
            // Iterasi setiap kalimat
            foreach(var word in inspectWord)
            {
                // Tiap kalimat dicocokkan dengan pattern
                var patternResult = new List<String> { };

                foreach(var pattern in patterns)
                {
                    List<int> found = new List<int>(){ 0, 0 };
                    var pointer = pattern.Count() - 1;

                    // Pencarian Booyer-Moore pada sepanjang kalimat
                    while (
                        found[0].Equals(0) && 
                        pointer <= (word.Count() - 1) && 
                        word.Count() >= pattern.Count())
                    {
                        found = checkWord(
                                    pattern.ToLower(),
                                    word.Substring(
                                        pointer - (pattern.Count() - 1),
                                        pattern.Count()
                                    ).ToLower()
                                );

                        // Jika tidak ditemukan pattern itu didalam kalimat
                        if (found[0].Equals(0))
                        {
                            pointer += found[1];
                        }
                    }

                    if (found[0].Equals(1))
                    {
                        if (patternResult.IndexOf(word) == -1)
                        {
                            patternResult.Add(word);
                        }
                        patternResult.Add(pattern);
                    }
                }

                if (patternResult.IndexOf(word) == 0)
                {
                    result.Add(patternResult);
                }
            }
            
            return result;
        }

        public List<int> checkWord(String pattern, String word)
        {
            var equals = true;
            var stepSize = 0;

            if (pattern.Last() == word.Last())
            {
                var i = 1;
                while (equals && i <= (pattern.Count() - 1))
                {
                    if (
                        pattern.ElementAt((pattern.Count() - 1) - i)
                            .Equals(word.ElementAt((pattern.Count() - 1) - i)))
                    {
                        i++;
                    } else
                    {
                        equals = false;
                        stepSize = i;
                    }
                }
            }
            else
            {
                stepSize = pattern.Count() - (pattern.IndexOf(word.Last()) == -1 ? 0 : pattern.IndexOf(word.Last()) + 1);
                equals = false;
            }

            return new List<int> { equals ? 1 : 0, stepSize };
        }

        public List<List<string>> Regex(List<string> pattern)
        {
            var result = new List<List<string>> { };

            return result;
        }
    }
}