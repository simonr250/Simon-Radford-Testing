using System.Collections.Generic;

namespace SimonRadford.Site.ViewModels
{
    public class ProductListViewModel
    {
        public ProductListViewModel(IEnumerable<ProductListViewModelRow> productListRows, string searchWord)
        {

            ProductListRows = productListRows;
            SearchWord = searchWord;
        }

        public ProductListViewModel()
        {
            // TODO: Complete member initialization
        }

        public IEnumerable<ProductListViewModelRow> ProductListRows { get; set; }

        public string SearchWord { get; set; }
        
    }
}