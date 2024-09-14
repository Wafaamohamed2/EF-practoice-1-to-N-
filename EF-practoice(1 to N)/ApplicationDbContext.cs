
using EF_practoice_1_to_N_;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEF
{
    public class ApplicationDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        
            options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EFCore; Integrated Security = True");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //    define one to Many relation using flount API

            //modelBuilder.Entity<Blog>()
            //.HasMany(b => b.Posts)
            //.WithOne();


            // Or

            ////modelBuilder.Entity<Post>()
            ////    .HasOne(p => p.Blog)
            ////    .WithMany(b => b.Posts);


            //Or whene there are no navigation property in any classes
            // we will use this

            //modelBuilder.Entity<Post>()
            //    .HasOne<Blog>()
            //    .WithOne()
            //    .HasForeignKey(p => p.BlogId)
            //    ;

            modelBuilder.Entity<RecordOfSafe>() 
                .HasOne(s => s.Car)
                .WithMany(c => c.SaleHistory)
                .HasForeignKey(s => new {s.CarLicensePlate , s.CarState})
                .HasPrincipalKey(c => new { c.LicensePalate  , c.State});
        }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }



        public DbSet<RecordOfSafe> Records { get; set; }
        public DbSet<Car> Cars { get; set; }
    }

    // One to Many relation

    //Principal Entity 
    public class Blog
    {
        public int BlogId { get; set; }  // PK

        [Required , MaxLength(1000)]
        public string Url { get; set; }

        public List<Post> Posts { get; set; }    // Collection Navigation Property ----> which type is the type of tha Child


    }

    //Dependent Entity
    public class Post
    {
      public int PostId { get; set; }   
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }   // FK

        public Blog Blog {  get; set; }   // Reference Navigation Property ----> which type is the type of tha parent

    }

    public class Car
    {
        public int CarId { get; set; }
        public string LicensePalate { get; set; }

        public string State { get; set; }
        public string Moke { get; set; }

        public List<RecordOfSafe> SaleHistory { get; set; }


    }
    public class RecordOfSafe
    {

        public int RecordOfSafeId { get; set; }
        public DateTime DateSold { get; set; }

        public decimal Price { get; set; }

        public string CarLicensePlate { get; set; }

        public string CarState { get; set; }
        public Car Car { get; set; }
    }
}
