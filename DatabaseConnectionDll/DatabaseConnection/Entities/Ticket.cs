using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnection.entities
{
    [Table("ticket")]
    public class Ticket : Entity
    {
        public string name { get; set; }
        public int price_percentage { get; set; }

        public override string ToString()
        {
            return "\nTicket: "
                + "\nName: " + name
                + "\nprice percentage: " + price_percentage;
        }
    }
}