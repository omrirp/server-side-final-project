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

        public User(string name, string email, string password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        public User() { }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

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
    }
}