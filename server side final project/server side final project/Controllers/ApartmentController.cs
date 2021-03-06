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

        public List<Apartment> Get()
        {
            Apartment a = new Apartment();
            return a.getAllaparts();
        }

        //not used at the moment !
        [HttpGet]
        [Route("api/Apartment/{from}/{to}")]
        // GET api/<controller>
        public List<Apartment> search(DateTime from,DateTime to)
        {
            Apartment a = new Apartment();
            return a.getApartments(from,to);
        }

        //not used at the moment !
        [HttpGet]
        [Route("api/Apartment/advance/{from}/{to}/{fromPrice:decimal}/{toPrice:decimal}/{rooms}/{score:decimal}/{distFromCenter:decimal}/")]
        // GET api/<controller>/5
        public List<Apartment> advanceSearch(DateTime from, DateTime to, float fromPrice, float toPrice, int rooms, float score, float distFromCenter)
        {
            Apartment a = new Apartment();
            return a.GetApartments(from, to, fromPrice, toPrice, rooms, score, distFromCenter);
        }

        [HttpGet]
        [Route("api/Apartment/byId/{id}")]
        public Apartment getByApartmentId(int id)
        {
            Apartment a = new Apartment();
            return a.getApartmentByID(id);
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