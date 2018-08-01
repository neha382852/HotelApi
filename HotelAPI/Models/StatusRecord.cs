using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelAPI.Models
{
    public class StatusRecord
    {
        public Status status  { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public enum Status
    {
        Success,
        Failure,
        Warning
    }
}