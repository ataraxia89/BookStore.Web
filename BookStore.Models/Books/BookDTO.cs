using System;
using BookStore.Models.Enums;

namespace BookStore.Models.Books
{
    public class BookDTO
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime PublishDate { get; set; }

        public int Pages { get; set; }

        public BookGenre Genre { get; set; }

        public string Synopsis { get; set; }

        public double Rating { get; set; }

        public string ISBN { get; set; }
    }
}