using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace PokeSpam.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        private string accessToken = "985637925676204032-oZYIX5y0ARwvWEYsj91AwnYUjoe9ISR";
        private string accessTokenSecret = "qt6tgqWWUsufyCqHREhTdGkS726i1DCVEsULjRXxAmKyP";
        private string consumerKey = "CMF9kqcVcmESFSB5wxKDd3ifZ";
        private string consumerKeySecret = " vif3dOiwCSeQPowWvdY1JYN51ciTJrppJ4CeifeIjyyZrJjiYl";

        private static List<string> examplePattern = new List<string> { "Test", "Sampah" };

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Search()
        {
            return Json("Test");
        }

        // ALGORITMA

        public List<List<string>> KMP(List<string> pattern)
        {
            var result = new List<List<string>> { };
       
            return result;
        }

        public List<List<string>> BooyerMoore(List<string> pattern)
        {
            var result = new List<List<string>> { };

            return result;
        }

        public List<List<string>> Regex(List<string> pattern)
        {
            var result = new List<List<string>> { };

            return result;
        }

    }
}