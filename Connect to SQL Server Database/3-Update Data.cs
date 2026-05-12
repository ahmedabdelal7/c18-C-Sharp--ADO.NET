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
    static void UpdateContact(int ContactID, stContact ContactInfo)
    {
        SqlConnection connection = new SqlConnection(connectionString);

        string query = @"UPDATE Contacts SET FirstName = @FirstName,LastName = @LastName ,Email = @Email ,Phone = @Phone ,Address = @Address ,CountryID =@CountryID " +
            "WHERE ContactID = @ContactID ";

        SqlCommand command = new SqlCommand(@query, connection);

        command.Parameters.AddWithValue("@FirstName", ContactInfo.FirstName);
        command.Parameters.AddWithValue("@LastName", ContactInfo.LastName);
        command.Parameters.AddWithValue("@Email", ContactInfo.Email);
        command.Parameters.AddWithValue("@Phone", ContactInfo.Phone);
        command.Parameters.AddWithValue("@Address", ContactInfo.Address);
        command.Parameters.AddWithValue("@CountryID", ContactInfo.CountryID);

        command.Parameters.AddWithValue("@ContactID", ContactID);

        try
        {
            connection.Open();

            int affectedRows = command.ExecuteNonQuery();

            if (affectedRows > 0)
            {
                Console.WriteLine($"Updated Done affectedRows = {affectedRows}");
            }
            else { Console.WriteLine("Faild To Update!"); }
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
    public static void Main()
    {

        stContact contactInfo = new stContact()
        {
            FirstName = "Sally",
            LastName = "Allam",
            Email = "sa@gmail.com",
            Phone = "015888154",
            Address = "322 san st",
            CountryID = 4
        };

        UpdateContact(11,contactInfo);

        Console.ReadKey();
    }

}
