using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sprzedazBiletow.Models
{
    public class SerachResponseView
    {
        public List<SearchResponse> list { get; set; }
        public List<SelectListItem> tickets { get; set; }
    }
}