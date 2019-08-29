using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sprzedazBiletow.Models
{
    public class Cities
    {
        public readonly List<City> list
            = new List<City>(){
                (new City(1,"Warszawa")),
                (new City(2, "Kutno")),
                (new City(3, "Konin")),
                (new City(4, "Poznań")),
                (new City(5, "Szczecin")),
                (new City(6, "Stargard")),
                (new City(7, "Łobez")),
                (new City(8, "Białogard")),
                (new City(9, "Koszalin")),
                (new City(10, "Słupsk")),
                (new City(11, "Wejherowo")),
                (new City(12, "Gdańsk"))
        };
    }
}