using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimonRadford.Site.Models;
using SimonRadford.Site.Repositories;
using SimonRadford.Site.ViewModels;
using MvcContrib.Sorting;
using MvcContrib.Pagination;
using MvcContrib.UI.Grid;
using SortDirection = MvcContrib.Sorting.SortDirection;

namespace SimonRadford.Site.Controllers
{
    [HandleError]
    public class ProductController : Controller
    {
        private readonly IManafacturerRepository _manafacturerRepository = new ManafacturerRepository();
        private readonly IProductRepository _productRepository = new ProductRepository();
        private readonly IReviewRepository _reviewRepository = new ReviewRepository();
        private readonly ISubmitterRepository _submitterRepository = new SubmitterRepository();

        public ProductController(IManafacturerRepository manafacturerRepository, IProductRepository productRepository, 
            IReviewRepository reviewReporistory, ISubmitterRepository submitterRepository)
        {
            _manafacturerRepository = manafacturerRepository;
            _productRepository = productRepository;
            _reviewRepository = reviewReporistory;
            _submitterRepository = submitterRepository;
        }
        public ActionResult ProductDetails(int id, GridSortOptions sort, int? page)
        {
            var product = _productRepository.GetByProductId(id);

            var reviews = _reviewRepository.List(id);

      //      var total = reviews.Aggregate(0, (current, r) => current + r.Rating);
      //      var avg = 0; 
      //     if(reviews.Count>0)avg = total / reviews.Count;

            ViewData["sort"] = sort;

            var reviewRowList = new List<ReviewRowModel>();
            if (reviews.Count() > 0)
            {
                reviewRowList = reviews.Select(r => new ReviewRowModel
                                                        {
                                                            SubmitterName =
                                                                _submitterRepository.GetByUserId(r.UserId).Name,
                                                            DetailedReview = r.Detail,
                                                            Rating = r.Rating,
                                                            Id = r.Id,
                                                            Flagged = r.Flagged
                                                        }).ToList();
            }
            IEnumerable<ReviewRowModel> orderedEnumerable = reviewRowList.OrderBy(r => r.SubmitterName);
                    //Sort by product code as default
                if (sort.Column != null)
                    orderedEnumerable = orderedEnumerable.OrderBy(sort.Column, sort.Direction);
            
            var productViewModel = new ProductViewModel
                                       {
                                           Description = product.Description,
                                           
                                           
                                           ManafacturerName =
                                               _manafacturerRepository.GetByManafacturerId(product.ManafacturerId).Name,
                                           Price = product.Price,
                                           ProductCode = product.ProductCode,
                                           ProductId = product.ProductId,
                                           ProductName = product.Name,
                                           ReviewRows = orderedEnumerable.AsPagination(page ?? 1, 10),
                                           TotalReviewRows = reviews.Count,
                                           AverageRating = _reviewRepository.GetAverageProductRating(product.ProductId)
                                      };

            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult ProductDetails(ReviewRowModel model, int id)
        {
            var review = new Review {ProductId = id, Rating = model.Rating, Detail = model.DetailedReview, 
                UserId= _submitterRepository.CheckExistingNamesAdd(model.SubmitterName)};
                
            _reviewRepository.Add(review);

            return Redirect("/Product/ProductDetails/" + id);
           
        }

        public ActionResult Index()
        {
            var productListViewModel = new ProductListViewModel();
            return View(productListViewModel);
        }

        [HttpPost]
        public ActionResult Index(string searchWord)
        {
            var productListViewModel = new ProductListViewModel
            {
                SearchWord = searchWord
            };

            return View(productListViewModel);
        }

        public ActionResult Sort(string ColumnName, int? PageNum, string GridDiv, string Controller, string Direction, string GridView, string SearchWord)
        {
       
            int page = PageNum ?? 1;
            const string defaultColumn = "ProductCode";
            string column = (ColumnName ?? defaultColumn) == "undefined" ? defaultColumn :
                (ColumnName == String.Empty) ? defaultColumn : ColumnName;
            
            var direction = new SortDirection();
            if (Direction == "ASC") direction = SortDirection.Ascending;
            if (Direction == "DESC") direction = SortDirection.Descending;

            List<Product> products;
            if (SearchWord != "")
            {
                var manafacturerIdList = _manafacturerRepository.Search(SearchWord).Select(m => m.ManafacturerId).ToList();
                products = _productRepository.Search(SearchWord, manafacturerIdList) as List<Product>;
            }
            else
            {
                products = _productRepository.List() as List<Product>;    
            }


            if (products != null)
            {
                var productRowList = products.Select(p => new ProductListViewModelRow
                                                              {
                                                                  Id = p.ProductId,
                                                                  ProductCode = p.ProductCode,
                                                                  ProductName = p.Name,
                                                                  Price = p.Price,
                                                                  ManafacturerName = _manafacturerRepository.GetByManafacturerId(p.ManafacturerId).Name
                                                              }).ToList().OrderBy(column, direction).AsPagination(page, 15);
            
                ViewData["ProductListRows"] = productRowList;
            }
            ViewData["controller"] = Controller;
            ViewData["griddiv"] = GridDiv;
            return View(GridView);
        }

        public JavaScriptResult FlagReview(int id)
        {
            var script = string.Format("");
            if (_reviewRepository.GetById(id).Flagged)
            {
                script = string.Format("alert('This review has already been reported to the administrators')");
            }
            else
            {
                var review = _reviewRepository.GetById(id);
                review.Flagged = true;
                _reviewRepository.Update(review);
                script = string.Format("alert('Thankyou, this review has now been reported to the administrators')");
            }
          
            return JavaScript(script);
        }
    }
}