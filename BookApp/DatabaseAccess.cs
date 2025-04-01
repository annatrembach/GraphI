using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

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
                    connection.Open();
                    string query = "SELECT * FROM Book";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка отримання даних: " + ex.Message);
            }
            return dataTable;
        }

        public int GetNextId()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT ISNULL(MAX(Id), 0) + 1 FROM Book";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        return (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка отримання наступного Id: " + ex.Message);
                return 1;
            }
        }

        public void AddBook(int id, int isbn, string name, string author, string publisher, int year)
        {
            ExecuteNonQuery(
                "INSERT INTO Book (Id, ISBN, Name, Author, Publisher, Year) VALUES (@Id, @ISBN, @Name, @Author, @Publisher, @Year)",
                id, isbn, name, author, publisher, year);
        }

        public void UpdateBook(int id, int isbn, string name, string author, string publisher, int year)
        {
            ExecuteNonQuery(
                "UPDATE Book SET ISBN = @ISBN, Name = @Name, Author = @Author, Publisher = @Publisher, Year = @Year WHERE Id = @Id",
                id, isbn, name, author, publisher, year);
        }

        public void DeleteBook(int id)
        {
            ExecuteNonQuery("DELETE FROM Book WHERE Id = @Id", id);
        }

        private void ExecuteNonQuery(string query, int id, int isbn = 0, string name = null, string author = null, string publisher = null, int year = 0)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Додаємо параметр Id у будь-якому випадку
                        command.Parameters.AddWithValue("@Id", id);

                        // Перевіряємо, чи є відповідний параметр у запиті перед додаванням
                        if (query.Contains("@ISBN")) command.Parameters.AddWithValue("@ISBN", isbn != 0 ? (object)isbn : DBNull.Value);
                        if (query.Contains("@Name")) command.Parameters.AddWithValue("@Name", !string.IsNullOrEmpty(name) ? (object)name : DBNull.Value);
                        if (query.Contains("@Author")) command.Parameters.AddWithValue("@Author", !string.IsNullOrEmpty(author) ? (object)author : DBNull.Value);
                        if (query.Contains("@Publisher")) command.Parameters.AddWithValue("@Publisher", !string.IsNullOrEmpty(publisher) ? (object)publisher : DBNull.Value);
                        if (query.Contains("@Year")) command.Parameters.AddWithValue("@Year", year != 0 ? (object)year : DBNull.Value);

                        // Виконання SQL-команди
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка запиту: " + ex.Message);
            }
        }


    }
}