using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Controllers
{
    public class HomeController : Controller
    {
        ////private readonly ILogger<HomeController> _logger;

        ////public HomeController(ILogger<HomeController> logger)
        ////{
        ////    _logger = logger;
        ////}

        ////constructor
        //private MoviesDbContext context { get; set; }
        //public HomeController(MoviesDbContext con)
        //{
        //    context = con;
        //}

        private MoviesDbContext context { get; set; }

        private readonly ILogger<HomeController> _logger;
        //Constructor for the controller. Passes in the DBcontext models. 
        public HomeController(ILogger<HomeController> logger, MoviesDbContext con)
        {
            _logger = logger;
            //set context to the passed in model to be used in the controller
            context = con;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyPodcasts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MovieForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MovieForm(ApplicationResponse applicationResponse)
        {
            if (ModelState.IsValid)
            {
                //add application to database
                context.Movies.Add(applicationResponse);
                context.SaveChanges();
                //update database
                //ApplicationList.AddApplication(applicationResponse);
                return View("Confirmation", applicationResponse);
            }
            else
            {
                return View();
            }
        }
        public IActionResult MoviesList()
        {
            return View(ApplicationList.Applications);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
