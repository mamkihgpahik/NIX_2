using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NIX_2.Entities;
using NIX_2.Repositories;

namespace NIX_2.Entities
{
    class Booking
    {
        public int ID { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime BookingDate { get; set; }

        public Booking(string id, Client clientId, Room roomId, string checkIn, string checkOut, string bookingDate)
        {
            this.ID = Convert.ToInt32(id);
            this.ClientId = clientId.ID;
            this.RoomId = roomId.ID;
            this.CheckIn = DateTime.Parse(checkIn);
            this.CheckOut = DateTime.Parse(checkOut);
            this.BookingDate = DateTime.Parse(bookingDate);
        }
        public Booking(string id, Client clientId, Room roomId, string checkIn, string checkOut)
        {
            this.ID = Convert.ToInt32(id);
            this.ClientId = clientId.ID;
            this.RoomId = roomId.ID;
            this.CheckIn = DateTime.Parse(checkIn);
            this.CheckOut = DateTime.Parse(checkOut);
        }
        public Booking(string id, string clientId, string roomId, string checkIn, string checkOut)
        {
            this.ID = Convert.ToInt32(id);
            this.ClientId = Int32.Parse(clientId);
            this.RoomId = Int32.Parse(roomId);
            this.CheckIn = DateTime.Parse(checkIn);
            this.CheckOut = DateTime.Parse(checkOut);
        }
        public Booking(string id, string clientId, string roomId, string checkIn, string checkOut, string bookingDate)
        {
            this.ID = Convert.ToInt32(id);
            this.ClientId = Int32.Parse(clientId);
            this.RoomId = Int32.Parse(roomId);
            this.CheckIn = DateTime.Parse(checkIn);
            this.CheckOut = DateTime.Parse(checkOut);
            this.BookingDate = DateTime.Parse(bookingDate);
        }
    }
}
