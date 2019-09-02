using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sprzedazBiletow.Models.DataProvider
{
    public class Tickets
    {
        public readonly List<Ticket> list
            = new List<Ticket>(){
                (new Ticket(1,"normalny")),
                (new Ticket(2, "studencki")),
                (new Ticket(3, "uczniowski"))
            };
    }
}