using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector2.Models
{
    public class AddressViewModel
    {
        public Employee employee { get; set; }
        public Customer customer { get; set; }
        public Address address { get; set; }
        public PickUps pickUps { get; set; }
    }
}