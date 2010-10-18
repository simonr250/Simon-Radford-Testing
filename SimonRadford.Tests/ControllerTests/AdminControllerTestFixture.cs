using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NHibernate;
using SimonRadford.Site.Models;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SimonRadford.Site.Repositories;
using SimonRadford.Site.Controllers;
using SimonRadford.Site.ViewModels;
using MvcContrib.UI.Grid;

namespace SimonRadford.Tests.ControllerTests
{
    [TestFixture]
    class AdminControllerTestFixture
    {
        private readonly IManafacturerRepository _manafacturerRepository = new ManafacturerRepository();
        private readonly IProductRepository _productRepository = new ProductRepository();
        private readonly IReviewRepository _reviewRepository = new ReviewRepository();
        private readonly ISubmitterRepository _submitterRepository = new SubmitterRepository();

        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        #region Initialize Test Data

        private readonly Manafacturer[] _manafacturers = new[]
                 {
                     new Manafacturer { Name = "TestManafacturer 1" , Website = "www.testco1.co.uk"},
                     new Manafacturer { Name = "TestManafacturer 2" , Website = "www.testco2.co.uk"},
                     new Manafacturer { Name = "TestManafacturer 3" , Website = "www.testco3.co.uk"},
                     new Manafacturer { Name = "TestManafacturer 4" , Website = "www.testco4.co.uk"},
                     new Manafacturer { Name = "TestManafacturer 5" , Website = "www.testco5.co.uk"},
                 };

        private readonly Product[] _products = new[]
                 {
                     new Product { ProductCode = "Test101" , Name = "TestProduct101", Description = "Testing product 1" , Price = 101 , ManafacturerId = 1},
                     new Product { ProductCode = "Test102" , Name = "TestProduct102", Description = "Testing product 2" , Price = 102 , ManafacturerId = 2},
                     new Product { ProductCode = "Test103" , Name = "TestProduct103", Description = "Testing product 3" , Price = 103 , ManafacturerId = 3},
                     new Product { ProductCode = "Test104" , Name = "TestProduct104", Description = "Testing product 4" , Price = 104 , ManafacturerId = 4},
                     new Product { ProductCode = "Test105" , Name = "TestProduct105", Description = "Testing product 5" , Price = 105 , ManafacturerId = 5}
                 };

        private readonly Submitter[] _submitters = new[]
                 {
                     new Submitter { Name = "TestSubmitter 1"},
                     new Submitter { Name = "TestSubmitter 2"},
                     new Submitter { Name = "TestSubmitter 3"},
                     new Submitter { Name = "TestSubmitter 4"},
                     new Submitter { Name = "TestSubmitter 5"},
                 };

        private readonly Review[] _reviews = new[]
                {
                    new Review { Detail ="TestReview1", ProductId = 1, Rating = 1, UserId = 1},
                    new Review { Detail ="TestReview2", ProductId = 2, Rating = 2, UserId = 2},
                    new Review { Detail ="TestReview3", ProductId = 3, Rating = 3, UserId = 3},
                    new Review { Detail ="TestReview4", ProductId = 4, Rating = 4, UserId = 4},
                    new Review { Detail ="TestReview5", ProductId = 5, Rating = 5, UserId = 5},
                };

        #endregion

        #region Initialize NHIbernate

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _configuration = new Configuration();
            _configuration.Configure("hibernate.cfg.xml");
            _configuration.AddAssembly(typeof(Product).Assembly);
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        #endregion

        #region Create DB and insert test data

        [SetUp]
        public void SetupContext()
        {
            new SchemaExport(_configuration).Execute(false, true, false);
            CreateInitialData();
        }
        private void CreateInitialData()
        {

            using (ISession session = _sessionFactory.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                foreach (var manafacturer in _manafacturers)
                    session.Save(manafacturer);
                foreach (var product in _products)
                    session.Save(product);
                foreach (var submitter in _submitters)
                    session.Save(submitter);
                foreach (var review in _reviews)
                    session.Save(review);
                transaction.Commit();
            }
        }

        #endregion

        #region AdminControllerTests

        [Test]
        public void TestManafacturerListView()
        {
            var adminController = new AdminController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = adminController.Index(new GridSortOptions(), 1) as ViewResult;
            if (result != null)
            {
                var manafacturerListResult = (ManafacturerListViewModel)result.ViewData.Model;
                Assert.AreEqual(5, manafacturerListResult.ManafacturerListRows.Count());

                Assert.IsTrue(IsInManafacturerCollection(_manafacturers[0], manafacturerListResult.ManafacturerListRows));
                Assert.IsTrue(IsInManafacturerCollection(_manafacturers[1], manafacturerListResult.ManafacturerListRows));
                Assert.IsTrue(IsInManafacturerCollection(_manafacturers[2], manafacturerListResult.ManafacturerListRows));
                Assert.IsTrue(IsInManafacturerCollection(_manafacturers[3], manafacturerListResult.ManafacturerListRows));
                Assert.IsTrue(IsInManafacturerCollection(_manafacturers[4], manafacturerListResult.ManafacturerListRows));

            }
        }

        [Test]
        public void TestCreateManafacturerView()
        {
            var adminController = new AdminController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = adminController.AddNewManafacturer() as ViewResult;
            if (result != null)
            {
                var createManafacturerResult = (ManafacturerViewModel)result.ViewData.Model;
                Assert.AreEqual(0, createManafacturerResult.ManafacturerId);
                Assert.IsNullOrEmpty(createManafacturerResult.ManafacturerName);
                Assert.IsNullOrEmpty(createManafacturerResult.ManafacturerWebsite);

            }
        }

