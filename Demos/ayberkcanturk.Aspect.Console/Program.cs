namespace ayberkcanturk.Aspect.Console
{
    using System;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            IProductService productService = ProxyFactory.GetTransparentProxy<IProductService, ProductService>();
            Product product = productService.GetProduct(1);

            Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}");
            Console.ReadLine();
        }
    }
}