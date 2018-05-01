using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;

namespace PokeSpams.Models
{
    public class Services
    {
        public static string oauth_token = "985637925676204032-SM2QQopAdzw2qyAdlxk5NBnhH4CG40g"; //"insert here...";
        public static string oauth_token_secret = "4lb7KrcVAzddYYg49v7aaIfJQPMPh0OP3cTkNwfjmdeFE"; //"insert here...";
        public static string oauth_consumer_key = "vDIwbKQyNx6tr5YNrJK5LYJIT";// "insert here...";
        public static string oauth_consumer_secret = "ANB1hcVEb13Xid6QHHWeisUsc2lrz3P1OO6mdoncKZ8PpsODFI";// "insert here...";

        public static List<List<string>> GetTweets()
        {
            var result = new List<List<string>> { };

            const string query = "tes";
            var url = "https://api.twitter.com/1.1/search/tweets.json";
            const string count = "100";
            const string lang = "id";

            // oauth implementation details
            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";

            var oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow
                               - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

            // create oauth signature
            var baseFormat = "count={7}&lang={8}&oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                             "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&q={6}";

            var baseString = string.Format(baseFormat,
                oauth_consumer_key,
                oauth_nonce,
                oauth_signature_method,
                oauth_timestamp,
                oauth_token,
                oauth_version,
                Uri.EscapeDataString(query),
                Uri.EscapeDataString(count),
                Uri.EscapeDataString(lang)
            );

            baseString = string.Concat("GET&", Uri.EscapeDataString(url), "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
               "&", Uri.EscapeDataString(oauth_token_secret));

            string oauth_signature;
            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(
                    hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            // create the request header
            var headerFormat = "OAuth oauth_consumer_key=\"{3}\", oauth_nonce=\"{0}\", " +
                               "oauth_signature=\"{5}\", oauth_signature_method=\"{1}\", " +
                               "oauth_timestamp=\"{2}\", oauth_token=\"{4}\", " +
                               "oauth_version=\"{6}\"";

            var authHeader = string.Format(headerFormat,
                Uri.EscapeDataString(oauth_nonce),
                Uri.EscapeDataString(oauth_signature_method),
                Uri.EscapeDataString(oauth_timestamp),
                Uri.EscapeDataString(oauth_consumer_key),
                Uri.EscapeDataString(oauth_token),
                Uri.EscapeDataString(oauth_signature),
                Uri.EscapeDataString(oauth_version)
            );

            ServicePointManager.Expect100Continue = false;

            // make the request
            url += "?q=" + Uri.EscapeDataString(query) + "&lang=" + Uri.EscapeDataString(lang) + "&count=" + Uri.EscapeDataString(count);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", authHeader);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var objText = reader.ReadToEnd();

            JObject jsonDat = JObject.Parse(objText);

            for (int i = 0; i < jsonDat.GetValue("statuses").Count(); i++)
            {
                var data = (JObject)jsonDat.GetValue("statuses")[i];

                result.Add(new List<string>
                {
                    data.GetValue("text").ToString(),
                    data.GetValue("created_at").ToString(),
                });
            }

            return result;
        }

        public static int[] BorderFunction(string pattern)
        {
            var result = new int[pattern.Length];
            result[0] = 0;

            var patternLength = pattern.Length;
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
        }

        public static List<int> CheckWord(string pattern, string word)
        {
            if (word == null) throw new ArgumentNullException(nameof(word));
            var equals = true;
            var stepSize = 0;

            if (pattern.Last() == word.Last())
            {
                var i = 1;
                while (equals && i <= (pattern.Length - 1))
                {
                    if (
                        pattern.ElementAt((pattern.Length - 1) - i)
                            .Equals(word.ElementAt((pattern.Length - 1) - i)))
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
                stepSize = pattern.Length - (pattern.IndexOf(word.Last()) == -1 ? 0 : pattern.IndexOf(word.Last()) + 1);
                equals = false;
            }

            return new List<int> { equals ? 1 : 0, stepSize };
        }
    }
}