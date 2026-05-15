using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Input;


namespace ContactsDataAccessLayer
{
    public static class clsCountryDataAccess
    {
        public static bool GetCountryInfoByID(int ID, ref string CountryName, ref string CountryCode, ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    CountryName = (string)reader["CountryName"];

                    if (reader["CountryCode"] != DBNull.Value)
                        CountryCode = (string)reader["CountryCode"];
                    else
                        CountryCode = "";

                    if (reader["PhoneCode"] != DBNull.Value)
                        PhoneCode = (string)reader["PhoneCode"];
                    else
                        PhoneCode = "";
                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static bool GetCountryInfoByName(ref int ID, string CountryName, ref string CountryCode, ref string PhoneCode)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
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

                    if (reader["CountryCode"] != DBNull.Value)
                        CountryCode = (string)reader["CountryCode"];
                    else
                        CountryCode = "";

                    if (reader["PhoneCode"] != DBNull.Value)
                        PhoneCode = (string)reader["PhoneCode"];
                    else
                        PhoneCode = "";

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
        public static int AddNewCountry(string CountryName, string CountryCode, string PhoneCode)
        {
            int CountryID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string @query = @"INSERT INTO Countries (CountryName ,CountryCode, PhoneCode ) VALUES (@CountryName, @CountryCode, @PhoneCode); 
                              SELECT Scope_Identity();";

            SqlCommand command = new SqlCommand(@query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);//Not Allow Null

            if (CountryCode != "")
                command.Parameters.AddWithValue("@CountryCode", CountryCode);
            else
                command.Parameters.AddWithValue("@CountryCode", DBNull.Value);

            if (PhoneCode != "")
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            else
                command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);



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
        public static bool UpdateCountry(int ID, string CountryName, string CountryCode, string PhoneCode)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(@clsDataAccessSettings.connectionString);
            string query = @"UPDATE Countries SET CountryName = @CountryName, 
                            CountryCode = @CountryCode, PhoneCode = @PhoneCode
                            WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            command.Parameters.AddWithValue("@CountryID", ID);

            if (CountryCode != "")
                command.Parameters.AddWithValue("@CountryCode", CountryCode);
            else
                command.Parameters.AddWithValue("@CountryCode", DBNull.Value);

            if (PhoneCode != "")
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            else
                command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);

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
            SqlConnection connection = new SqlConnection(@clsDataAccessSettings.connectionString);
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
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);
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
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

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
