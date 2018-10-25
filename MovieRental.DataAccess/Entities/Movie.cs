using System;
using System.ComponentModel.DataAnnotations;
using MovieRental.Contract.Enum;

namespace MovieRental.DataAccess.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public DateTime? DateAdded { get; set; }

        [Required]
        public int NumberInStock { get; set; }

        public int NumberAvailable { get; set; }

        public GenreType Genre { get; set; }

        [Required]
        public decimal AmountPerDay { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public string UpdatedBy { get; set; }
    }
}
