using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeSpams.Models
{
    public class Algorithm
    {

        ////////////////////////////////////////////////////
        ///////////////////// K M P ////////////////////////
        ////////////////////////////////////////////////////
        public List<List<string>> KMP(List<string> pattern)
        {

            var result = new List<List<string>> { };

            return result;
        }

        /////////////////////////////////////////////////////////////
        //////////////////// B O O Y E R M O O R E //////////////////
        /////////////////////////////////////////////////////////////
        public List<List<string>> BooyerMoore(List<string> patterns)
        {
            List<List<String>> result = new List<List<String>> { };
            var service = new Services();

            var inspectWord = new List<String>
            {
                "Dijual buku seharga 2000",
                "Test drive segera ditempat anda",
                "Hari yang cerah di pantai",
                "Enaknya menjadi atesit pada hari ini",
                "Tidak ada tempat yang lebin indah daripada tempat yang sudah dites",
                "Tidak ada tempat yang lebih indah selain tempat ini"
            };
            
            // Iterasi setiap kalimat
            foreach(var word in inspectWord)
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
                        pointer <= (word.Count() - 1) && 
                        word.Count() >= pattern.Count())
                    {
                        found = service.checkWord(
                                    pattern.ToLower(),
                                    word.Substring(
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
                        if (patternResult.IndexOf(word) == -1)
                        {
                            patternResult.Add(word);
                            patternResult.Add(pattern);
                        }
                    }
                }

                // Jika list tidak kosong, berarti ditemukan maka tambahkan ke hasil
                if (patternResult.IndexOf(word) == 0)
                    result.Add(patternResult);
            }
            return result;
        }

        ////////////////////////////////////////////////////
        /////////////////// R E G E X //////////////////////
        ////////////////////////////////////////////////////
        public List<List<string>> Regex(List<string> pattern)
        {
            var result = new List<List<string>> { };

            return result;
        }
    }
}