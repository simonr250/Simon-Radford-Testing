using System.Collections.Generic;

namespace SimonRadford.Site.ViewModels
{
    public class ManafacturerListViewModel
    {
        public ManafacturerListViewModel(IEnumerable<ManafacturerListViewModelRow> manafacturerListRows, string searchWord)
        {
        
            ManafacturerListRows = manafacturerListRows;
            SearchWord = searchWord;
        }

        public ManafacturerListViewModel()
        {
            // TODO: Complete member initialization
        }
        
        public IEnumerable<ManafacturerListViewModelRow> ManafacturerListRows { get; set; }
        public string SearchWord { get; set; }

    }
}