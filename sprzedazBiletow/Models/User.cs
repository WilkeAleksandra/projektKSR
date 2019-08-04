using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace sprzedazBiletow.Models
{
    public partial class User
    {
        public int UserID { get; set; }
        
        [DisplayName("LOGIN: ")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public String Login { get; set; }
        
        [DisplayName("PASSWORD: ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public String Password { get; set; }
    }
}