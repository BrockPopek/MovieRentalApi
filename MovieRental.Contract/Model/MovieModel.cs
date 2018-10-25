using System;
using System.ComponentModel.DataAnnotations;
using MovieRental.Contract.Enum;

namespace MovieRental.Contract.Model
{
    public class MovieModel
    {
        public int MovieId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public DateTime? DateAdded { get; set; }

        [Required]
        public int NumberInStock { get; set; } = 10;

        public int NumberAvailable { get; set; } = 10;

        [Required]
        public decimal AmountPerDay { get; set; } = 1.00m;

        [Required]
        [EnumDataType(typeof(GenreType))]
        public GenreType Genre { get; set; } = GenreType.Comedy;
    }
}
