using ayberkcanturk.Aspect.Default;
using System.Diagnostics;

namespace ayberkcanturk.Aspect.Console
{
    using System;
    class Program
    {
        static void Main(string[] args)
        {
            IDao dao = Dao.Instance;
            
            #region Without Interceptor
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            IProductService productService1 = new ProductService();
            var product1 = productService1.GetProductWithCache(1);
            stopWatch.Stop();
            Console.WriteLine("Without Interceptor:" + stopWatch.ElapsedTicks);
            Console.WriteLine("Id: {0}, Name: {1}, Price: {2}", product1.Id, product1.Name, product1.Price);
            Console.ReadLine();
            stopWatch.Reset();
            #endregion

            #region Without Cache
            stopWatch.Start();
            IProductService productService2 = TransparentProxy<ProductService, IProductService>.GenerateProxy();
            var product2 = productService2.GetProduct(1);
            stopWatch.Stop();
            Console.WriteLine("With Interceptor:" + stopWatch.ElapsedTicks);
            Console.WriteLine($"Id: {product2.Id}, Name: {product2.Name}, Price: {product2.Price}");
            Console.ReadLine();
            stopWatch.Reset();
            #endregion

            #region Without Interceptor
            stopWatch.Start();
            IProductService productService3 = new ProductService();
            var product3 = productService3.GetProductWithCache(1);
            stopWatch.Stop();
            Console.WriteLine("2nd Try: Without Interceptor:" + stopWatch.ElapsedTicks);
            Console.WriteLine("Id: {0}, Name: {1}, Price: {2}", product3.Id, product3.Name, product3.Price);
            Console.ReadLine();
            stopWatch.Reset();
            #endregion

            #region Without Cache
            stopWatch.Start();
            IProductService productService4 = TransparentProxy<ProductService, IProductService>.GenerateProxy();
            var product4 = productService2.GetProduct(1);
            stopWatch.Stop();
            Console.WriteLine("2nd Try: With Interceptor:" + stopWatch.ElapsedTicks);
            Console.WriteLine($"Id: {product4.Id}, Name: {product4.Name}, Price: {product4.Price}");
            Console.ReadLine();
            stopWatch.Reset();
            #endregion

            #region Without Interceptor
            stopWatch.Start();
            IProductService productService5 = new ProductService();
            var product5 = productService5.GetProductWithCache(1);
            stopWatch.Stop();
            Console.WriteLine("3rd Try: Without Interceptor:" + stopWatch.ElapsedTicks);
            Console.WriteLine("Id: {0}, Name: {1}, Price: {2}", product5.Id, product5.Name, product5.Price);
            Console.ReadLine();
            stopWatch.Reset();
            #endregion

            #region Without Cache
            stopWatch.Start();
            IProductService productService6 = TransparentProxy<ProductService, IProductService>.GenerateProxy();
            var product6 = productService6.GetProduct(1);
            stopWatch.Stop();
            Console.WriteLine("3rd Try: With Interceptor:" + stopWatch.ElapsedTicks);
            Console.WriteLine($"Id: {product6.Id}, Name: {product6.Name}, Price: {product6.Price}");
            Console.ReadLine();
            stopWatch.Reset();
            #endregion

            #region Without Interceptor
            stopWatch.Start();
            IProductService productService7 = new ProductService();
            var product7 = productService7.GetProductWithCache(1);
            stopWatch.Stop();
            Console.WriteLine("4th Try: Without Interceptor:" + stopWatch.ElapsedTicks);
            Console.WriteLine("Id: {0}, Name: {1}, Price: {2}", product7.Id, product7.Name, product7.Price);
            Console.ReadLine();
            stopWatch.Reset();
            #endregion

            #region Without Cache
            stopWatch.Start();
            IProductService productService8 = TransparentProxy<ProductService, IProductService>.GenerateProxy();
            var product8 = productService8.GetProduct(1);
            stopWatch.Stop();
            Console.WriteLine("4th Try: With Interceptor:" + stopWatch.ElapsedTicks);
            Console.WriteLine($"Id: {product8.Id}, Name: {product8.Name}, Price: {product8.Price}");
            Console.ReadLine();
            stopWatch.Reset();
            #endregion
        }
    }
}