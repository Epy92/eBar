using System;

namespace ViewModels
{
    public class RestaurantProductsModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductMeasurement { get; set; }
        public Nullable<int> ProductMeasurementValue { get; set; }
        public string ProductMadeOf { get; set; }
        public int RestaurantId { get; set; }
    }
}