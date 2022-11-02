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
        [Column(TypeName = "JSON")]
        public ExpandoObject Authors { get; set; }

        public Book(string title, string description, DateTime publishDate, ExpandoObject authors)
        {
            Title = title;
            Description = description;
            PublishDate = publishDate;
            Authors = authors;
        }
    }
}
