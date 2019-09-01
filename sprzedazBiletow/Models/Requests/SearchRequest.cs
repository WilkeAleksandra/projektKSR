using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sprzedazBiletow.Models
{
    public partial class SearchRequest
    {
        public List<SelectListItem> cities { get; set; }

        [DisplayName("STACJA POCZĄTKOWA: ")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string startStation { get; set; }

        [DisplayName("STACJA KOŃCOWA: ")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string endStation { get; set; }

        [DisplayName("DATA: ")]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public DateTime date { get; set; }

        //[DisplayName("GODZINA: ")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: HH:mm}")]
        //public DateTime hour { get; set; }
        public string SearchErrorMessage { get; set; }

    }
}