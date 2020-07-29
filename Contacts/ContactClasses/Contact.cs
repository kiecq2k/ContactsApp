using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.ContactClasses
{
    class Contact
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        private static string connstring = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString;


        
        /// <summary>
        /// Selecting data from database
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            // Database connection
            SqlConnection conn = new SqlConnection(connstring);
            // Data table
            DataTable dataTable = new DataTable();
            try
            {
                // Writing sql query
                string sqlQuery = "SELECT * FROM table_contacts;";
                // Creating cmd 
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                conn.Open();
                sqlDataAdapter.Fill(dataTable);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }

        /// <summary>
        /// Inserting data to database
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public bool Insert(Contact contact)
        {
            bool isSuccess = false;

            SqlConnection sqlConnection = new SqlConnection(connstring);
            try
            {
                string sqlQuery = "INSERT INTO table_contacts (FirstName,LastName,ContactNo,Address,Gender" +
                    "VALUES (@FirstName,@LastName,@ContactNo,@Address,@Gender)";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery,sqlConnection);
                sqlCommand.Parameters.AddWithValue("@FirstName", contact.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", contact.LastName);
                sqlCommand.Parameters.AddWithValue("@ContactNo", contact.ContactNo);
                sqlCommand.Parameters.AddWithValue("@Address", contact.Address);
                sqlCommand.Parameters.AddWithValue("@Gender", contact.Gender);

                sqlConnection.Open();
                int rows = sqlCommand.ExecuteNonQuery();
                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }

            return isSuccess;
        }

        /// <summary>
        /// Updating data in database
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public bool Update(Contact contact)
        {
            bool isSuccess = false;
            SqlConnection sqlConnection = new SqlConnection(connstring);
            try
            {
                string sqlQuery = "UPDATE table_contacts SET FirstName=@FirstName,LastName=@LastName," +
                    "ContactNo=@ContactNo,Address=@Address,Gender=@Gender WHERE ContactID=@ContactID";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@FirstName", contact.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", contact.LastName);
                sqlCommand.Parameters.AddWithValue("@ContactNo", contact.ContactNo);
                sqlCommand.Parameters.AddWithValue("@Address", contact.Address);
                sqlCommand.Parameters.AddWithValue("@Gender", contact.Gender);
                sqlCommand.Parameters.AddWithValue("@ContactID", contact.ContactID);
                sqlConnection.Open();
                int rows = sqlCommand.ExecuteNonQuery();
                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return isSuccess;
        }

        /// <summary>
        /// Deleting data from database
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public bool Delete(Contact contact)
        {
            bool isSuccess = false;
            SqlConnection sqlConnection = new SqlConnection(connstring);
            try
            {
                string sqlQuery = "DELETE FROM table_contacts WHERE ContactID=@ContactID";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@ContactID", contact.ContactID);
                sqlConnection.Open();
                int rows = sqlCommand.ExecuteNonQuery();
                if(rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return isSuccess;
        }

    }
}
