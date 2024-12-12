using Library.Domain.Enums;
using Library.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public sealed class Book : BaseEntity
    {
        [Key]
        [Required]
        public ISBN Isbn { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Author { get; set; }
        public DateTime? ReleaseDate { get;  set; }

        public static Book Create(string title, string author, ISBN isbn, DateTime ReleaseDate)
        {
            //ValidateBookData(title, author, isbn, ReleaseDate);
            return new Book
            {
                Title = title,
                Author = author,
                Isbn = isbn,
                ReleaseDate = ReleaseDate,
            };
        }
    }
}
