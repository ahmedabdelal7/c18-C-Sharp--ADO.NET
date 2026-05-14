using System;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public static class clsContactDataAccess
    {
        public static bool GetContactInfoByID(int ID, ref string FirstName, ref string LastName, ref string Email,
            ref string Phone, ref string Address, ref string ImagePath, ref DateTime DateOfBirth, ref int CountryID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string Query = @"select * from Contacts where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ContactID", ID);

            bool isFound = false;
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {

                    //Contact Found
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];

                    //ImagePath Allows NULL So We Handel Should NULL
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                        ImagePath = "";

                }
                else
                {
                    //Contact Not Found
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
               // Console.WriteLine("Error "+ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }


    }

    public static class DataAccessSettings
    {
        public static string connectionString = "Server=.;Database=ContactsDB;User Id=sa;Password=123456;";

    }
}
