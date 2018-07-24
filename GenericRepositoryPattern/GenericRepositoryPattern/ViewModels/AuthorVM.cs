using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericRepositoryPattern.ViewModels
{
    public class AuthorVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
    }

    public class AuthorDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }

        public ICollection<BookVM> BookList { get; set; }
    }
}