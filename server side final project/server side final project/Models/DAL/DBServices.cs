using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace server_side_final_project.Models.DAL
{
    public class DBServices
    {
        public DBServices() { }

        //get Apartments (sreach) -----
        public List<Apartment> readApartments(DateTime from, DateTime to)
        {
            return null;
        }


        //end -----


        //Insert User -----
        public int insertUser(User user)
        {
            SqlConnection con = Connect();

            SqlCommand command = CreateInsertCommand(con, user);

            int numAffected = command.ExecuteNonQuery();

            con.Close();

            return numAffected;
        }

        private SqlCommand CreateInsertCommand(SqlConnection con, User user)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@user_email", user.Email);
            command.Parameters.AddWithValue("@user_name", user.Name);
            command.Parameters.AddWithValue("@password", user.Password);

            command.CommandText = "spInserUserFP";//!!
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //end -----

        //get users -----
        public User readUserByEmail(string email)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetCommand(con, email);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            while (dr.Read())
            {
                string uEmail = dr["user_email"].ToString();
                string name = dr["user_name"].ToString();
                string password = dr["password"].ToString();
                if (email.Equals(uEmail))
                {
                    User u = new User(name, email, password);
                    con.Close();
                    return u;
                }
            }

            con.Close();
            return null;
        }

        private SqlCommand CreateGetCommand(SqlConnection con, string email)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@user_email", email);

            command.CommandText = "spGetUserByEmailFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //end -----

        //connect -----
        private SqlConnection Connect()
        {
            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);

            // open the database connection
            con.Open();

            return con;
        }
    }
}