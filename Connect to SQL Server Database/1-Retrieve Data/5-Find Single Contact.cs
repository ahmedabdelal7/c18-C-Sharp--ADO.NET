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
    static bool FindContactByID(int ContactID, ref stContact ContactInfo)
    {
        SqlConnection connection = new SqlConnection(connectionString);

        string query = "SELECT * FROM Contacts WHERE ContactID = @ContactID";

        bool IsFound = false;

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ContactID", ContactID);

        try
        {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) {

                IsFound = true;

                ContactInfo.ContactID = (int)(reader["ContactID"]);
                ContactInfo.FirstName = reader["FirstName"].ToString();
                ContactInfo.LastName = reader["LastName"].ToString();
                ContactInfo.Email = reader["Email"].ToString();
                ContactInfo.Phone = reader["Phone"].ToString();
                ContactInfo.Address = reader["Address"].ToString();
                ContactInfo.CountryID = (int)reader["CountryID"];

            }

            reader.Close();
            connection.Close();
        }
        catch (Exception ex) 
        { 
            Console.WriteLine("Error "+ex.Message.ToString());
        }
        return IsFound;
    }

    public static void Main()
    {


        stContact contact = new stContact();

        if(FindContactByID(4,ref contact))
        {
            Console.WriteLine("Contact ID : "+contact.ContactID);
            Console.WriteLine("First Name : "+contact.FirstName);
            Console.WriteLine("Last Name  : "+contact.LastName);
            Console.WriteLine("Email      : "+contact.Email);
            Console.WriteLine("Phone      : "+contact.Phone);
            Console.WriteLine("Address    : " + contact.Address);
            Console.WriteLine("CountryID  : " + contact.Address);
        }
        else
        {
            Console.WriteLine("Contact is Not Found!");
        }



        Console.ReadKey();
    }

}
