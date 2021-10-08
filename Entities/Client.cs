using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIX_2.Entities
{
    class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime bDate { get; set; }
        public string passID { get; set; }
        
        public Client(string id, string name, string bDate, string passID)
        {
            this.ID = Convert.ToInt32(id);
            this.Name = name;    
            this.bDate = Convert.ToDateTime(bDate);
            this.passID = passID;
        }
    }
}
