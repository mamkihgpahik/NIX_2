using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIX_2.Entities
{
    public class Room
    {
        public int ID { get; set; }
        public int roomNumber { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public Room(string id, string roomNumber, string category, string price)
        {
            this.ID = Convert.ToInt32(id);
            this.roomNumber = Convert.ToInt32(roomNumber);
            this.Category = category;
            this.Price = Convert.ToDecimal(price);
        }

    }
}
