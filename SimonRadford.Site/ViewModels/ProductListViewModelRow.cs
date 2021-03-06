﻿namespace SimonRadford.Site.ViewModels
{
    public class ProductListViewModelRow
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ManafacturerName { get; set; }
        public double Price { get; set; }
        public int AverageRating { get; set; }
    }
}