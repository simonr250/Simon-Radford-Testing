﻿using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SimonRadford.Site.Models;

namespace SimonRadford.Site.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void Add(Product product)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Save(product);
                transaction.Commit();
            }
        }

        public void Update(Product product)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Update(product);
                transaction.Commit();
            }
        }

        public void Remove(Product product)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                session.Delete(product);
                transaction.Commit();
            }
        }

        public IList<Product> List()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
             return  session.CreateCriteria(typeof (Product))
                    .List<Product>();
            }
        }

        public IList<Product> ManafacturerProductList(int manafacturerId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (ITransaction transaction = session.BeginTransaction())
            {
                return session.CreateCriteria(typeof(Product)).Add(Restrictions.Eq("ManafacturerId", manafacturerId))
                       .List<Product>();
            }
        }

        public Product GetByProductId(int productId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
                return session.Get<Product>(productId);
        }
        public Product GetByProductCode(string productCode)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var product = session
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("ProductCode", productCode))
                    .UniqueResult<Product>();
                return product;
            }
        }
        public Product GetByProductName(string name)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var product = session
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Product>();
                return product;
            }
        }

        public IList<Product> Search(string word, List<int> manafacturerIdList)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var productSearchList = session
                    .CreateCriteria(typeof (Product))
                    .Add(Restrictions.Like("Name", "%" + word + "%") || Restrictions.Like("ProductCode", "%" + word + "%") || Restrictions.Like("Description", "%" + word + "%")|| Restrictions.In("ManafacturerId", manafacturerIdList))
                    .List<Product>();  
                return productSearchList;
            }
        }

    }
}