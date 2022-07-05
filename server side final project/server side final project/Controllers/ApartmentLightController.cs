﻿using server_side_final_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server_side_final_project.Controllers
{
    public class ApartmentLightController : ApiController
    {
        [HttpGet]
        [Route("api/ApartmentLight/{from}/{to}")]
        // GET api/<controller>
        public List<ApartmentLight> search(DateTime from, DateTime to)
        {
            ApartmentLight al = new ApartmentLight();
            return al.getApartmentsLight(from, to);
        }

        [HttpGet]
        [Route("api/ApartmentLight/advance/{from}/{to}/{fromPrice:decimal}/{toPrice:decimal}/{rooms}/{score:decimal}/{distFromCenter:decimal}/")]
        // GET api/<controller>/5
        public List<ApartmentLight> advanceSearch(DateTime from, DateTime to, float fromPrice, float toPrice, int rooms, float score, float distFromCenter)
        {
            ApartmentLight al = new ApartmentLight();
            return al.GetApartmentLights(from, to, fromPrice, toPrice, rooms, score, distFromCenter);
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