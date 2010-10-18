using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SimonRadford.Site.ViewModels
{
    public class ManafacturerViewModel
    {
        public ManafacturerViewModel(int manafacturerId, string manafacturerName, string manafacturerWebsite, 
            IEnumerable<ProductListViewModelRow> productListRows)
            
        {
            ManafacturerId = manafacturerId;
            ManafacturerName = manafacturerName;
            ManafacturerWebsite = manafacturerWebsite;

            ProductListRows = productListRows;
        }

        public ManafacturerViewModel()
        {

        }

        public int ManafacturerId { get; set; }

        [Required(ErrorMessage = " *** You need to enter a manafacturer name ***")]
        [DisplayName("Manafacturer Name")]
        public string ManafacturerName { get; set; }

        [Required(ErrorMessage = " *** You need to enter a website ***")]
        [DisplayName("Website")]
        public string ManafacturerWebsite { get; set; }

        public IEnumerable<ProductListViewModelRow> ProductListRows { get; set; }
        

    }
}