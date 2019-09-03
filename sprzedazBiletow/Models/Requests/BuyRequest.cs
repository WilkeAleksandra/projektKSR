using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sprzedazBiletow.Models.Requests
{
    public class BuyRequest
    {
        public string TravelId { get; set; }
        public string UserId { get; set; }

        public string FromStation { get; set; }
        public string ToStation { get; set; }

        public string Amount { get; set; }
        public string TicketType { get; set; }
    }
}