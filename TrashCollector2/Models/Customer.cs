using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrashCollector2.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }

        [ForeignKey("Address")]
        [Display(Name = "Address")]
        public int AddressID { get; set; }
        public Address Address { get; set; }
        
        [ForeignKey("PickUps")]
        public int PickId { get; set; }
        public PickUps PickUps { get; set; }
        public string UserName { get; set; }
        public double AccountBalance { get; set; }


        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}