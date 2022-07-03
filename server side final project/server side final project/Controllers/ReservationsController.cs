using server_side_final_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server_side_final_project.Controllers
{
    public class ReservationsController : ApiController
    {
        public List<Reservation> Get(string email)
        {
            Reservation r = new Reservation();
            return r.getReservations(email);
        }

        // POST api/<controller>
        public string Post(Reservation r)
        {
            return r.insert(r);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete]
        [Route("api/Reservations/cancle/{email}/{id}/{from}/{to}")]
        // DELETE api/<controller>/5
        public string Delete(string email, int id, DateTime from, DateTime to)
        {
            Reservation r = new Reservation();
            return r.cancel(email, id, from, to);
        }
    }
}