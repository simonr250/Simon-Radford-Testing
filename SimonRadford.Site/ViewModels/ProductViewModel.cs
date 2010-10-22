using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimonRadford.Site.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel(int productId, string productCode, string productName, string manafacturerName, double price, string description,
            string submitterName, int rating, string detailedReview, IEnumerable<ReviewRowModel> reviewRows, int totalReviewRows, IList<string> manafacturerNames)
        {
            ProductId = productId;
            ProductCode = productCode;
            ProductName = productName;
            ManafacturerName = manafacturerName;
            Price = price;
            Description = description;

            SubmitterName = submitterName;
            Rating = rating;
            DetailedReview = detailedReview;

            ReviewRows = reviewRows;
            TotalReviewRows = totalReviewRows;

            ManafacturerNames = manafacturerNames;
        }
        public ProductViewModel()
        { }
       
        public int ProductId {get; set;}

        [Required(ErrorMessage = " *** You need to enter a product code ***")]
        [DisplayName("Product Code")]
        public string ProductCode {get; set;}

        [Required(ErrorMessage = " *** You need to enter a product name ***")]
        [DisplayName("Product Name")]
        public string ProductName {get; set;}

        [Required(ErrorMessage = " *** You need to enter a manafacturer name ***")]
        [DisplayName("Manafacturer Name")]
        public string ManafacturerName {get; set;}

        [Required(ErrorMessage = " *** You need to enter a price ***")]
        [DisplayName("Price")]
        public double Price {get; set;}

        [DisplayName("Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = " *** You need to enter a name ***")]
        [DisplayName("Your name")]
        public string SubmitterName { get; set; }

        [Required(ErrorMessage = " *** Please enter a rating between 1 and 5 ***")]
        [Range(1, 5, ErrorMessage = " *** Rating must be between 1 and 5 ***")]
        public int Rating { get; set; }

        [Required(ErrorMessage = " *** Please enter your review ***")]
        [DisplayName("Review")]
        public string DetailedReview { get; set; }
        
        public IEnumerable<ReviewRowModel> ReviewRows { get; set; }

        public int TotalReviewRows { get; set; }
        public double AverageRating { get; set; }

        public IList<string> ManafacturerNames { get; set; }

        public int ManafacturerId { get; set; } 
    }
}