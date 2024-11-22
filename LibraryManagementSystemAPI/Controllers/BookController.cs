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

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            string query = "SELECT * FROM Books";
            var result = _databaseHelper.ExecuteQuery(query);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
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


        [HttpPost("upload-csv")]
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

            return Ok("Books added successfully from CSV.");
        }
    }
}
