using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Models
{
    public class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int NoOfAvailableRooms { get; set; }
        public string Address { get; set; }
        public string CityCode { get; set; }
    }
}