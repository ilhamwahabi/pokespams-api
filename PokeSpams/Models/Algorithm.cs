using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace PokeSpams.Models
{
    public class Algorithm
    {
        private static List<String> inspectWord = new List<String>
        {
            "Dijual buku seharga 2000",
            "Test drive segera ditempat anda",
            "Hari yang cerah di pantai",
            "Enaknya menjadi atesit pada hari ini",
            "Tidak ada tempat yang lebin indah daripada tempat yang sudah dites",
            "Tidak ada tempat yang lebih indah selain tempat ini"
        };

        ////////////////////////////////////////////////////
        ///////////////////// K M P ////////////////////////
        ////////////////////////////////////////////////////
        public static List<List<string>> Kmp(List<string> patterns)
        {
            var result = new List<List<string>> { };

            var inspectedText = Services.getTweets();

            foreach (var word in inspectedText)
            {
                var patternResult = new List<String> { };
                var n = word[0].Count();

                foreach (var pattern in patterns)
                {
                    var m = pattern.Count();
                    var found = false;
                    var border = Services.borderFunction(pattern);

                    var i = 0;
                    var j = 0;

                    while (i < n && !found)
                    {
                        if (pattern.ElementAt(j).Equals(word[0].ToLower().ElementAt(i)))
                        {
                            if (j == m - 1)
                            {
                                found = true;
                                if (patternResult.IndexOf(word[0]) == -1)
                                {
                                    patternResult.Add(word[0]);
                                    patternResult.Add(word[1]);
                                }
                                patternResult.Add(pattern);
                            }
                            i++;
                            j++;
                        } else if (j > 0)
                        {
                            j = border[j - 1];
                        }
                        else
                        {
                            i++;
                        }
                    }
                }

                if (patternResult.IndexOf(word[0]) == 0)
                    result.Add(patternResult);
            }

            return result;
        }

        /////////////////////////////////////////////////////////////
        //////////////////// B O O Y E R M O O R E //////////////////
        /////////////////////////////////////////////////////////////
        public static List<List<string>> BooyerMoore(List<string> patterns)
        {
            List<List<String>> result = new List<List<String>> { };

            var inspectedText = Services.getTweets();

            // Iterasi setiap kalimat
            foreach(var word in inspectedText)
            {
                var patternResult = new List<String> { };

                // Tiap kalimat dicocokkan dengan pattern
                foreach (var pattern in patterns)
                {
                    List<int> found = new List<int>(){ 0, 0 };
                    var pointer = pattern.Count() - 1;

                    // Pencarian Booyer-Moore pada sepanjang kalimat
                    while (
                        found[0].Equals(0) && 
                        pointer <= (word[0].Count() - 1) && 
                        word.Count() >= pattern.Count())
                    {
                        found = Services.checkWord(
                                    pattern.ToLower(),
                                    word[0].Substring(
                                        pointer - (pattern.Count() - 1),
                                        pattern.Count()
                                    ).ToLower()
                                );

                        // Jika masih belum ditemukan, lakukakan looking-glass
                        if (found[0].Equals(0))
                            pointer += found[1];
                    }

                    // Jika ditemukan, simpan katanya jika belum tersimpan serta simpan patternnya
                    if (found[0].Equals(1))
                    {
                        if (patternResult.IndexOf(word[0]) == -1)
                        {
                            patternResult.Add(word[0]);
                            patternResult.Add(word[1]);
                        }
                        patternResult.Add(pattern);

                    }
                }

                // Jika list tidak kosong, berarti ditemukan maka tambahkan ke hasil
                if (patternResult.IndexOf(word[0]) == 0)
                    result.Add(patternResult);
            }
            return result;
        }

        ////////////////////////////////////////////////////
        /////////////////// R E G E X //////////////////////
        ////////////////////////////////////////////////////
        public static List<List<string>> Rgx(List<string> patterns)
        {
            var result = new List<List<string>> { };

            var inspectedText = Services.getTweets();

            foreach (var word in inspectedText)
            {
                var patternResult = new List<String> { };
                var found = false;
                foreach (var pattern in patterns)
                {
                    Match match = Regex.Match(word[0], @"" + pattern ,RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        found = true;
                        if (patternResult.IndexOf(word[0]) != 0)
                            patternResult.Add(word[0]);
                            patternResult.Add(word[1]);
                        patternResult.Add(pattern);
                    }
                }
                if (found)
                    result.Add(patternResult);
            }

            return result;
        }
    }
}