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

        //-----
        public List<ApartmentLight> readTop5()
        {
            SqlConnection con = Connect();

            SqlCommand command = createGetTop5Command(con);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<ApartmentLight> apartmentLights = new List<ApartmentLight>();

            while (dr.Read())
            {
                apartmentLights.Add(apartmentLightReader(dr));
            }
            con.Close();
            return apartmentLights;
        }
        private SqlCommand createGetTop5Command(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();
       
            command.CommandText = "spGetTop5ApartsFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }

        //Get all Reservations by apatment id-----

        public List<Reservation> GetResbyApartId(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetApartResCommand(con, id);
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

        private SqlCommand createGetApartResCommand(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spGetReservationsByApartsIdFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }      
        //-----

        //Get all Reservaiton by host id
        public List<Reservation>GetHostres(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetHostResCommand(con,id);
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

        private SqlCommand createGetHostResCommand(SqlConnection con,int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spGeHostReservationsFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //-----

        //Get all apartments
        public List<Apartment> readAllparts()
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetApartsCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Apartment> aparts = new List<Apartment>();
            while (dr.Read())
            {
                aparts.Add(apartmentReader(dr));
            }
            con.Close();
            return aparts;
        }
        private SqlCommand createGetApartsCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "spGetapartsFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //-----

        //Get Apartments by normal search
        public List<Apartment> readApartments(DateTime from, DateTime to)
        {
            SqlConnection con = Connect();

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
        //-----

        //Get ApartmentLights by normal search
        public List<ApartmentLight> readApartmentsLight(DateTime from, DateTime to)
        {
            SqlConnection con = Connect();

            SqlCommand command = createGetCommand(con, from, to);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<ApartmentLight> apartmentLights = new List<ApartmentLight>();

            while (dr.Read())
            {
                apartmentLights.Add(apartmentLightReader(dr));
            }
            con.Close();
            return apartmentLights;
        }

        public ApartmentLight apartmentLightReader(SqlDataReader dr)
        {
            int id = intDr(dr, "id");
            string name = stringDr(dr, "name");
            string picture_url = stringDr(dr, "picture_url");
            double price = doubleDr(dr, "price");

            return new ApartmentLight(id, name, picture_url, price);
        }
        //-----

        //Get Apartments by advance search
        public List<Apartment> readApartments(DateTime from, DateTime to, float fromPrice, float toPrice, int rooms, float score, float distFromCenter)
        {
            SqlConnection con = Connect();

            SqlCommand command = createGetCommand(con, from, to, fromPrice, toPrice, rooms, score, distFromCenter);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Apartment> apartments = new List<Apartment>();

            while (dr.Read())
            {
                apartments.Add(apartmentReader(dr));
            }

            con.Close();
            return apartments;
        }

        private SqlCommand createGetCommand(SqlConnection con, DateTime from, DateTime to, float fromPrice, float toPrice, int rooms, float score, float distFromCenter)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@from", from);
            command.Parameters.AddWithValue("@to", to);
            command.Parameters.AddWithValue("@fromPrice", fromPrice);
            command.Parameters.AddWithValue("@toPrice", toPrice);
            command.Parameters.AddWithValue("@rooms", rooms);
            command.Parameters.AddWithValue("@score", score);
            command.Parameters.AddWithValue("@distFromCenter", distFromCenter);

            command.CommandText = "spAdvanceSearchFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //-----

        //Get ApartmenLights by advance search
        public List<ApartmentLight> readApartmentLights(DateTime from, DateTime to, float fromPrice, float toPrice, int rooms, float score, float distFromCenter)
        {
            SqlConnection con = Connect();

            SqlCommand command = createGetCommand(con, from, to, fromPrice, toPrice, rooms, score, distFromCenter);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<ApartmentLight> apartmentLights = new List<ApartmentLight>();

            while (dr.Read())
            {
                apartmentLights.Add(apartmentLightReader(dr));
            }

            con.Close();
            return apartmentLights;
        }
        //-----

        //Get all ApartmenLights
        public List<ApartmentLight> readAllApartmentLights()
        {
            SqlConnection con = Connect();

            SqlCommand command = new SqlCommand();

            command.CommandText = "spGetapartsFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<ApartmentLight> apartmentLights = new List<ApartmentLight>();

            while (dr.Read())
            {
                apartmentLights.Add(apartmentLightReader(dr));
            }

            con.Close();
            return apartmentLights;
        }
        //-----

        //Get ApartmentLighs by name
        public List<ApartmentLight> readALbyName(string text)
        {
            SqlConnection con = Connect();

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@text", "%" + text + "%");

            command.CommandText = "spGetapartsByNameFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<ApartmentLight> apartmentLights = new List<ApartmentLight>();

            while (dr.Read())
            {
                apartmentLights.Add(apartmentLightReader(dr));
            }

            con.Close();
            return apartmentLights;
        }
        //-----

        //Get Apartment by id
        public Apartment readApartmentByID(int id)
        {
            SqlConnection con = Connect();

            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@id", id);   

            command.CommandText = "spGetApartmentByIdFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            
            while (dr.Read())
            {
                Apartment a = apartmentReader(dr);
                if (a != null)
                {
                    con.Close();
                    return a;
                }
            }
            con.Close();
            return null;
        }
        //-----

        //Get all users
        public List<User> getUsers()
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetUsersCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<User> Users = new List<User>();
            while (dr.Read())
            {
                Users.Add(userReader(dr));
            }
            con.Close();
            return Users;
        }
        private SqlCommand createGetUsersCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "spGetUsersFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //-----

        //Get Users by name
        public List<User> readUsersByName(string text)
        {
            SqlConnection con = Connect();
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@text", "%" + text + "%");
            command.CommandText = "spGetUsersByNamedFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<User> Users = new List<User>();
            while (dr.Read())
            {
                Users.Add(userReader(dr));
            }
            con.Close();
            return Users;
        }
        //-----


        //Get all Hosts
        public List<Host> getHosts()
        {
            SqlConnection con = Connect();
            SqlCommand command = createGetHostCommand(con);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Host> Hosts = new List<Host>();
            while (dr.Read())
            {
                Host h=hostReader(dr);
                Hosts.Add(h);
               
            }
            con.Close();
            return Hosts;
        }
        private SqlCommand createGetHostCommand(SqlConnection con)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = "spGetHostsFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //-----

        //Get Hosts by name
        public List<Host> readHostsByName(string text)
        {
            SqlConnection con = Connect();

            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@name", "%" + text + "%");

            command.CommandText = "spGetHostsByNamedFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Host> Hosts = new List<Host>();
            while (dr.Read())
            {
                Host h = hostReader(dr);
                Hosts.Add(h);

            }
            con.Close();
            return Hosts;
        }
        //-----

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
        //-----

        //Get User by email
        //this method will will return only 1 User object becouse dou to primary key
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
        //-----
        
        //Get Reservations
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
        //-----

        //cancel reservation----
        public string cancelReservation(string email, int id, DateTime from, DateTime to)
        {
            SqlConnection con = Connect();

            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@user_email", email);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@from", from);
            command.Parameters.AddWithValue("@to", to);

            command.CommandText = "spDeleteReservationFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            command.ExecuteNonQuery();
            con.Close();
            return "Reservation cancled";
        }
        //end-----
                
        //Get Reviews by apartment id
        public List<Review> readReviews(int apartment_id)
        {
            SqlConnection con = Connect();
            SqlCommand command = CreateGetCommand(con, apartment_id);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<Review> reviews = new List<Review>();
            while (dr.Read())
            {
                reviews.Add(reviewReader(dr));
            }

            con.Close();
            return reviews;
        }
        private SqlCommand CreateGetCommand(SqlConnection con, int apartment_id)
        {
            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@listing_id", apartment_id);

            command.CommandText = "spGetReviewsByListing_idFP";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
        //-----

        //Support function for read Apartment object from DB
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
            int num_of_cancles = intDr(dr, "num_of_cancles");

            Host h = hostReader(dr);

            return new Apartment(id, name, description, picture_url, neighbourhood_cleansed,
                latitude, longitude, room_type, accommodates, bathrooms_text, bedrooms, beds,
                amenities, price, number_of_reviews, review_scores_rating, num_of_cancles, h);
        }

        //Support function for read Host object from DB
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

        //Support function for read Review object from DB
        private Review reviewReader(SqlDataReader dr)
        {
            int listing_id = intDr(dr, "listing_id");
            DateTime date = Convert.ToDateTime(dr["date"]);
            string user_name = stringDr(dr, "user_name");
            string user_email = stringDr(dr, "user_email");
            string comments = stringDr(dr, "comments");
            return new Review(listing_id, date, user_name, user_email, comments);
        }

        //Support function for read User object from DB
        public User userReader(SqlDataReader dr)
        {
            string name = dr["user_name"].ToString();
            string email = dr["user_email"].ToString();
            string password = dr["password"].ToString();
            int num_of_reservations = Convert.ToInt32(dr["num_of_reservations"]);
            DateTime registration_date = Convert.ToDateTime(dr["registration_date"]);
            int num_of_cancles = Convert.ToInt32(dr["num_of_cancles"]);
           return new User(name, email, password, num_of_reservations, registration_date, num_of_cancles);
        }

        //try catch support functions
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
        //-----

        //Insert Review
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
        //-----

        //Insert Reservation
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
        //-----

        //Connect
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