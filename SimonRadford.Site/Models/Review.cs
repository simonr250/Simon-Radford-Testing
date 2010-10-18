using System.ComponentModel.DataAnnotations;

namespace SimonRadford.Site.Models
{
    public class Review
    {
        public virtual int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int UserId { get; set; }
        
        [Required(ErrorMessage = "Please enter a rating between 1 and 5")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public virtual int Rating { get; set; }

        [Required(ErrorMessage = "Please enter your review")]
        public virtual string Detail { get; set; }
    }
}