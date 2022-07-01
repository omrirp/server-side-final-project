using server_side_final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server_side_final_project.Models
{
    public class Reservation
    {
        string user_email;
        Apartment apartment;
        DateTime from_date;
        DateTime to_date;

        public Reservation(string user_email, Apartment apartment, DateTime from_date, DateTime to_date)
        {
            User_email = user_email;
            Apartment = apartment;
            From_date = from_date;
            To_date = to_date;
        }

        public string insert(Reservation r)
        {
            DBServices ds = new DBServices();
            return ds.insertReservation(r);
        }

        public Reservation() { }

        public string User_email { get => user_email; set => user_email = value; }
        public Apartment Apartment { get => apartment; set => apartment = value; }
        public DateTime From_date { get => from_date; set => from_date = value; }
        public DateTime To_date { get => to_date; set => to_date = value; }

        public List<Reservation> getReservations(string email)
        {
            DBServices db  =  new DBServices();
            return db.readReservations(email);
        }
    }
}