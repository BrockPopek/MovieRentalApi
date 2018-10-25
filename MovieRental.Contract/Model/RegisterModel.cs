using System;
using System.ComponentModel.DataAnnotations;
using MovieRental.Contract.Enum;

namespace MovieRental.Contract.Model
{
    public class RegisterModel : LoginModel
    {
        /// <summary>
        /// First Name
        /// </summary>
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        [Required]
        [StringLength(250)]
        public string LastName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)
        /// </summary>
        [Required]
        [Compare("Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string PasswordConfirmation { get; set; }

        /// <summary>
        /// Role of the user. 0 = Manager, 1 = Customer.
        /// </summary>
        [Required]
        [EnumDataType(typeof(RoleType), ErrorMessage = "UserRole must be 0 (Manager) or 1 (Customer)")]
        public RoleType UserRole { get; set; }
    }
}
