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
                        ErrorMessage = "List of all Hotels. Okay!",
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
                        ErrorMessage = "Internal Server Error."
                      
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
                    throw new Exception("Hotel Not Found");
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
                        ErrorCode = 404,
                        ErrorMessage = "Exception occured: Invalid HotelId. " + exception.Message
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
                    throw new Exception("Data sent was Invalid");
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
                        ErrorMessage = "Exception Occurred :" + exception.Message,
                        ErrorCode = 500
                    }
                };
               
            }

        }

        [HttpDelete]
        public ResponseAPI RemoveHotelById(int Id)
        {
            try
            {
                var hotelToBeDeleted = _hotels.Find(x => x.HotelId == Id);
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
                    throw new Exception("Hotel not found");
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
                        ErrorCode = 404,
                        ErrorMessage = "Exception Occurred :Invalid HotelId" + exception.Message
                    }
                };

            }
        }


        [HttpPut]
        public ResponseAPI BookHotelById(int Id, [FromBody] int NumOfRoomsToBeBooked)
        {
            try
            {
                var hotelToBeBooked = _hotels.Find(x => x.HotelId == Id);
                if (hotelToBeBooked != null && NumOfRoomsToBeBooked > 0)
                {

                    if (hotelToBeBooked.NoOfAvailableRooms >= NumOfRoomsToBeBooked)
                    {
                        hotelToBeBooked.NoOfAvailableRooms = hotelToBeBooked.NoOfAvailableRooms- NumOfRoomsToBeBooked;
                        return new ResponseAPI
                        {
                            HotelsList = _hotels,
                            Status = new StatusRecord()
                            {
                                status = Status.Success,
                                ErrorCode = 200,
                                ErrorMessage = "Room Booked Successfully"
                            }
                        };

                    }
                    else
                    {
                        throw new Exception("Rooms Not Available");
                      
                    }
                }
                else
                {
                    throw new Exception("Rooms Not Available");
                  
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
                        ErrorCode = 404,
                        ErrorMessage = "Exception Occurred :" + exception.Message
                    }
                };
            }
        }

    }
}

 