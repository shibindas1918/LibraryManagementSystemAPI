namespace LibraryManagementSystemAPI.Data
{
    public class Borrowing
    {
        public int BorrowingId { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }

}
