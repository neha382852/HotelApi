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

        [HttpGet]
        public ResponseAPI GetAllHotels()
        {
            try { 
                return new ResponseAPI()
                {

                    HotelsList = _hotels,
                    Status = new StatusRecord()
                    {
                        status = Status.Success,
                        ErrorMessage = string.Empty,
                        ErrorCode = 200

                    }
                };
            }
            catch(Exception exception)
            {
                return new ResponseAPI()
                {
                    HotelsList = null,
                    Status = new StatusRecord()
                    {
                        status = Status.Failure,
                        ErrorCode = 500,
                        ErrorMessage = "Internal Server Error."+exception.Message
                      
                    }

                };
            }

        }

        public ResponseAPI GetHotelById(int id)
        {
            try
            {
                var desiredHotel = _hotels.Find(x => x.HotelId == id);
                if (desiredHotel != null)
                {
                    return new ResponseAPI()
                    {
                        HotelsList = new List<Hotel>() { desiredHotel },
                        Status = new StatusRecord()
                        {
                            status = Status.Success,
                            ErrorMessage = "Hotel Found!",
                            ErrorCode = 200
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
                            status = Status.Failure,
                            ErrorCode = 404,
                            ErrorMessage = "Exception occured.Invalid HotelId"
                        }

                    };
                }
            }
            catch(Exception exception)
            {
                return new ResponseAPI()
                {
                    HotelsList = null,
                    Status = new StatusRecord()
                    {
                        status = Status.Failure,
                        ErrorCode = 500,
                        ErrorMessage ="Internal server error. "+exception.Message
                    }

                };
            }
           
        }

        [HttpPost]
        public ResponseAPI CreateHotel(Hotel hotel)
        {
            ResponseAPI APIResponse = new ResponseAPI();
            try
            {
                if (hotel != null)
                {
                    hotel.HotelId = _counter++;
                    _hotels.Add(hotel);
                    APIResponse.HotelsList = _hotels;
                    return new ResponseAPI
                    {
                        HotelsList = _hotels, 
                        Status = new StatusRecord()
                        {
                            status = Status.Success,
                            ErrorMessage = "Hotel Successfully Added!",
                            ErrorCode = 201
                        }
                    };
                }
                else
                {
                    return new ResponseAPI
                    {
                        HotelsList = null,
                        Status = new StatusRecord()
                        {
                            status = Status.Failure,
                            ErrorMessage = "Exception Occurred: Data send was Invalid ",
                            ErrorCode = 500
                        }
                    };
                }
            }
            catch (Exception exception)
            {
                return new ResponseAPI
                {
                    HotelsList = null,
                    Status = new StatusRecord()
                    {
                        status = Status.Failure,
                        ErrorMessage = "Internal Server error "+ exception.Message,
                        ErrorCode = 500
                    }
                };
               
            }

        }

        [HttpDelete]
        public ResponseAPI RemoveHotelById(int id)
        {
            try
            {
                var hotelToBeDeleted = _hotels.Find(x => x.HotelId == id);
                if (hotelToBeDeleted != null)
                {
                    _hotels.Remove(hotelToBeDeleted);
                    return new ResponseAPI
                    {
                        HotelsList = _hotels,
                        Status = new StatusRecord()
                        {
                            status = Status.Success,
                            ErrorCode = 200,
                            ErrorMessage = "Hotel Successfully Deleted"
                        }
                    };
                }
                else
                {
                    return new ResponseAPI
                    {
                        HotelsList = null,
                        Status = new StatusRecord()
                        {
                            status = Status.Failure,
                            ErrorCode = 404,
                            ErrorMessage = "Invalid HotelId"
                        }
                    };
                }
            }
            catch (Exception exception)
            {
                return new ResponseAPI
                {
                    HotelsList = null,
                    Status = new StatusRecord()
                    {
                        status = Status.Failure,
                        ErrorCode = 500,
                        ErrorMessage = "Exception Occurred :Internal Server error " + exception.Message
                    }
                };

            }
        }


        [HttpPut]
        public ResponseAPI BookHotelById(int id, [FromBody] int numOfRoomsToBeBooked)
        {
            try
            {
                var hotelToBeBooked = _hotels.Find(x => x.HotelId == id);
                if (hotelToBeBooked != null && numOfRoomsToBeBooked > 0)
                {

                    if (hotelToBeBooked.NoOfAvailableRooms >= numOfRoomsToBeBooked)
                    {
                        hotelToBeBooked.NoOfAvailableRooms = hotelToBeBooked.NoOfAvailableRooms- numOfRoomsToBeBooked;
                        return new ResponseAPI
                        {
                            HotelsList = _hotels,
                            Status = new StatusRecord()
                            {
                                status = Status.Success,
                                ErrorCode = 200,
                            }
                        };

                    }
                    else
                    {
                        return new ResponseAPI
                        {
                            HotelsList = null,
                            Status = new StatusRecord()
                            {
                                status = Status.Failure,
                                ErrorCode = 404,
                                ErrorMessage = "Rooms not available"
                            }
                        };

                    }
                }
                else
                {
                    return new ResponseAPI
                    {
                        HotelsList = null,
                        Status = new StatusRecord()
                        {
                            status = Status.Failure,
                            ErrorCode = 404,
                            ErrorMessage = "Invalid HotelId"
                        }
                    };
                }

            }
            catch (Exception exception)
            {
                return new ResponseAPI
                {
                    HotelsList = null,
                    Status = new StatusRecord()
                    {
                        status = Status.Failure,
                        ErrorCode = 500,
                        ErrorMessage = "Exception Occurred :" + exception.Message
                    }
                };
            }
        }

    }
}

 