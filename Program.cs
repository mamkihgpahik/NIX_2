using System;
using NIX_2.Entities;
using NIX_2.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NIX_2
{

    class Program
    {
        public static T Find<T>(Repository<T> repository)
        {
            Console.WriteLine("Enter id:");
            Int32.TryParse(Console.ReadLine(), out int number);  
            return repository.Search(number);
        }
        public static List<T> Show<T>(Repository<T> repository)
        {
            return repository.GetAll();
        }
        public static void Remove<T>(Repository<T> repository)
        {
            
            Console.WriteLine("Enter the number of the attribute you want to remove: ");
            Int32.TryParse(Console.ReadLine(), out int number);
            bool _isDeleted = repository.Delete(number);
            if (_isDeleted)
            {
                Console.WriteLine($"Attribute №{number} was successfully deleted ");
            }
            else Console.WriteLine("An error has occurred ");
        }

        static void Main(string[] args)
        {
            //initialization
            Room testRoom1 = new Room("1", "1", "Normal", "600");
            Room testRoom2 = new Room("2", "2", "Lux", "1200");
            Room testRoom3 = new Room("3", "3", "legacy", "3000");
            

            Client testClient1 = new Client("1", "Arshava Artem", "5/1/2008", "NK219132298");
            Client testClient2 = new Client("2", "Pupkin Vasya", "13/6/2001", "NK292138198");
            Client testClient3 = new Client("3", "Vasyl Daniil", "20/2/1998", "KB2921384298");

            
            Booking testBooking4 = new Booking("1", testClient3, testRoom1, ("10/10/2021"), ("15/10/2021"));
            Booking testBooking2 = new Booking("2", testClient2, testRoom2, ("20/10/2021"), ("23/10/2021"), ("1/10/2021"));
            Booking testBooking3 = new Booking("3", testClient1, testRoom2, ("8/11/2021"), ("25/11/2021"));

            RoomRepository roomRepository = new RoomRepository("rooms.txt");
            ClientRepository clientRepository = new ClientRepository("clients.txt");
            BookingRepository bookingRepository = new BookingRepository("orders.txt");
          
            //methods
            void ShowAllRooms(RoomRepository roomRepository)
            {
                var _buff = Show<Room>(roomRepository);
                Console.WriteLine("-------------------------All Rooms-----------------------");
                foreach (var room in _buff)
                {
                    Console.WriteLine($"ID: {room.ID}\tNumber: {room.roomNumber}\tCategory: {room.Category}\tPrice for one night: {room.Price}");
                }
                Console.WriteLine("---------------------------------------------------------");
            }
            void FindRoom(RoomRepository roomRepository)
            {
                Room room = Find<Room>(roomRepository);
                if (room.ID != -1)
                    Console.WriteLine($"Searched room for id - {room.ID}:\n\tNumber: {room.roomNumber}" +
                        $"\tCategory: {room.Category}\tPrice for one night: {room.Price}");
                else Console.WriteLine($"Searched room for id - {room.ID} wasn't found");
            }
            void DeleteRoom(RoomRepository roomRepository)
            {
                Remove<Room>(roomRepository);
            }
            void ShowAllClients(ClientRepository clientRepository)
            {
                var _buff = Show<Client>(clientRepository);
                Console.WriteLine("-------------------------All Clients-----------------------");
                foreach (var client in _buff)
                {
                    Console.WriteLine($"ID: {client.ID}\tName: {client.Name}\tBirthday: {client.bDate} \tPassport: {client.passID}");
                }
                Console.WriteLine("---------------------------------------------------------");
            }
            void FindClient(ClientRepository clientRepository)
            {
                Client client = Find<Client>(clientRepository);
                if (client.ID != -1)
                    Console.WriteLine($"Searched client for ID: { client.ID}\tName: {client.Name}\tBirthday: { client.bDate} \tPassport: { client.passID}");
                else Console.WriteLine($"Searched client for id - {client.ID} wasn't found");
            }
            void DeleteClient(ClientRepository clientRepository)
            {
                Remove<Client>(clientRepository);
            }
            void ShowAllBookings(BookingRepository bookingRepository)
            {
                var _buff = Show<Booking>(bookingRepository);
                Console.WriteLine("-------------------------All Bookings-----------------------");
                foreach (var booking in _buff)
                {
                    Console.WriteLine($"ID: {booking.ID}\tClientID: {booking.ClientId}\tRoomID: {booking.RoomId} \tCheckIn: {booking.CheckIn} \tCheckOut: {booking.CheckOut} \tBookingDate: {booking.BookingDate}");
                }
                Console.WriteLine("---------------------------------------------------------");
            }
            void FindBooking(BookingRepository bookingRepository)
            {
                Booking booking = Find<Booking>(bookingRepository);
                if (booking.ID != -1)
                Console.WriteLine($"Searched booking for ID: {booking.ID}\tClientID: {booking.ClientId}\tRoomID: {booking.RoomId} \tCheckIn: {booking.CheckIn} \tCheckOut{booking.CheckOut} \tBookingDate{booking.BookingDate}");
                else Console.WriteLine($"Searched booking for id - {booking.ID} wasn't found");
            }
            void DeleteBooking(BookingRepository bookingRepository)
            {
                Remove<Booking>(bookingRepository);
            }

            roomRepository.Add(testRoom1);
            roomRepository.Add(testRoom2);
            roomRepository.Add(testRoom3);

            clientRepository.Add(testClient1);
            clientRepository.Add(testClient2);
            clientRepository.Add(testClient3);

            bookingRepository.Add(testBooking3);
            bookingRepository.Add(testBooking2);
            bookingRepository.Add(testBooking4);

            bookingRepository.MakeBooking("1", testClient2, testRoom3, ("10/10/2021"), ("15/10/2021")); // should be done

            bookingRepository.MakeBooking("2", testClient2, testRoom1, ("10/10/2022"), ("15/10/2022")); // shouldn't be done

            bookingRepository.MakeBooking("3", testClient3, testRoom1, ("16/10/2021"), ("19/10/2021")); // should be done
            bookingRepository.MakeBooking("4", testClient1, testRoom3, ("8/11/2021"), ("25/11/2021")); // should be done

            bookingRepository.MakeBooking("5", testClient2, testRoom2, ("10/11/2021"), ("30/11/2021")); // shouldn't be done
            bookingRepository.MakeBooking("6", testClient2, testRoom2, ("1/11/2021"), ("20/11/2021")); // shouldn't be done
            bookingRepository.MakeBooking("7", testClient2, testRoom2, ("1/11/2021"), ("30/11/2021")); // shouldn't be done

            bookingRepository.FindRoomForDate(DateTime.Parse("15/10/2021"), roomRepository);
            bookingRepository.FindRoomForDate(DateTime.Parse("10/11/2021"), roomRepository);

            bookingRepository.FindRoomForDate(DateTime.Parse("10/11/2021"), DateTime.Parse("30/11/2021"), roomRepository);
            bookingRepository.FindRoomForDate(DateTime.Parse("1/11/2021"), DateTime.Parse("20/11/2021"), roomRepository);
            bookingRepository.FindRoomForDate(DateTime.Parse("1/11/2021"), DateTime.Parse("30/11/2021"), roomRepository);
            bookingRepository.FindRoomForDate(DateTime.Parse("16/10/2021"), DateTime.Parse("19/10/2021"), roomRepository);

            ShowAllRooms(roomRepository);
            FindRoom(roomRepository);
            DeleteRoom(roomRepository);

            ShowAllClients(clientRepository);
            FindClient(clientRepository);
            DeleteClient(clientRepository);

            ShowAllBookings(bookingRepository);
            FindBooking(bookingRepository);
            DeleteBooking(bookingRepository);
        }
    }
}
