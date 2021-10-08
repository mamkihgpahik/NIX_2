using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NIX_2.Entities;

namespace NIX_2.Repositories
{
    class BookingRepository : Repository<Booking>
    {
        public BookingRepository(string address) : base(address) { }
       
        public List<Room> FindRoomForDate(DateTime date, RoomRepository roomRepository)
        {
            List<Room> rooms = roomRepository.GetAll();
            List<Room> freeRooms = new List<Room>();
            bool isBusy;
            string[] buff;

            foreach (var room in rooms)
            {
                using (var f = File.Open(_address, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    using (var sr = new StreamReader(f))
                    {

                        isBusy = false;
                        while (sr.Peek() > -1)
                        {
                            buff = sr.ReadLine().Split(sep);
                            if (room.ID == Convert.ToInt32(buff[2]))
                            {
                                if (DateTime.Compare(date, Convert.ToDateTime(buff[3])) < 0)
                                    continue;
                                else if (DateTime.Compare(date, Convert.ToDateTime(buff[4])) <= 0)
                                    isBusy = true;
                            }
                        }
                    }
                }
                if (!isBusy)
                    freeRooms.Add(room);
            }
            return freeRooms;
        }
        public List<Room> FindRoomForDate(DateTime checkIn, DateTime checkOut, RoomRepository roomRepository)
        {
            List<Room> rooms = roomRepository.GetAll();
            List<Room> freeRooms = new List<Room>();
            bool isBusy;
            string[] buff;

            foreach (var room in rooms)
            {
                using (var f = File.Open(_address, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    using (var sr = new StreamReader(f))
                    {
                        isBusy = false;
                        while (sr.Peek() > -1)
                        {
                            buff = sr.ReadLine().Split(sep);

                            if (room.ID == Convert.ToInt32(buff[2]))
                            {
                                if (DateTime.Compare(checkOut, Convert.ToDateTime(buff[3])) < 0)
                                    continue;
                                else if (DateTime.Compare(checkOut, Convert.ToDateTime(buff[4])) <= 0)
                                    isBusy = true;
                                else if (DateTime.Compare(checkIn, Convert.ToDateTime(buff[4])) <= 0)
                                    isBusy = true;
                                else continue;
                            }
                        }
                    }
                }
                if (!isBusy)
                    freeRooms.Add(room);
            }
            return freeRooms;
        }
        public void MakeBooking(string id, Client client, Room room, string checkIn, string checkOut)
        {
            DateTime now = DateTime.Now;
            bool isExisting = false;
            string[] buff;
            using (var f = File.Open(_address, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var sr = new StreamReader(f))
                {
                    while (sr.Peek() > -1)
                    {
                        buff = sr.ReadLine().Split(sep);
                        if (Convert.ToInt32(buff[2]) == room.ID)
                        {
                            if (DateTime.Compare(Convert.ToDateTime(checkOut), Convert.ToDateTime(buff[3])) < 0)
                                continue;
                            else if (DateTime.Compare(Convert.ToDateTime(checkOut), Convert.ToDateTime(buff[4])) <= 0)
                                isExisting = true;
                            else if (DateTime.Compare(Convert.ToDateTime(checkIn), Convert.ToDateTime(buff[4])) <= 0)
                                isExisting = true;
                            else continue;
                        }
                    }
                }
            }
            if(!isExisting)
            {
                this.Add(new Booking(id, client, room, checkIn, checkOut, now.ToLongDateString()));
            }
        }
        public override void Add(Booking elem)
        {
           
                using (var sw = new StreamWriter(_address, true))
                {
                    sw.WriteLine($"{elem.ID}{sep}{elem.ClientId}{sep}{elem.RoomId}{sep}{elem.CheckIn}{sep}{elem.CheckOut}{sep}{elem.BookingDate}");
                }
        }
        public override bool Delete(int id)
        {
            return base.Delete(id);
        }
        public override List<Booking> GetAll()
        {
            return base.GetAll();
        }
        public override Booking Search(int id)
        {
            return base.Search(id);
        }
    }
}
