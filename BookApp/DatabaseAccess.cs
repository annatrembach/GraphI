using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BookApp
{
    public class DatabaseAccess
    {
        private readonly string _connectionString;

        public DatabaseAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        }

        public DataTable GetBooks()
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Book";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка отримання даних: " + ex.Message);
            }

            return dataTable;
        }
    }
}
