using System;
using System.Data;
using System.Net;
using System.Data.SqlClient;

public class Program
{
    static string connectionString = "Server=.;Database=ContactsDB;User Id=sa;Password=123456;"; // Replace with your actual connection string
     
   
    static string GetFirstName(int ContactID)

    {
        string FirstName = "";

        SqlConnection connection = new SqlConnection(connectionString);

        string query = "Select FirstName from Contacts where ContactID = @ContactID ";

        SqlCommand commend = new SqlCommand(query, connection);

        commend.Parameters.AddWithValue("@ContactID ", ContactID);

        try
        {
            connection.Open();
            //Use ExecuteScalar when you wnt to return single value
            // return first colmn from first row.
            object result = commend.ExecuteScalar(); 

            if(result != null)
            {
                //return FirstName;//Wrong Way, You should close the connection first.
                FirstName = result.ToString();
            }
            else
            {
                FirstName = "";
            }

            connection.Close();
        }
        catch (Exception ex) {

            Console.WriteLine($"Error: {ex.Message}");
        }
        return FirstName;

    }

    public static void Main()
    {
        Console.WriteLine(GetFirstName(1));

        Console.ReadKey();
    }

}
