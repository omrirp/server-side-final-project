﻿using System;
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
            SqlConnection con = Connect();

            //temporrary !!!
            string commandStr = "select top 10 * from apartmentsFP";

            SqlCommand command = new SqlCommand(commandStr, con);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Apartment> apartments = new List<Apartment>();

            while (dr.Read())
            {
                apartments.Add(apartmentReader(dr));
            }

            con.Close();
            return apartments;
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

        //get Users -----
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
        
        //get Reservations -----
        public List<Reservation> readReservations(string email)
        {
            SqlConnection con = Connect();

            SqlCommand command = ResListCommand(con, email);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<Reservation> reservations = new List<Reservation>();

            while (dr.Read())
            {
                string uEmail = dr["user_email"].ToString();
                DateTime FromDate = Convert.ToDateTime(dr["from_date"]);
                DateTime ToDate = Convert.ToDateTime(dr["to_date"]);
                Apartment a = apartmentReader(dr);
                if (a != null)
                { 
                    Reservation res = new Reservation(uEmail, a, FromDate, ToDate);
                    reservations.Add(res);
                }
            }
            con.Close();
            return reservations;
        }

        private SqlCommand ResListCommand(SqlConnection con, string email)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@user_email", email);
            command.CommandText = "spreadResFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }
        //end -----


        private Apartment apartmentReader(SqlDataReader dr)
        {
            int id = intDr(dr, "id");
            string name = stringDr(dr, "name");
            string description = stringDr(dr, "description");
            string picture_url = stringDr(dr, "picture_url");
            string neighbourhood_cleansed = stringDr(dr, "neighbourhood_cleansed");
            float latitude = floafDr(dr, "latitude");
            float longitude = floafDr(dr, "longitude");
            string room_type = stringDr(dr, "room_type");
            int accommodates = intDr(dr, "accommodates");
            string bathrooms_text = stringDr(dr, "bathrooms_text");
            int bedrooms = intDr(dr, "bedrooms");
            int beds = intDr(dr, "beds");
            string amenities = stringDr(dr, "amenities");
            double price = doubleDr(dr, "price");
            int number_of_reviews = intDr(dr, "number_of_reviews");
            float review_scores_rating = floafDr(dr, "review_scores_rating");

            Host h = hostReader(dr);

            return new Apartment(id, name, description, picture_url, neighbourhood_cleansed,
                latitude, longitude, room_type, accommodates, bathrooms_text, bedrooms, beds,
                amenities, price, number_of_reviews, review_scores_rating, h);
        }

        private Host hostReader(SqlDataReader dr)
        {
            int id = intDr(dr, "host_id");
            string name = stringDr(dr, "host_name");
            string host_response_time = stringDr(dr, "host_response_time");
            string host_picture_url = stringDr(dr, "host_picture_url");
            int host_listings_count = intDr(dr, "host_listings_count");
            int host_total_listings_count = intDr(dr, "host_total_listings_count");
            bool host_has_profile_pic;
            if (dr["host_has_profile_pic"].ToString().Equals("t"))
            {
                host_has_profile_pic = true;
            }
            else
            {
                host_has_profile_pic = false;
            }
            bool has_availability;
            if (Convert.ToInt32(dr["has_availability"]) == 1)
            {
                has_availability = true;
            }
            else
            {
                has_availability = false;
            }

            return new Host(id, name, host_response_time, host_picture_url, host_listings_count,
                host_total_listings_count, host_has_profile_pic, has_availability);
        }

        private bool boolDr(SqlDataReader dr, string attr)
        {
            try
            {
                return (dr[attr].ToString() == "1" || dr[attr].ToString() == "t");
            }
            catch (Exception)
            {
                return false;
            }
        }
        private int intDr(SqlDataReader dr, string attr)
        {
            try
            {
                return Convert.ToInt32(dr[attr]);
            }
            catch (Exception)
            {
                return 0;                
            }
        }
        private float floafDr(SqlDataReader dr, string attr)
        {
            try
            {
                return float.Parse(dr[attr].ToString());
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private double doubleDr(SqlDataReader dr, string attr)
        {
            try
            {
                return Convert.ToDouble(dr[attr]);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        private string stringDr(SqlDataReader dr, string attr)
        {
            try
            {
                return dr[attr].ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        //not good !
        //private T genericReader(SqlDataReader dr, string type, string attr)
        //{
        //    if (type.Equals("int"))
        //    {
        //        try
        //        {
        //            return (T)Convert.ChangeType(int.Parse(dr[attr].ToString()),typeof(int));
        //        }
        //        catch (Exception)
        //        {

        //            return (T)Convert.ChangeType(0, typeof(int));
        //        }
        //    }
        //    if (type.Equals("float"))
        //    {
        //        try
        //        {
        //            return (T)Convert.ChangeType(float.Parse(dr[attr].ToString()), typeof(float));
        //        }
        //        catch (Exception)
        //        {

        //            return (T)Convert.ChangeType(0, typeof(float));
        //        }
        //    }
        //    if (type.Equals("double"))
        //    {
        //        try
        //        {
        //            return (T)Convert.ChangeType(double.Parse(dr[attr].ToString()), typeof(double));
        //        }
        //        catch (Exception)
        //        {

        //            return (T)Convert.ChangeType(0, typeof(int));
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            return (T)Convert.ChangeType(int.Parse(dr[attr].ToString()), typeof(string));
        //        }
        //        catch (Exception)
        //        {

        //            return (T)Convert.ChangeType("", typeof(string));
        //        }
        //    }
        //}

        //connect
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