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
        //pull up the BaconSale Podcast
        public IActionResult MyPodcasts()
        {
            return View();
        }
        //form for the movies which is added to the database when it's added
        [HttpGet]
        public IActionResult MovieForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MovieForm(ApplicationResponse applicationResponse)
        {
            //adding the movie to the database, UNLESS it's independence day
            if (ModelState.IsValid)
            {
                if (applicationResponse.Title.ToLower() == "independence day")
                {
                    //if the movie is independence day, we won't update it and return this view instead
                    return View("NoIndependenceDay");

                }
                else
                {
                    //update the database, add the new application, and return confirmation view
                    context.Movies.Add(applicationResponse);
                    context.SaveChanges();
                    return View("Confirmation", applicationResponse);

                }
            }
            else
            {
                return View(applicationResponse);
            }
        }
        //once the edit button is clicked on, pull up this form, with the infor from the movie to edit
        [HttpGet]
        public IActionResult EditMovie()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditMovie(ApplicationResponse applicationResponse)
        {
            if (ModelState.IsValid)
            {

                if (applicationResponse.Title.ToLower() == "independence day")
                {
                    //if the movie is independence day, we won't update it and return this view instead
                    return View("NoIndependenceDay");

                }
                else
                {
                    //otherwise, update the application with the new changes and a success page

                    context.Movies.Update(applicationResponse);
                    context.SaveChanges();
                    return View("EditSuccess", applicationResponse);

                }
            }
            else
            {
                return View(applicationResponse);
            }
        }

        //view to delete movie, passing in the data of the movie 
        [HttpPost]
        public IActionResult DeleteMovie(ApplicationResponse applicationResponse)
        {
            //where the ID matches, remove it from the database
            IQueryable<ApplicationResponse> queryable = context.Movies.Where(m => m.MovieID == applicationResponse.MovieID);

            foreach (var x in queryable)
            {
                //remove x from the list
                context.Movies.Remove(x);
            }
            context.SaveChanges();
            //return success view once movie is deleted
            return View("DeleteMovie");
        }
        [HttpGet]
        public IActionResult MoviesList()
        {

            //only return the movies list
            return View(context.Movies);
        }
        [HttpPost]
        public IActionResult MoviesList(ApplicationResponse applicationResponse)
        {
            //Returns the edit move view
            return View("EditMovie", applicationResponse);
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
