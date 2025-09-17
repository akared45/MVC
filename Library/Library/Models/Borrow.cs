using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string BorrowerName { get; set; }
        public DateTime BorrowDate { get; set; }
    }
}