        [Test]
        public void TestCreateManafacturerSubmit()
        {
            var adminController = new AdminController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = (RedirectResult) adminController.AddNewManafacturer(new ManafacturerViewModel
            {
                ManafacturerName = "TestManafacturer 6",
                ManafacturerWebsite = "www.testco6.co.uk"
            });
            if (result != null)
            {
                Assert.AreEqual("/Admin/Index", result.Url);
            }
        }

        [Test]
        public void TestViewManafacturerDetails()
        {
            var adminController = new AdminController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result =  adminController.ViewManafacturer(1, new GridSortOptions(), 1) as ViewResult;
            if (result != null)
            {
                var manafacturerViewResult = (ManafacturerViewModel) result.ViewData.Model;
                Assert.AreEqual(manafacturerViewResult.ManafacturerId, _manafacturers[0].ManafacturerId);
                Assert.AreEqual(manafacturerViewResult.ManafacturerName, _manafacturers[0].Name);
                Assert.AreEqual(manafacturerViewResult.ManafacturerWebsite, _manafacturers[0].Website);
                Assert.IsTrue(IsInProductCollection(_products[0], manafacturerViewResult.ProductListRows));
            }
        }

        [Test]
        public void TestCreateProductView()
        {
            var adminController = new AdminController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = adminController.CreateProduct(1) as ViewResult;
            if (result != null)
            {
                var createProductResult = (ProductViewModel)result.ViewData.Model;
                Assert.AreEqual(0, createProductResult.ProductId);
                Assert.IsNullOrEmpty(createProductResult.ProductCode);
                Assert.IsNullOrEmpty(createProductResult.ProductName);
                Assert.IsNullOrEmpty(createProductResult.Description);
                Assert.AreEqual(0, createProductResult.Price);
                Assert.AreEqual(0, createProductResult.AverageRating);
                Assert.AreEqual(0, createProductResult.TotalReviewRows);
                Assert.AreEqual("TestManafacturer 1", createProductResult.ManafacturerName);
                Assert.AreEqual("TestManafacturer 1", createProductResult.ManafacturerNames[0]);
                Assert.AreEqual("TestManafacturer 2", createProductResult.ManafacturerNames[1]);
                Assert.AreEqual("TestManafacturer 3", createProductResult.ManafacturerNames[2]);
                Assert.AreEqual("TestManafacturer 4", createProductResult.ManafacturerNames[3]);
                Assert.AreEqual("TestManafacturer 5", createProductResult.ManafacturerNames[4]);
            }
        }

        [Test]
        public void TestCreateProductSubmit()
        {
            var adminController = new AdminController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = (RedirectResult)adminController.CreateProduct(new ProductViewModel
            {
                ProductCode = "Test106",
                ProductName = "TestProduct106",
                Description = "Testing product 6",
                Price = 106,
                ManafacturerName = "TestManafacturer 1"
            });
            if (result != null)
            {
                Assert.AreEqual("/Admin/ViewManafacturer?id=1", result.Url);
            }

        }

        [Test]
        public void TestEditProduct()
        {
            var adminController = new AdminController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = adminController.EditProduct(1) as ViewResult;
            if (result != null)
            {
                var editProductResult = (ProductViewModel) result.ViewData.Model;
                Assert.AreEqual("Test101", editProductResult.ProductCode);
                Assert.AreEqual("TestProduct101", editProductResult.ProductName);
                Assert.AreEqual(101, editProductResult.Price);
                Assert.AreEqual("Testing product 1", editProductResult.Description);
                Assert.AreEqual("TestManafacturer 1", editProductResult.ManafacturerName);
            }
        }

        [Test]
        public void TestEditProductSave()
        {
            var adminController = new AdminController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = (RedirectResult) adminController.EditProduct(new ProductViewModel
                                                                          {
                                                                              ProductId = 1,
                                                                              ProductCode = "Test101Edit",
                                                                              ProductName = "TestProduct101Edit",
                                                                              Price = 10101,
                                                                              Description = "Testing product 1 edit",
                                                                              ManafacturerName = "TestManafacturer 1",
                                                                          }
                                                                          );
            if (result != null)
            {
                Assert.AreEqual("/Admin/ViewManafacturer?id=1", result.Url);
            }
        }

        [Test]
        public void TestEditManafacturer()
        {
            var adminController = new AdminController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = adminController.EditManafacturer(1) as ViewResult;
            if (result != null)
            {
                var editProductResult = (ManafacturerViewModel)result.ViewData.Model;
                Assert.AreEqual("TestManafacturer 1", editProductResult.ManafacturerName);
                Assert.AreEqual("www.testco1.co.uk", editProductResult.ManafacturerWebsite);
            }
        }

        [Test]
        public void TestEditManafacturerSave()
        {
            var adminController = new AdminController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = (RedirectResult)adminController.EditManafacturer(new ManafacturerViewModel
            {
                ManafacturerId = 1,
                ManafacturerName = "TestManafacturer 1 edit",
                ManafacturerWebsite = "www.testco1.co.uk_edit"
            });                                                                          
            if (result != null)
            {
                Assert.AreEqual("/Admin/ViewManafacturer?id=1", result.Url);
            }
        }

        private static bool IsInManafacturerCollection(Manafacturer manafacturer, IEnumerable<ManafacturerListViewModelRow> fromDb)
        {
            return fromDb.Any(item => manafacturer.ManafacturerId == item.ManafacturerId);
        }
        private static bool IsInProductCollection(Product product, IEnumerable<ProductListViewModelRow> fromDb)
        {
            return fromDb.Any(item => product.ProductId == item.Id);
        }


        #endregion
    }
}