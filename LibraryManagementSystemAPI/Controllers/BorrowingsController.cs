﻿using LibraryManagementSystemAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly DatabaseHelper _databaseHelper;

        public BorrowingsController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        [HttpGet]
        public IActionResult GetAllBorrowings()
        {
            string query = "SELECT * FROM Borrowings";
            var result = _databaseHelper.ExecuteQuery(query);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBorrowing([FromBody] Borrowing borrowing)
        {
            string query = "INSERT INTO Borrowings (MemberId, BookId, BorrowDate, ReturnDate) VALUES (@MemberId, @BookId, @BorrowDate, @ReturnDate)";
            var parameters = new[]
            {
                new SqlParameter("@MemberId", borrowing.MemberId),
                new SqlParameter("@BookId", borrowing.BookId),
                new SqlParameter("@BorrowDate", borrowing.BorrowDate),
              new SqlParameter("@ReturnDate", borrowing.ReturnDate.HasValue ? borrowing.ReturnDate.Value : (object)DBNull.Value)

            };

            _databaseHelper.ExecuteNonQuery(query, parameters);
            return Ok("Borrowing added successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBorrowing(int id, [FromBody] Borrowing borrowing)
        {
            string query = "UPDATE Borrowings SET ReturnDate = @ReturnDate WHERE BorrowingId = @BorrowingId";
            var parameters = new[]
            {
                new SqlParameter("@BorrowingId", id),
               new SqlParameter("@ReturnDate", borrowing.ReturnDate.HasValue ? borrowing.ReturnDate.Value : (object)DBNull.Value)

            };

            _databaseHelper.ExecuteNonQuery(query, parameters);
            return Ok("Borrowing updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBorrowing(int id)
        {
            string query = "DELETE FROM Borrowings WHERE BorrowingId = @BorrowingId";
            var parameters = new[] { new SqlParameter("@BorrowingId", id) };

            _databaseHelper.ExecuteNonQuery(query, parameters);
            return Ok("Borrowing deleted successfully.");
        }
    }
}

