using System;
using System.Data.Entity;

namespace WebApplication.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool ValidStatus { get; set; }
    }

    public class BookRecord
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public DateTimeOffset? CreatedTime { get; set; }
        public DateTimeOffset? ReturnTime { get; set; }
    }

    public class BookDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookRecord> BookRecords { get; set; }
    }
}