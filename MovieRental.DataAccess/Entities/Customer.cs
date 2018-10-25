using System;
using System.ComponentModel.DataAnnotations;

namespace MovieRental.DataAccess.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public ApplicationUser ApplicationUser { get; set;} 

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]        
        [MaxLength(2)]
        public string State { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        [Required]
        [Phone]
        public string CellPhone { get; set; }

        [Required]
        public DateTime StartServiceDate { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public string UpdatedBy { get; set; }
    }
}
