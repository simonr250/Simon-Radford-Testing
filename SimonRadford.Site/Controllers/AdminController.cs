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
    public class AdminController : Controller
    {
        private readonly IManafacturerRepository _manafacturerRepository = new ManafacturerRepository();
        private readonly IProductRepository _productRepository = new ProductRepository();

        public AdminController(IManafacturerRepository manafacturerRepository, IProductRepository productRepository, 
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

            return Redirect("/Admin/Index");
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

        public ActionResult EditManafacturer(int id)
        {
            var editManafacturerViewModel = new ManafacturerViewModel
                                                {
                                                 ManafacturerId = id,
                                                  ManafacturerName = _manafacturerRepository.GetByManafacturerId(id).Name,
                                                  ManafacturerWebsite = _manafacturerRepository.GetByManafacturerId(id).Website
                                                };
            return View(editManafacturerViewModel);
        }

        [HttpPost]
        public ActionResult EditManafacturer (ManafacturerViewModel model)
        {
            var updatedManafacturer = new Manafacturer
                                          {
                                              ManafacturerId = model.ManafacturerId,
                                              Name = model.ManafacturerName,
                                              Website = model.ManafacturerWebsite
                                          };
            _manafacturerRepository.Update(updatedManafacturer);
            return Redirect("/Admin/ViewManafacturer?id=" + model.ManafacturerId);
        }

        public ActionResult CreateProduct(int id)
        {
            var productViewModel = new ProductViewModel
            {
                ManafacturerNames = _manafacturerRepository.DistinctNamesList(),
                ManafacturerName = _manafacturerRepository.GetByManafacturerId(id).Name
            };
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel model)
        {
            var newProduct = new Product
            {
                ProductCode = model.ProductCode,
                Name = model.ProductName,
                Price = model.Price,
                Description = model.Description,
                ManafacturerId = _manafacturerRepository.CheckExistingNamesAdd(model.ManafacturerName)
            };

            _productRepository.Add(newProduct);

            return Redirect("/Admin/ViewManafacturer?id="+newProduct.ManafacturerId);
        }

        public ActionResult EditProduct(int id)
        {
            var product = _productRepository.GetByProductId(id);
            var editProductViewModel = new ProductViewModel
                                           {
                                             ProductId = id,
                                             ProductCode = product.ProductCode,
                                             ProductName = product.Name,
                                             ManafacturerName = _manafacturerRepository.GetByManafacturerId(product.ManafacturerId).Name,
                                             ManafacturerNames = _manafacturerRepository.DistinctNamesList(),
                                             Description = product.Description,
                                             Price = product.Price 
                                           };
            return View(editProductViewModel);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModel model)
        {
            var updatedProduct = new Product
                                     {
                                         Description = model.Description,
                                         ManafacturerId = _manafacturerRepository.CheckExistingNamesAdd(model.ManafacturerName),
                                         Name = model.ProductName,
                                         Price = model.Price,
                                         ProductCode = model.ProductCode,
                                         ProductId = model.ProductId
                                     };
            _productRepository.Update(updatedProduct);
            return Redirect("/Admin/ViewManafacturer?id=" + updatedProduct.ManafacturerId);
        }
    }


}
