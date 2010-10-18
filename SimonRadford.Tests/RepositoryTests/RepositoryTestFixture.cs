using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using SimonRadford.Site.Models;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using SimonRadford.Site.Repositories;


namespace ProductReviewTestProject
{
    [TestFixture]
    public class RepositoryTestFixture
    {
        private ISessionFactory _sessionFactory;
        private Configuration _configuration;

        #region Initialize Test Data

        private readonly Manafacturer[] _manafacturers = new[]
                 {
                     new Manafacturer() { Name = "TestManafacturer 1" , Website = "www.testco1.co.uk"},
                     new Manafacturer() { Name = "TestManafacturer 2" , Website = "www.testco2.co.uk"},
                     new Manafacturer() { Name = "TestManafacturer 3" , Website = "www.testco3.co.uk"},
                     new Manafacturer() { Name = "TestManafacturer 4" , Website = "www.testco4.co.uk"},
                     new Manafacturer() { Name = "TestManafacturer 5" , Website = "www.testco5.co.uk"},
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
                     new Submitter() { Name = "TestSubmitter 1"},
                     new Submitter() { Name = "TestSubmitter 2"},
                     new Submitter() { Name = "TestSubmitter 3"},
                     new Submitter() { Name = "TestSubmitter 4"},
                     new Submitter() { Name = "TestSubmitter 5"},
                 };

        private readonly Review[] _reviews = new[]
                {
                    new Review() { Detail ="TestReview1", ProductId = 1, Rating = 1, UserId = 1},
                    new Review() { Detail ="TestReview2", ProductId = 2, Rating = 2, UserId = 2},
                    new Review() { Detail ="TestReview3", ProductId = 3, Rating = 3, UserId = 3},
                    new Review() { Detail ="TestReview4", ProductId = 4, Rating = 4, UserId = 4},
                    new Review() { Detail ="TestReview5", ProductId = 5, Rating = 5, UserId = 5},
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

        #region Manafacturer Repository Testing

        [Test]
        public void CanAddNewManafacturer()
        {
            var manafacturer = new Manafacturer
            {
                Name = "TestManafacturer 6",
                Website = "www.testco6.co.uk"
            };

            IManafacturerRepository repository = new ManafacturerRepository();
            repository.Add(manafacturer);

            // use session to try to load the Manafacturer
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Manafacturer>(manafacturer.ManafacturerId);
                // Test that the Manafacturer was successfully inserted
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(manafacturer, fromDb);
                Assert.AreEqual(manafacturer.Name, fromDb.Name);
                Assert.AreEqual(manafacturer.Website, fromDb.Website);
            }
        }

        [Test]
        public void CanUpdateExistingManafacturer()
        {
            if (_manafacturers != null)
            {
                var manafacturer = _manafacturers[0];
                manafacturer.Name = "TestManafacturerRename";
                manafacturer.Website = "www.TestManafacturerWebsiteChange.co.uk";

                IManafacturerRepository repository = new ManafacturerRepository();
                repository.Update(manafacturer);

                // use session to try to load the Manafacturer
                using (ISession session = _sessionFactory.OpenSession())
                {
                    var fromDb = session.Get<Manafacturer>(manafacturer.ManafacturerId);
                    Assert.AreEqual(manafacturer.Name, fromDb.Name);
                    Assert.AreEqual(manafacturer.Website, fromDb.Website);
                }
            }
        }

        [Test]
        public void CanRemoveExistingManafacturer()
        {
            var manafacturer = _manafacturers[0];
            IManafacturerRepository repository = new ManafacturerRepository();
            repository.Remove(manafacturer);

            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Manafacturer>(manafacturer.ManafacturerId);
                Assert.IsNull(fromDb);
            }
        }

