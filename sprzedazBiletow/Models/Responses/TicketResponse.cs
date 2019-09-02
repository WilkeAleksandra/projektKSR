using System.ComponentModel;

namespace sprzedazBiletow.Models.Responses
{
    public class TicketResponse
    {
        public TicketResponse()
        {
            this.TrainName = "aaa";
            this.FromStation = "aaa";
            this.ToStation = "aaa";
            this.SaleDate = "aaa";
            this.DepartureDate = "aaa";
            this.DepartureHour = "aaa";
            this.Price = "aaa";
            this.Time = "aaa";
            this.PaymentStatus = "aaa";
        }
        public TicketResponse(string TrainName, string FromStation, string ToStation, string SaleDate, string DepartureDate,
            string DepartureHour, string Price, string Time, string PaymentStatus)
        {
            this.TrainName = TrainName;
            this.FromStation = FromStation;
            this.ToStation = ToStation;
            this.SaleDate = SaleDate;
            this.DepartureDate = DepartureDate;
            this.DepartureHour = DepartureHour;
            this.Price = Price;
            this.Time = Time;
            this.PaymentStatus = PaymentStatus;
        }

        [DisplayName("Pociąg: ")]
        public string TrainName { get; set; }

        [DisplayName("Ze stacji: ")]
        public string FromStation { get; set; }

        [DisplayName("Do stacji: ")]
        public string ToStation { get; set; }

        [DisplayName("Data zakupu: ")]
        public string SaleDate { get; set; }

        [DisplayName("Data: ")]
        public string DepartureDate { get; set; }

        [DisplayName("Godzina odjazdu: ")]
        public string DepartureHour { get; set; }

        [DisplayName("Cena: ")]
        public string Price { get; set; }

        [DisplayName("Czas: ")]
        public string Time { get; set; }

        [DisplayName("Status: ")]
        public string PaymentStatus { get; set; }
    }
}