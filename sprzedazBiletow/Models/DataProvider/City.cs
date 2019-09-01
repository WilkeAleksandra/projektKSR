using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sprzedazBiletow.Models
{
    public class City
    {
        public City(int Id, string name)
        {
            this.Id = Id;
            this.Name = name;
            this.isSelected = false;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isSelected { get; set; }
    }
}