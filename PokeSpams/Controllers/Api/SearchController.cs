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
        [HttpGet]
        public List<String> GetSearch()
        {
            return new List<String> { "Hei", "Woy" };
        }

        [HttpPost]
        public List<List<String>> PostSearch(string method)
        {
            var algo = new Algorithm();
            var result = new List<List<String>>();
            var pattern = new List<String> { "Test", "Jual" };

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
            //return new List<String> { "Yes", "No", method };
        }
    }
}
