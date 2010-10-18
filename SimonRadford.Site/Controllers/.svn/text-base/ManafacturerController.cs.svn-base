using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SimonRadford.Site.Models;
using SimonRadford.Site.Repositories;
using SimonRadford.Site.ViewModels;
using MvcContrib.Sorting;
using MvcContrib.Pagination;
using MvcContrib.UI.Grid;

namespace SimonRadford.Site.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ManafacturerController : Controller
    {
        private readonly IManafacturerRepository _manafacturerRepository = new ManafacturerRepository();
        private readonly IProductRepository _productRepository = new ProductRepository();

        public ManafacturerController(IManafacturerRepository manafacturerRepository, IProductRepository productRepository, 
            IReviewRepository reviewReporistory, ISubmitterRepository submitterRepository)
        {
            _manafacturerRepository = manafacturerRepository;
            _productRepository = productRepository;
            
        }

        public ActionResult Index(GridSortOptions sort, int? page)
        {
            IList<Manafacturer> manafacturers = _manafacturerRepository.List();

            ViewData["sort"] = sort;

            var manafacturerRowList = manafacturers.Select(m => new ManafacturerListViewModelRow
            {
                ManafacturerId = m.ManafacturerId,
                ManafacturerName = m.Name
            }).ToList();
            IEnumerable<ManafacturerListViewModelRow> orderedEnumerable = manafacturerRowList.OrderBy(m => m.ManafacturerName); //Sort by manafacturer

            if (sort.Column != null)
                orderedEnumerable = orderedEnumerable.OrderBy(sort.Column, sort.Direction);

            var manafacturerListViewModel = new ManafacturerListViewModel
                                                {
                                                    ManafacturerListRows = orderedEnumerable.AsPagination(page ?? 1, 15)
                                                };

            return View(manafacturerListViewModel);
        }

        [HttpPost]
        public ActionResult Index(GridSortOptions sort, int? page, string searchWord)
        {
            IList<Manafacturer> manafacturers = _manafacturerRepository.Search(searchWord);

            ViewData["sort"] = sort;

            var manafacturerRowList = manafacturers.Select(m => new ManafacturerListViewModelRow
            {
                ManafacturerId = m.ManafacturerId,
                ManafacturerName = m.Name
            }).ToList();
            IEnumerable<ManafacturerListViewModelRow> orderedEnumerable = manafacturerRowList.OrderBy(m => m.ManafacturerName); //Sort by manafacturer

            if (sort.Column != null)
                orderedEnumerable = orderedEnumerable.OrderBy(sort.Column, sort.Direction);

            var manafacturerListViewModel = new ManafacturerListViewModel
                                                {
                                                    SearchWord = searchWord,
                ManafacturerListRows = orderedEnumerable.AsPagination(page ?? 1, 15)
            };

            return View(manafacturerListViewModel);
        }

        public ActionResult AddNewManafacturer()
        {
            var manafacturerViewModel = new ManafacturerViewModel();
            return View(manafacturerViewModel);
        }

        [HttpPost]
        public ActionResult AddNewManafacturer(ManafacturerViewModel model)
        {
            var newManafacturer = new Manafacturer
            {
                Name = model.ManafacturerName,
                Website = model.ManafacturerWebsite
            };
            _manafacturerRepository.Add(newManafacturer);

            return Redirect("/Manafacturer/Index");
        }

        public ActionResult ViewManafacturer(int id, GridSortOptions sort, int? page)
        {
            IList<Product> products = _productRepository.ManafacturerProductList(id);

            ViewData["sort"] = sort;

            var productRowList = products.Select(p => new ProductListViewModelRow
            {
                Id = p.ProductId,
                ProductCode = p.ProductCode,
                ProductName = p.Name,
                Price = p.Price,
            }).ToList();
            IEnumerable<ProductListViewModelRow> sortedEnum = productRowList.OrderBy(p => p.ProductCode); //Sort by product code as default
            if (sort.Column != null)
                sortedEnum = sortedEnum.OrderBy(sort.Column, sort.Direction);

            var manafacturerViewModel = new ManafacturerViewModel
            {
                ManafacturerId = id,
                ManafacturerName = _manafacturerRepository.GetByManafacturerId(id).Name,
                ManafacturerWebsite =
                    _manafacturerRepository.GetByManafacturerId(id).Website,
                ProductListRows = sortedEnum.AsPagination(page ?? 1, 10)
            };

            return View(manafacturerViewModel);
        }

    }
}
