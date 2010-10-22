using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimonRadford.Site.ViewModels
{
    public class ReviewRowModel
    {
        public ReviewRowModel(string name, int rating, string detailedReview)
        {
            SubmitterName = name;
            Rating = rating;
            DetailedReview = detailedReview;
        }

        public ReviewRowModel()
        {
            // TODO: Complete member initialization
        }
        [Required(ErrorMessage = " *** You need to enter a name ***")]
        [DisplayName("Your name")]
        public string SubmitterName { get; set; }

        [Required(ErrorMessage = " *** Please enter a rating between 1 and 5 ***")]
        [Range(1, 5, ErrorMessage = " *** Rating must be between 1 and 5 ***")]
        public int Rating { get; set; }

        [Required(ErrorMessage = " *** Please enter your review ***")]
        [DisplayName("Review")]
        public string DetailedReview { get; set; }

        public int Id { get; set; }
        public bool Flagged { get; set; }
    }
}