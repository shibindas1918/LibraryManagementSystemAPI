using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryManagementSystemAPI.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        if (parameters?.Length > 0) // Check if parameters are not null and contain elements
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (example: using a logger or custom error handling)
                throw new Exception($"Error executing query: {query}. Details: {ex.Message}", ex);
            }
        }



        public int ExecuteNonQuery(string query, SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
