using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenericRepositoryPattern.ViewModels
{
    public class BookVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorName { get; set; }
    }

    public class BookEM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public int AuthorId { get; set; }
    }
}