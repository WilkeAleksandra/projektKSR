using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseConnection.entities
{
    [Table("route")]
    public class Route : Entity
    {
        public string train_name { get; set; }
        public DateTime departure_date { get; set; }
        public TimeSpan departure_hour { get; set; }

        public override string ToString()
        {
            return "\nRoute: " 
                + "\nName: " + train_name 
                + "\ndepartue date: " + departure_date
                + "\ndepartue hour: " + departure_hour;
        }
    }
}