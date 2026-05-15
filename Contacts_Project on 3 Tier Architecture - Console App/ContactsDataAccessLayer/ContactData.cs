using System;
using System.Data;
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

        public static int AddNewContact(string FirstName, string LastName, string Email, string Phone,
            string Address, string ImagePath, DateTime DateOfBirth, int CountryID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            int ContactID = -1;
            string @Query =
                @"INSERT INTO Contacts (FirstName,LastName,Email,Phone,Address,ImagePath,DateOfBirth,CountryID)" +
                "VALUES(@FirstName,@LastName,@Email,@Phone,@Address,@ImagePath,@DateOfBirth,@CountryID);"+
                "SELECT Scope_Identity();";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@FirstName",FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);

            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            if (ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString(),out int InsertedID))
                {
                    ContactID = InsertedID;
                }

            }
            catch (Exception ex)
            {
               // Console.WriteLine("error "+ex.Message);
            }
            finally { 
                connection.Close();
            }

            return ContactID;
        }


        public static bool UpdateContact(int ID, string FirstName, string LastName, string Email, string Phone,
            string Address, string ImagePath, DateTime DateOfBirth, int CountryID)
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            int rowsAffectted = 0;

            string Query = "UPDATE Contacts SET " +
                "FirstName = @FirstName," +
                "LastName = @LastName," +
                "Email = @Email," +
                "Phone = @Phone," +
                "Address = @Address," +
                "ImagePath = @ImagePath," +
                "DateOfBirth = @DateOfBirth," +
                "CountryID = @CountryID " +
                "WHERE ContactID = @ID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName",LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@ID", ID);

            if(ImagePath != "")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();
                rowsAffectted =  command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error "+ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return rowsAffectted > 0;

        }

        public static bool DeleteContact(int ID)
        {
            int rowsAffectted = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string @Query = "delete from Contacts where ContactID = @ID";

            SqlCommand command = new SqlCommand(@Query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                rowsAffectted = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return rowsAffectted > 0;
        }

        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string @Query = "SELECT * FROM Contacts";

            SqlCommand command = new SqlCommand(@Query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error "+ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;

        }

        public static bool IsContactExist(int ID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string @query = @"SELECT Found = 1 FROM Contacts WHERE ContactID = @ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", ID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    isFound = true;

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isFound;


        }

        

    }

}
