namespace GenericRepositoryPattern.Models
{
    using System;
    using System.Collections.Generic;
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }

        public ICollection<Book> BookList { get; set; }
    }
}