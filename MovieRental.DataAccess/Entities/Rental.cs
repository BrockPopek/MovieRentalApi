using System;
using System.ComponentModel.DataAnnotations;

namespace MovieRental.DataAccess.Entities
{
    public class Rental
    {
        public int RentalId { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public Movie Movie { get; set; }

        public DateTime RentedDate { get; set; }

        public DateTime? ReturnedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public string UpdatedBy { get; set; }
    }
}
