using EShop.Model;
using EShop.Model.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace EShop.ConsoleApp.Domain
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EShop;Integrated Security=True;");
            connection.Open();

            List<Book> books = new List<Book>();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "select * from Book";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                books.Add(new Book
                {
                    BookId = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Image = reader.GetString(2),
                    Price = reader.GetDouble(3),
                    Supplies = reader.GetInt32(4)
                });
            }


            connection.Close();
        }
    }
}
