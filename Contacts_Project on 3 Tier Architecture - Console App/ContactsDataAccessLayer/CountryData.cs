using System;
using System.Data.SqlClient;
using System.Data;


namespace ContactsDataAccessLayer
{
    public static class clsCountryDataAccess
    {
        public static bool GetCountryInfoByID(int ID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string @query = @"SELECT * FROM Countries WHERE CountryID = @ID";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    //Country Found
                    isFound = true;

                    CountryName = (string)reader["CountryName"];

                }
                reader.Close();

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetCountryInfoByName(ref int ID, string CountryName)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string @query = @"select * from Countries where CountryName = @CountryName";

            SqlCommand command = new SqlCommand(@query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    ID = (int)reader["CountryID"];

                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static int AddNewCountry(string CountryName)
        {
            int CountryID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string @query = @"INSERT INTO Countries (CountryName) VALUES (@CountryName); 
                              SELECT Scope_Identity();";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != DBNull.Value && int.TryParse(result.ToString(), out int InsertedID))
                {
                    CountryID = InsertedID;
                }

            }
            catch (Exception ex)
            { Console.WriteLine("Error " + ex.Message); }
            finally
            {
                connection.Close();
            }
            return CountryID;
        }
        public static bool UpdateCountry(int ID, string CountryName)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(@DataAccessSettings.connectionString);
            string query = @"UPDATE Countries SET CountryName = @CountryName WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;

        }
        public static bool IsCountryExist(int ID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(@DataAccessSettings.connectionString);
            string query = @"SELECT Found = 1 FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                isFound = (result != null);

                //Another Way

                //SqlDataReader reader = command.ExecuteReader();
                //isFound = reader.HasRows;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool DeleteCountry(int ID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);
            string @query = @"DELETE FROM Countries WHERE CountryID = @CountryID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;
        }
        public static DataTable GetAllCountries()
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.connectionString);

            string @query = @"SELECT * FROM Countries ORDER BY CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    table.Load(reader);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }
    }
}
