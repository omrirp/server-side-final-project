using server_side_final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server_side_final_project.Models
{
    public class User
    {
        string name;
        string email;
        string password;
        int num_of_reservations;
        DateTime registration_date;
        int num_of_cancles;
        int Totalprice;

        public User(string name, string email, string password, int num_of_reservations, DateTime registration_date, int num_of_cancles)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            Num_of_reservations = num_of_reservations;
            Registration_date = registration_date;
            Num_of_cancles = num_of_cancles;
        }

        public User() { }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int Num_of_reservations { get => num_of_reservations; set => num_of_reservations = value; }
        public DateTime Registration_date { get => registration_date; set => registration_date = value; }
        public int Num_of_cancles { get => num_of_cancles; set => num_of_cancles = value; }

        public int addUser(User user)
        {
            DBServices ds = new DBServices();
            return ds.insertUser(user);

        }

        public User getUserByEmail(string email)
        {
            DBServices ds = new DBServices();
            return ds.readUserByEmail(email);
        }
        public List<User> getAllUsers()
        {
            DBServices ds = new DBServices();
            return ds.getUsers();
        }
    }
}