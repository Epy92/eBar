namespace eBarDatabase
{
    public class ProductOperations : IProductOperations
    {
        private readonly DBModels _databaseEntities;
        public ProductOperations()
        {
            _databaseEntities = new DBModels();
        }

        //public List<RestaurantProducts> GetAllRestaurantProducts(int restaurantId)
        //{
        //    return _databaseEntities.RestaurantProducts.Where(x => x.RestaurantId == restaurantId).ToList();
        //}

        //public List<RestaurantProducts> GetProductsByCategoryId(int categoryId)
        //{
        //    return _databaseEntities.RestaurantProducts.Where(x => x.CategoryId == categoryId).ToList();
        //}

        //public List<RestaurantProducts> GetProductsByPrice(int minPrice, int maxPrice)
        //{
        //    return _databaseEntities.RestaurantProducts.Where(x => x.ProductPrice > minPrice && x.ProductPrice < maxPrice).ToList();
        //}

//        public string AddProduct(RestaurantProducts product)
//        {
//            string message;
//            try
//            {
//                _databaseEntities.RestaurantProducts.Add(product);
//                _databaseEntities.SaveChanges();
//                message = ProductMessages.ProductSaved;
//            }
//#pragma warning disable CS0168 // The variable 'ex' is declared but never used
//            catch (Exception ex)
//#pragma warning restore CS0168 // The variable 'ex' is declared but never used
//            {
//                message = ProductMessages.ProductUnsaved;
//            }
//            return message;
//        }
    }
}