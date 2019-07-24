using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnection.entities
{
    [Table("subroute")]
    public class Subroute : Entity
    {
        public string from_station { get; set; }
        public string to_station { get; set; }
        public decimal price { get; set; }
        public TimeSpan travel_duration { get; set; }

        public override string ToString()
        {
            return "\nSubroute:" 
                + "\nFrom station: " + from_station
                + "\nTo station: " + to_station
                + "\nprice: " + price
                + "\ntravel duration: " + travel_duration;
        }
    }
}