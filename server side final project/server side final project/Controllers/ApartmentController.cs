using server_side_final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server_side_final_project.Controllers
{
    public class ApartmentController : ApiController
    {
        [HttpGet]
        [Route("api/Apartment/{from}/{to}")]
        // GET api/<controller>
        public List<Apartment> search(DateTime from,DateTime to)
        {
            Apartment a = new Apartment();
            return a.getApartments(from,to);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
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