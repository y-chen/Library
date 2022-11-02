using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Library.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace Library.Database.Entities
{
    public class Book : EntityBase
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        [Required]
        public string Author { get; set; }

        public Book(string title, string description, DateTime publishDate, string author)
        {
            Title = title;
            Description = description;
            PublishDate = publishDate;
            Author = author;
        }
    }
}
