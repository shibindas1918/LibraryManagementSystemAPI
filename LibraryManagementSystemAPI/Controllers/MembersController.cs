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

        [HttpGet]
        public IActionResult GetAllMembers()
        {
            string query = "SELECT * FROM Members";
            var result = _databaseHelper.ExecuteQuery(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetMemberById(int id)
        {
            string query = "SELECT * FROM Members WHERE MemberId = @MemberId";
            var parameters = new[] { new SqlParameter("@MemberId", id) };

            var result = _databaseHelper.ExecuteQuery(query, parameters);
            if (result.Rows.Count == 0) return NotFound("Member not found.");
            return Ok(result);
        }

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


