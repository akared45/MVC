﻿namespace Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishedYear { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
