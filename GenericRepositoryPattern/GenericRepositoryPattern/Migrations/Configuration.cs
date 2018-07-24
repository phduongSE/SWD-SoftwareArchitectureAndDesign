namespace GenericRepositoryPattern.Migrations
{
    using GenericRepositoryPattern.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GenericRepositoryPattern.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GenericRepositoryPattern.Models.DatabaseContext context)
        {
            List<Author> authors = new List<Author>();
            for (int i = 1; i <= 10; i++)
            {
                Author newAuthor = new Author()
                {
                    Id = i,
                    Name = "Author" + i,
                    BirthDay = DateTime.Now
                };
                authors.Add(newAuthor);

                context.Author.AddOrUpdate(newAuthor);
            }

            Random rd = new Random();

            for (int i = 0; i < 100; i++)
            {
                context.Book.AddOrUpdate(new Book()
                {
                    Title = "Book " + i,
                    PublishDate = DateTime.Now,
                    AuthorId = rd.Next(1, 10)
                });
            }

            
        }
    }
}
