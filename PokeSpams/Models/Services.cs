using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeSpams.Models
{
    public class Services
    {
        //private string accessToken = "985637925676204032-oZYIX5y0ARwvWEYsj91AwnYUjoe9ISR";
        //private string accessTokenSecret = "qt6tgqWWUsufyCqHREhTdGkS726i1DCVEsULjRXxAmKyP";
        //private string consumerKey = "CMF9kqcVcmESFSB5wxKDd3ifZ";
        //private string consumerKeySecret = " vif3dOiwCSeQPowWvdY1JYN51ciTJrppJ4CeifeIjyyZrJjiYl";

        public List<List<String>> getTwit()
        {
            List<List<String>> result = null;

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