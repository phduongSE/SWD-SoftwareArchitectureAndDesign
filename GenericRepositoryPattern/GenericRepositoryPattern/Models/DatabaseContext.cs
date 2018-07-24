using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GenericRepositoryPattern.Models
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }

        public DatabaseContext() : base("GRPContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<Author>().HasKey(_ => _.Id);
            modelBuilder.Entity<Author>().Property(_ => _.Name).HasMaxLength(192);
            modelBuilder.Entity<Author>().Property(_ => _.BirthDay);

            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Book>().HasKey(_ => _.Id);
            modelBuilder.Entity<Book>().Property(_ => _.Title).HasMaxLength(192);
            modelBuilder.Entity<Book>().Property(_ => _.PublishDate);

            modelBuilder.Entity<Author>()
                .HasMany<Book>(_ => _.BookList)
                .WithRequired(_ => _.Author)
                .HasForeignKey(_ => _.AuthorId);
        }
    }
}