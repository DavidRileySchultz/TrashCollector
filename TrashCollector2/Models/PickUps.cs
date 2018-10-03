using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrashCollector2.Models
{
    public class PickUps
    {
        [Key]
        public int PickUpId { get; set; }
        public int PickCustomerId { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime? PickUpDate { get; set; }
        public double Cost { get; set; }
        public string Zipcode { get; set; }
        public DateTime? SuspendPickUpStart { get; set; }
        public DateTime? SuspendPickUpEnd { get; set; }
    }
}