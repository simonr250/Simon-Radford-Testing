using System.Collections.Generic;

namespace SimonRadford.Site.ViewModels
{
    public class ReportedReviewsViewModel
    {
        public ReportedReviewsViewModel(IEnumerable<ReviewRowModel> reportedReviewListRows, string searchWord)
        {

            ReportedReviewListRows = reportedReviewListRows;
            SearchWord = searchWord;
        }

        public ReportedReviewsViewModel()
        {

            
        }

        public IEnumerable<ReviewRowModel> ReportedReviewListRows { get; set; }

        public string SearchWord { get; set; }

    }
}