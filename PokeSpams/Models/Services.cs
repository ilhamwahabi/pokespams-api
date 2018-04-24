using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

namespace PokeSpams.Models
{
    public class Services
    {
        //private string accessToken = "985637925676204032-oZYIX5y0ARwvWEYsj91AwnYUjoe9ISR";
        //private string accessTokenSecret = "qt6tgqWWUsufyCqHREhTdGkS726i1DCVEsULjRXxAmKyP";
        //private string consumerKey = "CMF9kqcVcmESFSB5wxKDd3ifZ";
        //private string consumerKeySecret = " vif3dOiwCSeQPowWvdY1JYN51ciTJrppJ4CeifeIjyyZrJjiYl";

        public static List<List<String>> getTweets()
        {
            List<List<String>> result = null;

            return result;
        }

        public static int[] borderFunction(String pattern)
        {
            var result = new int[pattern.Count()];
            result[0] = 0;

            var patternLength = pattern.Count();
            var j = 0;
            var i = 1;

            while (i < patternLength)
            {
                if (pattern.ElementAt(j).Equals(pattern.ElementAt(i)))
                {
                    result[i] = j + 1;
                    i++;
                    j++;
                } else if (j > 0)
                {
                    j = result[j - 1];
                }
                else
                {
                    result[i] = 0;
                    i++;
                }
            }

            return result;
;        }

        public static List<int> checkWord(String pattern, String word)
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
                    }
                    else
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
    }
}