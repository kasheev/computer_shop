using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasaDate.Models
{
    public class Valute
    {
        public string id { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Previous { get; set;}
    }
}