using ayberkcanturk.Aspect.UnitTest.Interceptor;
using System;

namespace ayberkcanturk.Aspect.UnitTest
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
    public class ProductService : IProductService
    {
        private readonly IDao dao;

        public ProductService()
        {
            dao = Dao.Instance;
        }

        [CacheInterceptor(DurationInMinute = 10)]
        //[ExceptionHandlingInterceptor]
        public Product GetProduct(int productId)
        {
            return dao.GetByIdFromDb(productId);
        }

        public Product GetProductWithCache(int productId)
        {
            Product product = dao.GetByKeyFromCache<Product>("GetProduct_1");

            if (product == null)
            {
                product = dao.GetByIdFromDb(productId);
                dao.AddToCache("GetProduct_1", product, DateTime.UtcNow.AddMinutes(10));
            }

            return product;
        }
    }
}