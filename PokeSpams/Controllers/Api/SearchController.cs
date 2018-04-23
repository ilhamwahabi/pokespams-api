using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using PokeSpams.Models;

namespace PokeSpams.Controllers.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SearchController : ApiController
    {
        [HttpGet]
        public List<List<String>> GetSearch(string method, string patterns)
        {
            List<String> pattern = new List<String> (patterns.Split(','));
            var algo = new Algorithm();
            var result = new List<List<String>>();
            
            if (method == "regex")
            {
                result = algo.Regex(pattern);
            }
            else if (method == "kmp")
            {
                result = algo.KMP(pattern);
            }
            else if (method == "bym")
            {
                result = algo.BooyerMoore(pattern);
            }

            return result;
        }
    }
}
