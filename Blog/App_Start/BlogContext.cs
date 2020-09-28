using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blog.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Blog.App_Start
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("DefaultConnection")
        {
        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Subcategories> Subcategories { get; set; }
        public DbSet<Articles> Articles { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Replies> Replies { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Tagmap> Tagmap { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Imagemap> Imagemap { get; set; }
        public DbSet<Shop> Shop { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Comments>().HasRequired(r => r.Articles).WithMany().WillCascadeOnDelete(true);
            modelBuilder.Entity<Replies>().HasRequired(r => r.Comments).WithMany().WillCascadeOnDelete(true);
            //modelBuilder.Entity<Categories>().HasRequired(r => r.Articles).WithMany().WillCascadeOnDelete(true);
            //modelBuilder.Entity<Subcategories>().HasRequired(r => r.Articles).WithMany().WillCascadeOnDelete(true);
        }
    }
}