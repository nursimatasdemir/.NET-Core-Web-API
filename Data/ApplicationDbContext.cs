using api.Models;
using Microsoft.EntityFrameworkCore;
//using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace api.Data;

//this class is for searching individual tables and to transform database tables to objects
//AppDBContext is also an object
    public class ApplicationDbContext : DbContext
    {
        //base constructor is going to add the info we gave to the database
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        //dbSet stands for a set of data that comes from database 'and' you can specify which form it should return
        //deferred execution // data manipulation
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comment { get; set; }

    }
