namespace Library.Domain.Models
{
    public class Borrow : Model
    {
        public DateOnly BorrowDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public string? Comments { get; set; }
        public Book Book { get; set; }
        public User Borrower { get; set; }
    }
}
