using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using PokeSpams.Models;

namespace PokeSpams.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SearchController : ApiController
    {
        [HttpGet]
        public List<List<String>> GetSearch(string method, string patterns)
        {
            List<String> pattern = new List<string> ((patterns.Replace("%20", " ")).Split(','));
            var result = new List<List<string>>();
            
            if (method == "regex")
            {
                result = Algorithm.Rgx(pattern);
            }
            else if (method == "kmp")
            {
                result = Algorithm.Kmp(pattern);
            }
            else if (method == "bym")
            {
                result = Algorithm.BooyerMoore(pattern);
            }

            return result;
        }
    }
}
