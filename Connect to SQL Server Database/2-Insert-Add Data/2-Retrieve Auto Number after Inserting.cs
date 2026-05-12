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
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CountryID { get; set; }

    }
    static void AddNewContact(stContact Contact)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        string Query = @"insert into Contacts (FirstName,LastName,Email,Phone,Address,CountryID) " +
            "values(@FirstName,@LastName,@Email,@Phone,@Address,@CountryID);" +
            "select Scope_Identity();";

        SqlCommand command = new SqlCommand(Query, connection);

        command.Parameters.AddWithValue("@FirstName", Contact.FirstName);
        command.Parameters.AddWithValue("@LastName", Contact.LastName);
        command.Parameters.AddWithValue("@Email", Contact.Email);
        command.Parameters.AddWithValue("@Phone", Contact.Phone);
        command.Parameters.AddWithValue("@Address", Contact.Address);
        command.Parameters.AddWithValue("@CountryID", Contact.CountryID);

        try
        {
            connection.Open();
            object result =  command.ExecuteScalar();
            

            if (result != null && int.TryParse(result.ToString(), out int InsertedID))
            {
                Console.WriteLine($"Insertion Successfully ID: {InsertedID}");
            }
            else
            {
                Console.WriteLine("Insertion Faild!");
            }

            //connection.Close();
        }
        catch (Exception ex) {
            Console.WriteLine("Error "+ex.Message);
        }

    }

    public static void Main()
    {

        stContact contactInfo = new stContact()
        {
            FirstName = "Moba",
            LastName = "Mohamed",
            Email = "mm@gmail.com",
            Phone = "015544512154",
            Address = "182 Camel st",
            CountryID = 1
        };

        AddNewContact(contactInfo);



        Console.ReadKey();
    }

}
