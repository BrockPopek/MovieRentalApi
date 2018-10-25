using System;

namespace MovieRental.Contract.Model
{
    public class RentalHistoryModel
    {
        public int RentalId { get; set; }
                
        public MovieModel Movie { get; set; }

        public DateTime RentedDate { get; set; }

        public DateTime? ReturnedDate { get; set; }
    }
}
