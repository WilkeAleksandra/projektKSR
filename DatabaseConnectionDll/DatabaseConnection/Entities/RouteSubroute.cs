using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnection.entities
{
    [Table("route_subroute")]
    public class RouteSubroute : Entity
    {
        public short route_order_number { get; set; }
        public short seats_amount { get; set; }
        public int subroute_id { get; set; }
        public int route_id { get; set; }

        public override string ToString()
        {
            return "\nRouteSubroute:"
                + "\nRoute order number: " + route_order_number
                + "\nseats amount: " + seats_amount
                + "\nsubroute_id: " + subroute_id
                + "\nroute_id: " + route_id;
        }
    }
}