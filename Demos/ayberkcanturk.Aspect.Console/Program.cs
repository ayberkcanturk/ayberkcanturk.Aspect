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

            IProductService ps = new ProductService();
            IProductService psProxy = ProxyFactory.GetTransparentProxy<IProductService, ProductService>(ps);
            var product = psProxy.GetProduct(1);

            IProductService psProxy2 = ProxyFactory.GetTransparentProxy<IProductService, ProductService>();
            var product2 = psProxy2.GetProduct(1);

            Console.WriteLine();
        }
    }
}