using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnection.entities
{
    [Table("traveller")]
    public class Traveller : Entity
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public override string ToString()
        {
            return "\nTraveller:"
                + "\nfirst name: " + first_name
                + "\nlast name: " + last_name
                + "\nemail: " + email
                + "\nlogin: " + login
                + "\npassword: " + password;
        }
    }
}