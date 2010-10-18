using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimonRadford.Site.Models;

namespace SimonRadford.Site.ViewModels
{
    public class ReviewListViewModel
    {
        public IList<Review> Reviews { get; set;}
        public IList<Submitter> Submitters { get; set; } 
        public Product Product { get; set;}
        public Manafacturer Manafacturer { get; set; } 
    }
}