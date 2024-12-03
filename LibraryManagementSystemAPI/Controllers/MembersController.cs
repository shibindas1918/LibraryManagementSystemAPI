using LibraryManagementSystemAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LibraryManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly DatabaseHelper _databaseHelper;

        public MembersController(DatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }

        // Get Method for getting all the Members 

        [HttpGet]
        public IActionResult GetAllMembers()
        {
            string query = "SELECT * FROM Members";
            var result = _databaseHelper.ExecuteQuery(query);
            return Ok(result);
        }

        // Get Method  for getting a particular Member by id 

        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            string query = "SELECT * FROM Members WHERE MemberId = @MemberId";
            var parameters = new[] { new SqlParameter("@MemberId", id) };

            var result = _databaseHelper.ExecuteQuery(query, parameters);
            if (result.Rows.Count == 0)
                return NotFound("Member not found.");
            return Ok(result);
        }
        // Post method to add a Member 
        [HttpPost]
        public IActionResult AddMember([FromBody] Member member)
        {
            string query = "INSERT INTO Members (Name, Email, PhoneNumber, MembershipDate) VALUES (@Name, @Email, @PhoneNumber, @MembershipDate)";
            var parameters = new[]
            {
                new SqlParameter("@Name", member.Name),
                new SqlParameter("@Email", member.Email),
                new SqlParameter("@PhoneNumber", member.PhoneNumber),
                new SqlParameter("@MembershipDate", member.MembershipDate)
            };

            _databaseHelper.ExecuteNonQuery(query, parameters);
            return Ok("Member added successfully.");
        }

         // Head method for Member 
        [HttpHead]
        public IActionResult Header(Member header)
        {
            string query = "Insert into the Members(Name,Email,PhoneNumber,MembershipDate) values (@Name,@Email,@PhoneNumber,@MembershipDate)";

            var parameters = new[]
            {
                new SqlParameter("Name", header.Name),
                new SqlParameter("Email", header.Email),
                new SqlParameter("PhoneNumber", header.PhoneNumber),
                new SqlParameter("MembershipDate",header.MembershipDate)

            };
            _databaseHelper.ExecuteNonQuery(query,parameters);
            return Ok("Header updated successfully.");
            
        }
        // Method to update a Member by ID
        [HttpPut("{id}")]
        public IActionResult UpdateMember(int id, [FromBody] Member member)
        {
            string query = "UPDATE Members SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber WHERE MemberId = @MemberId";
            var parameters = new[]
            {
                new SqlParameter("@MemberId", id),
                new SqlParameter("@Name", member.Name),
                new SqlParameter("@Email", member.Email),
                new SqlParameter("@PhoneNumber", member.PhoneNumber)
            };

            _databaseHelper.ExecuteNonQuery(query, parameters);
            return Ok("Member updated successfully.");
        }
        // Method to Delete a Member by ID 
        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            string query = "DELETE FROM Members WHERE MemberId = @MemberId";
            var parameters = new[] { new SqlParameter("@MemberId", id) };

            _databaseHelper.ExecuteNonQuery(query, parameters);
            return Ok("Member deleted successfully.");
        }
    }
}


