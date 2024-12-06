using LibraryManagementSystemAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly DatabaseHelper _databaseHelper;

        public BookController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }
        

        // Get Method for Getting all the Books 
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            string query = "SELECT * FROM Books";
            var result = _databaseHelper.ExecuteQuery(query);
            return Ok(result);
        }
        // Post Method for adding books 
        [HttpPost]
        public IActionResult AddBook([FromBody] Books book)
        {
            string query = "INSERT INTO Books (Title, Author, ISBN, PublishedYear, CopiesAvailable) VALUES (@Title, @Author, @ISBN, @PublishedYear, @CopiesAvailable)";
            var parameters = new[]
            {
                new SqlParameter("@Title", book.Title),
                new SqlParameter("@Author", book.Author),
                new SqlParameter("@ISBN", book.ISBN),
                new SqlParameter("@PublishedYear", book.PublishedYear),
                new SqlParameter("@CopiesAvailable", book.CopiesAvailable)
            };

            _databaseHelper.ExecuteNonQuery(query, parameters);
            return Ok("Book added successfully");
        }

        //Post Method for Adding Csv

        [HttpPost("Upload-csv")]
        public IActionResult AddBooksFromCsv(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("File is empty.");

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length < 5) continue; 

                    string query = "INSERT INTO Books (Title, Author, ISBN, PublishedYear, CopiesAvailable) VALUES (@Title, @Author, @ISBN, @PublishedYear, @CopiesAvailable)";
                    var parameters = new[]
                    {
                new SqlParameter("@Title", values[0]),
                new SqlParameter("@Author", values[1]),
                new SqlParameter("@ISBN", values[2]),
                new SqlParameter("@PublishedYear", int.Parse(values[3])),
                new SqlParameter("@CopiesAvailable", int.Parse(values[4]))
            };

                    _databaseHelper.ExecuteNonQuery(query, parameters);
                }
            }
            return Ok("Book added successfully from CSV.");
        }
        // Delete Method for Deleting a particular by ID 
        [HttpDelete("{id}")]
        public IActionResult DeleteBorrowing(int id)
        {
            string query = "DELETE FROM Books WHERE BookId = @BookId";
            var parameters = new[] { new SqlParameter("@BookId", id) };

            _databaseHelper.ExecuteNonQuery(query, parameters);
            return Ok("Book has been deleted successfully.");
        }

        // Put Method for updating a particular Borrowing details  
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Books borrowing)
        {
            string query = "UPDATE Books SET Title =@Title, Author=@Author, ISBN = @ISBN, PublishedYear=@PublishedYear, CopiesAvailable=@CopiesAvailable  WHERE Bookid = @Bookid";
            var parameters = new[]
            {
                new SqlParameter("@Bookid", id),
               new SqlParameter("@Title", borrowing.Title),
               new SqlParameter("@Author", borrowing.Author),
               new SqlParameter("@ISBN", borrowing.ISBN),
               new SqlParameter("@PublishedYear", borrowing.PublishedYear),

            };

            _databaseHelper.ExecuteNonQuery(query, parameters);
            return Ok("Borrowings updated successfully.");
        }
    }
}
