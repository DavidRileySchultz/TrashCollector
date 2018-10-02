using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrashCollector2.Models
{
    public class AddressViewModel
    {
        public Customer Customer { get; set; }
        public Address Address { get; set; }
    }
}