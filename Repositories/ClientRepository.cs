using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NIX_2.Entities;
using System.IO;

namespace NIX_2.Repositories
{
    class ClientRepository : Repository<Client>
    {
        public ClientRepository(string address) : base(address) { }
      
        public override void Add(Client client)
        {
            using (var sw = new StreamWriter(_address, true))
            {
                sw.WriteLine($"{client.ID}{sep}{client.Name}{sep}{client.bDate}{sep}{client.passID}");
            }
        }

        public override bool Delete(int id)
        {
            return base.Delete(id);
        }

        public override List<Client> GetAll()
        {
            return base.GetAll();
        }

        public override Client Search(int id)
        {
            return base.Search(id);
        }
    }
}

