using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceBooking.Models
{
    public class LocalUse
    {

        [Key]
        public long Id { get; set; }

        [Required]
        public Booking Booking { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public DateTime Leave { get; set; }
    }
}
