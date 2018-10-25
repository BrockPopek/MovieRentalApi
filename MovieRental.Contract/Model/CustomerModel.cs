using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MovieRental.Contract.Model
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        
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

        #region ApplicationUser Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        # endregion ApplicationUser Properties
    }
}
