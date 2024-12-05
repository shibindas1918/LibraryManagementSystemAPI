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

        public List<Dictionary<string, object>> ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (parameters?.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        return dataTable.AsEnumerable()
                            .Select(row => dataTable.Columns.Cast<DataColumn>()
                            .ToDictionary(column => column.ColumnName, column => row[column]))
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
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
