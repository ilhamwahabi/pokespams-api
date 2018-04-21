using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using PokeSpams.Models;

namespace PokeSpams.Controllers.Api
{
    public class SearchController : ApiController
    {
        [HttpPost]
        public List<List<String>> PostSearch(string method)
        {
            var algo = new Algorithm();
            var result = new List<List<String>>();
            var pattern = new List<String> {
                "tes",
                "jual",
                "telpon",
                "beli",
                "game"
            };

            if (method == "regex")
            {
                 result = algo.Regex(pattern);
            } else if (method == "kmp")
            {
                result = algo.KMP(pattern);
            } else
            {
                result = algo.BooyerMoore(pattern);
            }

            return result;
        }
    }
}
