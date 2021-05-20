using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceBooking.Models
{
    public class Booking
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public Person Person { get; set; }

        [Required]
        public Local Local { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }
	}
}
