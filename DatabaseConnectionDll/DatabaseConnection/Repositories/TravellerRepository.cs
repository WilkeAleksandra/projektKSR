using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseConnection.entities;

namespace DatabaseConnection.Repositories
{
    public class TravellerRepository : Repository<Traveller>, ITravellerRepository
    {
        public Traveller FindUserByEmail(string email)
        {
            using (var context = new GenericContext<Traveller>())
            {
                var item = context.Entity.Where<Traveller>(t => t.email == email).Single();
                if (item == null)
                {
                    return null;
                }
                return item;
            }
        }

        public Traveller FindUserByLogin(string login)
        {
            using (var context = new GenericContext<Traveller>())
            {
                var item = context.Entity.Where<Traveller>(t => t.login == login).Single();
                if (item == null)
                {
                    return null;
                }
                return item;
            }
        }
    }
}
