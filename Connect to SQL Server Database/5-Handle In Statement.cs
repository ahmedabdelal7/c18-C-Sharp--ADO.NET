using System;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using System.ComponentModel;

public class Program
{
    static string connectionString = "Server=.;Database=ContactsDB;User Id=sa;Password=123456;"; // Replace with your actual connection string
    public struct stContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CountryID { get; set; }

    }
    static void DeleteContact(string ContactIDs)
    {
        SqlConnection connection = new SqlConnection(connectionString);

        string query = "DELETE FROM Contacts WHERE ContactID IN ("+ ContactIDs +")";

        SqlCommand command = new SqlCommand(@query, connection);

        try
        {
            connection.Open();

            int affectedRows = command.ExecuteNonQuery();

            if (affectedRows > 0)
            {
                Console.WriteLine($"Delete Done, AffectedRows = {affectedRows}");
            }
            else { Console.WriteLine("Faild To Delete!"); }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
    public static void Main()
    {

        DeleteContact("5,6");

        Console.ReadKey();
    }

}
