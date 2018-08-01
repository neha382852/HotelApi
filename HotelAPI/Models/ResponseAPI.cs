using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Models
{
    public class ResponseAPI
    {
        public List<Hotel> HotelsList { get; set; }

        public StatusRecord Status { get; set; }
    }
}