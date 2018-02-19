using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WorldList.Models;

namespace WorldList.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            List<Country> allCountries = Country.GetAll();
            return View(allCountries);
        }

        [HttpGet("/CountryD/{code}")]
        public ActionResult CountryD (string code)
        {
            Country country = Country.Find(code);
            return View(country);
        }
    }
}
