using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server_side_final_project.Models.DAL
{
    public class Apartment
    {
        int id;
        string name;
        string description;
        string picture_url;
        string neighbourhood_cleansed;
        float latitude;
        float longitude;
        string room_type;
        int accommodates;
        string bathrooms_text;
        int bedrooms;
        int beds;
        string amenities;
        double price;
        int number_of_reviews;
        float review_scores_rating;
        Host host;

        public Apartment(int id, string name, string description, string picture_url, string neighbourhood_cleansed,
            float latitude, float longitude, string room_type, int accommodates, string bathrooms_text, int bedrooms,
            int beds, string amenities, double price, int number_of_reviews, float review_scores_rating, Host host)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Picture_url = picture_url;
            this.Neighbourhood_cleansed = neighbourhood_cleansed;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Room_type = room_type;
            this.Accommodates = accommodates;
            this.Bathrooms_text = bathrooms_text;
            this.Bedrooms = bedrooms;
            this.Beds = beds;
            this.Amenities = amenities;
            this.Price = price;
            this.Number_of_reviews = number_of_reviews;
            this.Review_scores_rating = review_scores_rating;
            this.Host = host;
        }

        public Apartment() { }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Picture_url { get => picture_url; set => picture_url = value; }
        public string Neighbourhood_cleansed { get => neighbourhood_cleansed; set => neighbourhood_cleansed = value; }
        public float Latitude { get => latitude; set => latitude = value; }
        public float Longitude { get => longitude; set => longitude = value; }
        public string Room_type { get => room_type; set => room_type = value; }
        public int Accommodates { get => accommodates; set => accommodates = value; }
        public string Bathrooms_text { get => bathrooms_text; set => bathrooms_text = value; }
        public int Bedrooms { get => bedrooms; set => bedrooms = value; }
        public int Beds { get => beds; set => beds = value; }
        public string Amenities { get => amenities; set => amenities = value; }
        public double Price { get => price; set => price = value; }
        public int Number_of_reviews { get => number_of_reviews; set => number_of_reviews = value; }
        public float Review_scores_rating { get => review_scores_rating; set => review_scores_rating = value; }
        public Host Host { get => host; set => host = value; }

        public List<Apartment> getApartments(DateTime from, DateTime to)
        {
            DBServices ds = new DBServices();
            return ds.readApartments(from, to);
        }

        public List<Apartment> GetApartments(DateTime from, DateTime to, float fromPrice, float toPrice, int rooms, float score, float distFromCenter)
        {
            DBServices ds = new DBServices();
            return ds.readApartments(from, to, fromPrice, toPrice, rooms, score, distFromCenter);
        }
    }
}