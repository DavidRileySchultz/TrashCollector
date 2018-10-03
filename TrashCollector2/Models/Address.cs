using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector2.Models
{
    public class Address
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Street Address")]
        public string Address1 { get; set; }
        [Display(Name = "Apt/Suite Number")]
        public string Address2 { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string Zipcode { get; set; }
    }
}