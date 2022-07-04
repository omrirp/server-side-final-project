﻿using server_side_final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server_side_final_project.Models
{
    public class ApartmentLight
    {
        int id;
        string name;
        string picture_url;
        double price;

        public ApartmentLight(int id, string name, string picture_url, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Picture_url = picture_url;
            this.Price = price;
        }

        public ApartmentLight() { }

        public int Id { get => id; set => id = value; }

        public string Name { get => name; set => name = value; }
        public string Picture_url { get => picture_url; set => picture_url = value; }
        public double Price { get => price; set => price = value; }

        public List<ApartmentLight> getApartmentsLight(DateTime from, DateTime to)
        {
            DBServices ds = new DBServices();
            return ds.readApartmentsLight(from, to);
        }
    }
}