﻿using System.Collections.Generic;

namespace SimonRadford.Site.Models
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Remove(Product product);
        IList<Product> List();
        Product GetByProductId(int productId);
        Product GetByProductCode(string productCode);
        Product GetByProductName(string name);
        IList<Product> ManafacturerProductList(int manafacturerId);
        IList<Product> Search(string searchWord, List<int> manafacturerIdList);
    }
}