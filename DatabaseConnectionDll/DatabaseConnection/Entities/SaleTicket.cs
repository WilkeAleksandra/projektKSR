using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnection.entities
{
    [Table("sale_ticket")]
    public class SaleTicket : Entity
    {
        public short amount { get; set; }
        public int sale_id { get; set; }
        public int ticket_id { get; set; }

        public override string ToString()
        {
            return "\nSaleTicket:"
                + "\namount: " + amount
                + "\nsale id: " + sale_id
                + "\nticket id: " + ticket_id;
        }
    }
}