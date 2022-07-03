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
            SqlConnection con = Connect();

            //temporrary !!!
            //string commandStr = "select top 10 * from apartmentsFP";

            SqlCommand command = createGetCommand(con, from, to);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Apartment> apartments = new List<Apartment>();

            while (dr.Read())
            {
                apartments.Add(apartmentReader(dr));
            }

            con.Close();
            return apartments;
        }

        private SqlCommand createGetCommand(SqlConnection con, DateTime from, DateTime to)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@from", from);
            command.Parameters.AddWithValue("@to", to);

            command.CommandText = "spSearchFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
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

                if (email.Equals(uEmail))
                {
                    string name = dr["user_name"].ToString();
                    string password = dr["password"].ToString();
                    int num_of_reservations = Convert.ToInt32(dr["num_of_reservations"]);
                    DateTime registration_date = Convert.ToDateTime(dr["registration_date"]);
                    int num_of_cancles = Convert.ToInt32(dr["num_of_cancles"]);
                    User u = new User(name, email, password,  num_of_reservations, registration_date,  num_of_cancles);
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
                    reservations.Add(new Reservation(uEmail, a, FromDate, ToDate));
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

        //get Apartments
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

        //get Host
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

        //get Reviews
        public List<Review> readReviews(int listing_id)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetCommand(con, listing_id);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<Review> reviews = new List<Review>();
            while (dr.Read())
            {
                reviews.Add(reviewReader(dr));
            }

            con.Close();
            return reviews;
        }

        private Review reviewReader(SqlDataReader dr)
        {
            int listing_id = intDr(dr, "listing_id");
            DateTime date = Convert.ToDateTime(dr["date"]);
            string user_name = stringDr(dr, "user_name");
            string user_email = stringDr(dr, "user_email");
            string comments = stringDr(dr, "comments");
            return new Review(listing_id, date, user_name, user_email, comments);
        }

        private SqlCommand CreateGetCommand(SqlConnection con,int listing_id)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@listing_id", listing_id);

            command.CommandText = "spGetReviewsByListing_idFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
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

        public Review insertReview(Review r)
        {
            SqlConnection con = Connect();

            SqlCommand command = createInsertCommand(con, r);

            command.ExecuteNonQuery();

            con.Close();
            return r;
        }

        private SqlCommand createInsertCommand(SqlConnection con, Review r)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@listing_id", r.Listing_id);
            command.Parameters.AddWithValue("@date", r.Date);
            command.Parameters.AddWithValue("@user_email", r.User_email);
            command.Parameters.AddWithValue("@user_name", r.User_name);
            command.Parameters.AddWithValue("@comments", r.Comments);

            command.CommandText = "spInsertReviewFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //insert new reservation -----
        public string insertReservation(Reservation r)
        {
            SqlConnection con = Connect();

            SqlCommand command = createInsertCommand(con, r);
            command.ExecuteNonQuery();

            con.Close();
            return "success";
        }

        private SqlCommand createInsertCommand(SqlConnection con, Reservation r)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@user_email",r.User_email);
            command.Parameters.AddWithValue("@apartment_id", r.Apartment.Id);
            command.Parameters.AddWithValue("@from_date", r.From_date);
            command.Parameters.AddWithValue("@to_date", r.To_date);

            command.CommandText = "spInsertReservationFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //end -----

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