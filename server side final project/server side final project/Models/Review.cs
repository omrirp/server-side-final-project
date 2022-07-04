using server_side_final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server_side_final_project.Models
{
    public class Review
    {
        int listing_id;//Apartment id
        DateTime date;
        string user_name;
        string user_email;
        string comments;

        public Review(int listing_id, DateTime date, string user_name, string user_email, string comments)
        {
            Listing_id = listing_id;
            Date = date;
            User_name = user_name;
            User_email = user_email;
            Comments = comments;
            
        }

        public Review() { }

        public int Listing_id { get => listing_id; set => listing_id = value; }
        public DateTime Date { get => date; set => date = value; }
        public string User_name { get => user_name; set => user_name = value; }
        public string User_email { get => user_email; set => user_email = value; }
        public string Comments { get => comments; set => comments = value; }

        public List<Review> getReviews(int apartment_id)
        {
            DBServices ds = new DBServices();
            return ds.readReviews(apartment_id);
        }

        public Review insert()
        {
            DBServices ds = new DBServices();
            return ds.insertReview(this);
        }
    }
}