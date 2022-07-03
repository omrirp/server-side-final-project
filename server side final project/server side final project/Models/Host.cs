using server_side_final_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server_side_final_project.Models
{
    public class Host
    {
        int id;
        string name;
        string host_response_time;
        string host_picture_url;
        int host_listings_count;
        int host_total_listings_count;
        bool host_has_profile_pic;
        bool has_availability;


        public Host() { }

        public Host(int id, string name, string host_response_time, string host_picture_url,
            int host_listings_count, int host_total_listings_count, bool host_has_profile_pic, bool has_availability)
        {
            this.id = id;
            this.name = name;
            this.host_response_time = host_response_time;
            this.host_picture_url = host_picture_url;
            this.host_listings_count = host_listings_count;
            this.host_total_listings_count = host_total_listings_count;
            this.Host_has_profile_pic = host_has_profile_pic;
            this.has_availability = has_availability;           
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Host_response_time { get => host_response_time; set => host_response_time = value; }
        public string Host_picture_url { get => host_picture_url; set => host_picture_url = value; }
        public int Host_listings_count { get => host_listings_count; set => host_listings_count = value; }
        public int Host_total_listings_count { get => host_total_listings_count; set => host_total_listings_count = value; }
        public bool Host_has_profile_pic { get => host_has_profile_pic; set => host_has_profile_pic = value; }
        public bool Has_availability { get => has_availability; set => has_availability = value; }
        public List<Host> getAllHosts()
        {
            DBServices ds = new DBServices();
            return ds.getHosts();
        }
    }
    
}