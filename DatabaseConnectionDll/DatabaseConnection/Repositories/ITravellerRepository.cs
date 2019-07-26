using DatabaseConnection.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConnection.Repositories
{
    public interface ITravellerRepository: IRepository<Traveller>
    {
        Traveller FindUserByLogin(string login);
        Traveller FindUserByEmail(string email);
    }
}
