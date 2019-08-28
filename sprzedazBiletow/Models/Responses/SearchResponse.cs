
using System.ComponentModel;

namespace sprzedazBiletow.Models
{
    public class SearchResponse
    {
        public SearchResponse()
        {
            this.TrainName = "aaa";
            this.DepartureDate = "aaa";
            this.DepartureHour = "aaa";
            this.Price = "aaa";
            this.Time = "aaa";
        }
        public SearchResponse(string TrainName, string DepartureDate, string DepartureHour, string Price, string Time)
        {
            this.TrainName = TrainName;
            this.DepartureDate = DepartureDate;
            this.DepartureHour = DepartureHour;
            this.Price = Price;
            this.Time = Time;
        }

        [DisplayName("Pociąg: ")]
        public string TrainName { get; set; }

        [DisplayName("Data: ")]
        public string DepartureDate { get; set; }

        [DisplayName("Godzina odjazdu: ")]
        public string DepartureHour { get; set; }

        [DisplayName("Cena: ")]
        public string Price { get; set; }

        [DisplayName("Czas: ")]
        public string Time { get; set; }
    }
}