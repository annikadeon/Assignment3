using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Models
{
    //sets up the Databases for the table created
    public class MoviesDbContext : DbContext
    {
        //constructor that's called when the object is built the first time, inherits from base
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {

        }
        //create movie of application response type (from models)
        //table = Movies
        public DbSet<ApplicationResponse> Movies { get; set; }
    }
}
