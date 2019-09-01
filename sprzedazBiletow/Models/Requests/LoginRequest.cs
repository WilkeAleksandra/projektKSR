using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace sprzedazBiletow.Models
{
    public partial class LoginRequest
    {
        public int UserID { get; set; }
        
        [DisplayName("LOGIN: ")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Login { get; set; }
        
        [DisplayName("HASŁO: ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Password { get; set; }
        public string LoginErrorMessage { get; set; }

    }
}