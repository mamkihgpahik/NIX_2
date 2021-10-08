using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NIX_2.Entities;
using System.IO;

namespace NIX_2.Repositories
{
    internal class RoomRepository : Repository<Room>
    {
     
        public RoomRepository(string address) : base(address) { }
        

        public override void Add(Room room)
        {
            using (var sw = new StreamWriter(_address, true)) 
            {
                sw.WriteLine($"{room.ID}{sep}{room.roomNumber}{sep}{room.Category}{sep}{room.Price}");
            }
        }

        public override bool Delete(int id)
        {
           return base.Delete(id);
        }

        public override List<Room> GetAll()
        {
            return base.GetAll();
        }        

        public override Room Search(int id)
        {
           return base.Search(id);           
        }
    }
}
