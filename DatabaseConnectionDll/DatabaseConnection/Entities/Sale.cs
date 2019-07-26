using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnection.entities
{
    [Table("sale")]
    public class Sale : Entity
    {
        public bool payment_status { get; set; }
        public DateTime sale_date { get; set; }
        public string from_station { get; set; }
        public string to_station { get; set; }
        public int route_id { get; set; }
        public int traveller_id { get; set; }

        public override string ToString()
        {
            return "\nSale:"
                + "\npayment status: " + payment_status
                + "\nsale date: " + sale_date
                + "\nfrom_station: " + from_station
                + "\nto station: " + to_station
                + "\nroute id:" + route_id
                + "\ntraveller id: " + traveller_id;
        }
    }
}