        [Test]
        public void CanGetExistingManafacturerById()
        {
            IManafacturerRepository repository = new ManafacturerRepository();
            var fromDb = repository.GetByManafacturerId(_manafacturers[1].ManafacturerId);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_manafacturers[1], fromDb);
            Assert.AreEqual(_manafacturers[1].Name, fromDb.Name);
        }

        [Test]
        public void CanGetExistingManafacturerByManafacturerName()
        {
            IManafacturerRepository repository = new ManafacturerRepository();
            var fromDb = repository.GetByName(_manafacturers[1].Name);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_manafacturers[1], fromDb);
            Assert.AreEqual(_manafacturers[1].ManafacturerId, fromDb.ManafacturerId);
        }

        [Test]
        public void CanGetExistingManafacturerList()
        {
            IManafacturerRepository repository = new ManafacturerRepository();
            var fromDb = repository.List();

            Assert.AreEqual(5, fromDb.Count);
            Assert.IsTrue(IsInManafacturerCollection(_manafacturers[0], fromDb));
            Assert.IsTrue(IsInManafacturerCollection(_manafacturers[1], fromDb));
            Assert.IsTrue(IsInManafacturerCollection(_manafacturers[2], fromDb));
            Assert.IsTrue(IsInManafacturerCollection(_manafacturers[3], fromDb));
            Assert.IsTrue(IsInManafacturerCollection(_manafacturers[4], fromDb));

        }

        [Test]
        public void CanGetDistinctManafacturerNameList()
        {
            IManafacturerRepository repository = new ManafacturerRepository();
            var fromDb = repository.DistinctNamesList();

            Assert.AreEqual(5, fromDb.Count);
            Assert.IsTrue(IsInManafacturerNameCollection(_manafacturers[0].Name, fromDb));
            Assert.IsTrue(IsInManafacturerNameCollection(_manafacturers[1].Name, fromDb));
            Assert.IsTrue(IsInManafacturerNameCollection(_manafacturers[2].Name, fromDb));
            Assert.IsTrue(IsInManafacturerNameCollection(_manafacturers[3].Name, fromDb));
            Assert.IsTrue(IsInManafacturerNameCollection(_manafacturers[4].Name, fromDb));

        }

        [Test]
        public void CanCheckAndAddManafacturerWhereNotExisting()
        {
            var manafacturer = new Manafacturer
            {
                Name = "TestManafacturer 7",
                Website = "www.testco7.co.uk"
            };
            IManafacturerRepository repository = new ManafacturerRepository();
            var fromDb = repository.DistinctNamesList();
            Assert.IsFalse(IsInManafacturerNameCollection(manafacturer.Name, fromDb));

            var newManFromDb = repository.GetByManafacturerId(repository.CheckExistingNamesAdd(manafacturer.Name));
            Assert.IsTrue(manafacturer.Name == newManFromDb.Name);
        }

        [Test]
        public void CanCheckAndAddManafacturerWhereExisting()
        {
            var manafacturer = new Manafacturer
            {
                Name = "TestManafacturer 5",
                Website = "www.testco5.co.uk"
            };
            IManafacturerRepository repository = new ManafacturerRepository();
            var fromDb = repository.DistinctNamesList();
            Assert.IsTrue(IsInManafacturerNameCollection(manafacturer.Name, fromDb));
            var newManFromDb = repository.GetByManafacturerId(repository.CheckExistingNamesAdd(manafacturer.Name));
            Assert.IsTrue(manafacturer.Name == newManFromDb.Name);
        }

        private static bool IsInManafacturerCollection(Manafacturer manafacturer, IEnumerable<Manafacturer> fromDb)
        {
            return fromDb.Any(item => manafacturer.ManafacturerId == item.ManafacturerId);
        }

        private static bool IsInManafacturerNameCollection(string manafacturerName, IEnumerable<string> fromDb)
        {
            return fromDb.Any(item => manafacturerName == item);
        }

        #endregion

        #region Product Repository Testing

        [Test]
        public void CanAddNewProduct()
        {
            var product = new Product
                              {
                                  ProductCode = "Test106",
                                  Name = "TestProduct106",
                                  Description = "Testing product 6",
                                  Price = 106,
                                  ManafacturerId = 1
                              };

                IProductRepository repository = new ProductRepository();
                repository.Add(product);

                // use session to try to load the product
                using (ISession session = _sessionFactory.OpenSession())
                {
                    var fromDb = session.Get<Product>(product.ProductId);
                    // Test that the product was successfully inserted
                    Assert.IsNotNull(fromDb);
                    Assert.AreNotSame(product, fromDb);
                    Assert.AreEqual(product.Name, fromDb.Name);
                    Assert.AreEqual(product.Description, fromDb.Description);
                    Assert.AreEqual(product.Price, fromDb.Price);
                   
                }
            }
        
        [Test]
        public void CanUpdateExistingProduct()
        {
            if (_products != null)
            {
                var product = _products[0];
                product.Name = "TestProductRename";
                product.Description = "TestProductDescriptionChange";
                product.Price = 99;

                IProductRepository repository = new ProductRepository();
                repository.Update(product);

                // use session to try to load the product
                using (ISession session = _sessionFactory.OpenSession())
                {
                    var fromDb = session.Get<Product>(product.ProductId);
                    Assert.AreEqual(product.Name, fromDb.Name);
                    Assert.AreEqual(product.Description, fromDb.Description);
                    Assert.AreEqual(product.Price, fromDb.Price);
                }
            }
        }

        [Test]
        public void CanRemoveExistingProduct()
        {
            var product = _products[0];
            IProductRepository repository = new ProductRepository();
            repository.Remove(product);

            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Product>(product.ProductId);
                Assert.IsNull(fromDb);
            }
        }

        [Test]
        public void CanGetExistingProductById()
        {
            IProductRepository repository = new ProductRepository();
            var fromDb = repository.GetByProductId(_products[1].ProductId);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_products[1], fromDb);
            Assert.AreEqual(_products[1].Name, fromDb.Name);
        }

        [Test]
        public void CanGetExistingProductByProductName()
        {
            IProductRepository repository = new ProductRepository();
            var fromDb = repository.GetByProductName(_products[1].Name);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_products[1], fromDb);
            Assert.AreEqual(_products[1].ProductId, fromDb.ProductId);
        }

        [Test]
        public void CanGetExistingProductByProductCode()
        {
            IProductRepository repository = new ProductRepository();
            var fromDb = repository.GetByProductCode(_products[1].ProductCode);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_products[1], fromDb);
            Assert.AreEqual(_products[1].ProductId, fromDb.ProductId);
        }

        [Test]
        public void CanGetExistingProductList()
        {
            IProductRepository repository = new ProductRepository();
            var fromDb = repository.List();

            Assert.AreEqual(5, fromDb.Count);
            Assert.IsTrue(IsInProductCollection(_products[0], fromDb));
            Assert.IsTrue(IsInProductCollection(_products[1], fromDb));
            Assert.IsTrue(IsInProductCollection(_products[2], fromDb));
            Assert.IsTrue(IsInProductCollection(_products[3], fromDb));
            Assert.IsTrue(IsInProductCollection(_products[4], fromDb));
            
        }

        [Test]
        public void CanGetExistingProductsByManafacturer()
        {
            IProductRepository repository = new ProductRepository();
            var fromDb = repository.ManafacturerProductList(2);

            Assert.AreEqual(1, fromDb.Count);
            Assert.IsTrue(IsInProductCollection(_products[1], fromDb));
        }

        [Test]
        public void CanSearchByName()
        {
            IProductRepository repository = new ProductRepository();
            var fromDb = repository.Search("101", new List<int>());

            Assert.AreEqual(1, fromDb.Count);
            Assert.IsTrue(IsInProductCollection(_products[0], fromDb));
        }

        private static bool IsInProductCollection(Product product, IEnumerable<Product> fromDb)
        {
            return fromDb.Any(item => product.ProductId == item.ProductId);
        }

        #endregion

        #region Submitter Repository Testing

        [Test]
        public void CanAddNewSubmitter()
        {
            var submitter = new Submitter
            {
                Name = "TestSubmitter6",
            };

            ISubmitterRepository repository = new SubmitterRepository();
            repository.Add(submitter);

            // use session to try to load the Submitter
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Submitter>(submitter.UserId);
                // Test that the Submitter was successfully inserted
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(submitter, fromDb);
                Assert.AreEqual(submitter.Name, fromDb.Name);
            }
        }

        [Test]
        public void CanUpdateExistingSubmitter()
        {
            if (_submitters != null)
            {
                var submitter = _submitters[0];
                submitter.Name = "TestSubmitterRename";

                ISubmitterRepository repository = new SubmitterRepository();
                repository.Update(submitter);

                // use session to try to load the Submitter
                using (ISession session = _sessionFactory.OpenSession())
                {
                    var fromDb = session.Get<Submitter>(submitter.UserId);
                    Assert.AreEqual(submitter.Name, fromDb.Name);
                }
            }
        }

        [Test]
        public void CanRemoveExistingSubmitter()
        {
            var submitter = _submitters[0];
            ISubmitterRepository repository = new SubmitterRepository();
            repository.Remove(submitter);

            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Submitter>(submitter.UserId);
                Assert.IsNull(fromDb);
            }
        }

        [Test]
        public void CanGetExistingSubmitterById()
        {
            ISubmitterRepository repository = new SubmitterRepository();
            var fromDb = repository.GetByUserId(_submitters[1].UserId);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_submitters[1], fromDb);
            Assert.AreEqual(_submitters[1].Name, fromDb.Name);
        }

        [Test]
        public void CanGetExistingSubmitterBySubmitterName()
        {
            ISubmitterRepository repository = new SubmitterRepository();
            var fromDb = repository.GetByName(_submitters[1].Name);

            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_submitters[1], fromDb);
            Assert.AreEqual(_submitters[1].UserId, fromDb.UserId);
        }

        [Test]
        public void CanGetExistingSubmitterList()
        {
            ISubmitterRepository repository = new SubmitterRepository();
            var fromDb = repository.List();

            Assert.AreEqual(5, fromDb.Count);
            Assert.IsTrue(IsInSubmitterCollection(_submitters[0], fromDb));
            Assert.IsTrue(IsInSubmitterCollection(_submitters[1], fromDb));
            Assert.IsTrue(IsInSubmitterCollection(_submitters[2], fromDb));
            Assert.IsTrue(IsInSubmitterCollection(_submitters[3], fromDb));
            Assert.IsTrue(IsInSubmitterCollection(_submitters[4], fromDb));

        }

        [Test]
        public void CanCheckAndAddSubmitterWhereNotExisting()
        {
            var submitter = new Submitter
            {
                Name = "TestSubmitter 7",
            };
            ISubmitterRepository repository = new SubmitterRepository();
            var fromDb = repository.List();
            Assert.IsFalse(IsInSubmitterNameCollection(submitter, fromDb));

            var newSubFromDb = repository.GetByUserId(repository.CheckExistingNamesAdd(submitter.Name));
            Assert.IsTrue(submitter.Name == newSubFromDb.Name);
        }

        [Test]
        public void CanCheckAndAddSubmitterWhereExisting()
        {
            var submitter = new Submitter
            {
                Name = "TestSubmitter 5",
            };
            ISubmitterRepository repository = new SubmitterRepository();
            var fromDb = repository.List();
            Assert.IsTrue(IsInSubmitterNameCollection(submitter, fromDb));
            var newSubFromDb = repository.GetByUserId(repository.CheckExistingNamesAdd(submitter.Name));
            Assert.IsTrue(submitter.Name == newSubFromDb.Name);
        }

        private static bool IsInSubmitterCollection(Submitter submitter, IEnumerable<Submitter> fromDb)
        {
            return fromDb.Any(item => submitter.UserId == item.UserId);
        }

        private static bool IsInSubmitterNameCollection(Submitter submitter, IEnumerable<Submitter> fromDb)
        {
            return fromDb.Any(item => submitter.Name == item.Name);
        }

        #endregion

        #region Review RepositoryTesting

        [Test]
        public void CanAddNewReview()
        {
            var review = new Review
            {
                Detail = "TestReview6",
                Rating = 5,
                ProductId = 1,
                UserId = 1,
            };

            IReviewRepository repository = new ReviewRepository();
            repository.Add(review);

            // use session to try to load the Review
            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Review>(review.Id);
                // Test that the Review was successfully inserted
                Assert.IsNotNull(fromDb);
                Assert.AreNotSame(review, fromDb);
                Assert.AreEqual(review.ProductId, fromDb.ProductId);
                Assert.AreEqual(review.UserId, fromDb.UserId);
                Assert.AreEqual(review.Detail, fromDb.Detail);
                Assert.AreEqual(review.Rating, fromDb.Rating);
            }
        }

        [Test]
        public void CanUpdateExistingReview()
        {
            if (_reviews != null)
            {
                var review = _reviews[0];
                review.Detail = "TestReviewUpdate";
                review.Rating = 1;

                IReviewRepository repository = new ReviewRepository();
                repository.Update(review);

                // use session to try to load the Review
                using (ISession session = _sessionFactory.OpenSession())
                {
                    var fromDb = session.Get<Review>(review.Id);
                    Assert.AreEqual(review.ProductId, fromDb.ProductId);
                    Assert.AreEqual(review.UserId, fromDb.UserId);
                    Assert.AreEqual(review.Detail, fromDb.Detail);
                    Assert.AreEqual(review.Rating, fromDb.Rating);
                }
            }
        }

        [Test]
        public void CanRemoveExistingReview()
        {
            var review = _reviews[0];
            IReviewRepository repository = new ReviewRepository();
            repository.Remove(review);

            using (ISession session = _sessionFactory.OpenSession())
            {
                var fromDb = session.Get<Review>(review.Id);
                Assert.IsNull(fromDb);
            }
        }

        [Test]
        public void CanGetExistingReviewById()
        {
            IReviewRepository repository = new ReviewRepository();
            var fromDb = repository.GetById(_reviews[1].UserId);
            Assert.IsNotNull(fromDb);
            Assert.AreNotSame(_reviews[1], fromDb);
            Assert.AreEqual(_reviews[1].ProductId, fromDb.ProductId);
            Assert.AreEqual(_reviews[1].UserId, fromDb.UserId);
            Assert.AreEqual(_reviews[1].Detail, fromDb.Detail);
            Assert.AreEqual(_reviews[1].Rating, fromDb.Rating);
        }

        [Test]
        public void CanGetExistingReviewList()
        {
            IReviewRepository repository = new ReviewRepository();
            var fromDb = repository.List(2);

            Assert.AreEqual(1, fromDb.Count);
            Assert.IsTrue(IsInReviewCollection(_reviews[1], fromDb));

        }

        private static bool IsInReviewCollection(Review review, IEnumerable<Review> fromDb)
        {
            return fromDb.Any(item => review.Id == item.Id);
        }

        #endregion
    }
}
