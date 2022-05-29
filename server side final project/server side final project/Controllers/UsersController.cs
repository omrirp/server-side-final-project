using server_side_final_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server_side_final_project.Controllers
{
    public class UsersController : ApiController
    {
  
        //GET
        public User Get(string email)
        {
            User u = new User();
            return u.getUserByEmail(email);
        }

        // POST api/<controller>
        public int Post([FromBody] User user)
        {
            User u = new User();
            return u.addUser(user);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}