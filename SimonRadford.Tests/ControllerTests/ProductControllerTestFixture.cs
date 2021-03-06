﻿using System.Collections.Generic;
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
using MvcContrib.Pagination;

namespace SimonRadford.Tests.ControllerTests
{
    [TestFixture]
    class ProductControllerTestFixture
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

        #region Product Controller Tests

        [Test]
        public void TestProductDetailsViews()
        {   
            var productController = new ProductController( _manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = productController.ProductDetails( 1, new GridSortOptions(),  1 ) as ViewResult;
            if (result != null)
            {
                var productDetailsResult = (ProductViewModel) result.ViewData.Model;
                Assert.AreEqual("TestProduct101",productDetailsResult.ProductName);
                Assert.AreEqual("Test101", productDetailsResult.ProductCode);
                Assert.AreEqual(101, productDetailsResult.Price);
                Assert.AreEqual("TestManafacturer 1", productDetailsResult.ManafacturerName);
                Assert.AreEqual("Testing product 1", productDetailsResult.Description);
                Assert.AreEqual(1, productDetailsResult.AverageRating);
                Assert.AreEqual(1, productDetailsResult.TotalReviewRows);
                Assert.AreEqual("TestSubmitter 1", productDetailsResult.ReviewRows.ElementAt(0).SubmitterName);
                Assert.AreEqual(1, productDetailsResult.ReviewRows.ElementAt(0).Rating);
                Assert.AreEqual("TestReview1", productDetailsResult.ReviewRows.ElementAt(0).DetailedReview);
            }
        }

        [Test]
        public void TestReviewSubmitValid()
        {
            var productController = new ProductController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = (RedirectResult) productController.ProductDetails(new ReviewRowModel("TestSubmitter 6", 5, "TestReview6"), 1);
            if (result != null)
            {
                Assert.AreEqual("/Product/ProductDetails/1", result.Url);
            }
        }

        [Test]
        public void TestProductListView()
        {
            var productController = new ProductController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = productController.Index() as ViewResult;
            if (result != null)
            {
                Assert.AreEqual(0, result.ViewData.Count);
            }
        }

        [Test]
        public void TestProductListSearchView()
        {
            var productController = new ProductController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = productController.Index("101") as ViewResult;
            if (result != null)
            {
                var productSearchResult = (ProductListViewModel)result.ViewData.Model;
                Assert.AreEqual("101", productSearchResult.SearchWord);
            }
        }

        [Test]
        public void TestProductSortSearch()
        {
            var productController = new ProductController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = productController.Sort("ProductCode", 1, "#product_grid", "product/sort/" , "ASC", "ProductGrid", "101" )as ViewResult;
            if (result != null)
            {
                var productSearchResult = (IPagination<ProductListViewModelRow>)result.ViewData["ProductListRows"];
                Assert.AreEqual(1, productSearchResult.Count());
                Assert.AreEqual("Test101", productSearchResult.ElementAt(0).ProductCode);
            }
        }

        [Test]
        public void TestProductSortSearchAsc()
        {
            var productController = new ProductController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = productController.Sort("ProductCode", 1, "#product_grid", "product/sort/", "ASC", "ProductGrid", "Test") as ViewResult;
            if (result != null)
            {
                var productSearchResult = (IPagination<ProductListViewModelRow>)result.ViewData["ProductListRows"];
                Assert.AreEqual(5, productSearchResult.Count());
                Assert.AreEqual("Test101", productSearchResult.ElementAt(0).ProductCode);
                Assert.AreEqual("Test105", productSearchResult.ElementAt(4).ProductCode);
            }
        }

        [Test]
        public void TestProductSortSearchDesc()
        {
            var productController = new ProductController(_manafacturerRepository, _productRepository, _reviewRepository, _submitterRepository);
            var result = productController.Sort("ProductCode", 1, "#product_grid", "product/sort/", "DESC", "ProductGrid", "Test") as ViewResult;
            if (result != null)
            {
                var productSearchResult = (IPagination<ProductListViewModelRow>)result.ViewData["ProductListRows"];
                Assert.AreEqual(5, productSearchResult.Count());
                Assert.AreEqual("Test101", productSearchResult.ElementAt(4).ProductCode);
                Assert.AreEqual("Test105", productSearchResult.ElementAt(0).ProductCode);
            }
        }

        private static bool IsInProductCollection(Product product, IEnumerable<ProductListViewModelRow> fromDb)
        {
            return fromDb.Any(item => product.ProductId == item.Id);
        }

    
        #endregion
    }
}
