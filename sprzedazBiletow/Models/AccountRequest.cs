using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace sprzedazBiletow.Models
{
    public class AccountRequest
    {
        public int UserID { get; set; }

        [DisplayName("LOGIN: ")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Login { get; set; }

        [DisplayName("HASŁO: ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Password { get; set; }

        [DisplayName("IMIĘ: ")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string FirstName { get; set; }

        [DisplayName("NAZWISKO: ")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string LastName { get; set; }

        [DisplayName("EMAIL: ")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Email { get; set; }

        public string AccountErrorMessage { get; set; }
    }
}