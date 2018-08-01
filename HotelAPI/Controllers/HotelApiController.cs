using HotelAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HotelAPI.Controllers
{
    public class HotelApiController : ApiController
    {
        private static int _counter = 5;
        private static List<Hotel> _hotels = new List<Hotel>()
        {
            new Hotel(){ HotelId =1, Name = "Hotel Ashiyana", NoOfAvailableRooms = 20, Address = " 1198, FC Rd, Shivajinagar, Pune, Maharashtra", CityCode = " 411004" },
            new Hotel(){ HotelId =2, Name = "Hotel Fidalgo", NoOfAvailableRooms = 10, Address = " No. 100-101, Viman Nagar, Sakore Nagar Road, Sakore Nagar,", CityCode = " 140103" },
            new Hotel(){ HotelId =3, Name = "Turquoise", NoOfAvailableRooms = 50, Address = "29/7, Industrial & Business Park Phase 2, Near Tribune Chowk, Chandigarh,", CityCode = " 160002" },
            new Hotel(){ HotelId =4, Name = "JW Marriott ", NoOfAvailableRooms = 30, Address = "Asset Area 4 - Hospitality District Delhi,", CityCode = "780244" },

        };

        public ResponseAPI GetAllHotels(int id)
        {
            try { 
                return new ResponseAPI()
                {
                    HotelsList = null,
                    Status = new StatusRecord()
                    {
                        status = Status.Failure,
                        ErrorCode = 500,
                        ErrorMessage = "Internal Server Error"
                    }
                };
            }
            catch(Exception ex)
            {
                return new ResponseAPI()
                {
                    HotelsList = null,
                    Status = new StatusRecord()
                    {
                        status = Status.Success,
                        ErrorMessage = string.Empty,
                        ErrorCode = 200
                    }
                };
            }

        }

        public ResponseAPI GetHotelById(int id)
        {
            var desiredHotel = _hotels.Find(x => x.HotelId == id);
            if (desiredHotel == null)
            {
                return new ResponseAPI()
                {
                    HotelsList = null,
                    Status = new StatusRecord()
                    {
                        status = Status.Failure,
                        ErrorCode = 500,
                        ErrorMessage = "Invalid HotelId"
                    }
                };
            }
            else
            {
                return new ResponseAPI()
                {
                    HotelsList = null,
                    Status = new StatusRecord()
                    {
                        status = Status.Success,
                        ErrorMessage = string.Empty,
                        ErrorCode = 200
                    }
                };
            }
           
        }

        public void CreateHotelRecord(Hotel hotelRecord)
        {
            hotelRecord.HotelId = _counter;
            _counter++;
            _hotels.Add(hotelRecord);

        }

    }
}
