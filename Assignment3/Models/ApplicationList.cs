using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Models
{
    //This will add in new applications when someone fills out the form
    public static class ApplicationList 
    {
        private static List<ApplicationResponse> applications = new List<ApplicationResponse>();

        public static IEnumerable<ApplicationResponse> Applications => applications;
        public static void AddApplication(ApplicationResponse application)
        {
            //adds the application to the Sqlite database so it can be accessed when website is closed
            applications.Add(application);
        }
    }
}
