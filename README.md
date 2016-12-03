# ayberkcanturk.Aspect
ayberkcanturk.Aspect is a provider of a proxy between the woven class and the consumer. It uses the same mechanism as in remoting: the client 'see' the remote object, but it actually talks to its proxy. All accesses to the aspected object go through the proxy class. The aspect is implemented as a transparent proxy, derived from the System.Runtime.Remoting.Proxies.RealProxy class.

#ayberkcanturk.Aspect is on nuget
To install ayberkcanturk.Aspect, run the following command in the Package Manager Console:
>> Install-Package ayberkcanturk.Aspect

#Usage


    public class ProductService : IProductService
    {
        private readonly IDao dao;

        public ProductService()
        {
            dao = Dao.Instance;
        }

        [CacheInterceptor(DurationInMinute = 10)]
        public Product GetProduct(int productId)
        {
            return dao.GetByIdFromDb(productId);
        }
        
        public Product GetProductWithCache(int productId)
        {
            Product product = dao.GetByKeyFromCache<Product>($"GetProduct_{productId}");
            if (product == null)
            {
                product = dao.GetByIdFromDb(productId);
                dao.AddToCache($"GetProduct_{productId}", product, DateTime.UtcNow.AddMinutes(10));
            }

            return product;
         }
    }

    public class CacheInterceptor : Interceptor
    {
        public int DurationInMinute { get; set; }

        private readonly IDao cacheService;

        public CacheInterceptor()
        {
            cacheService = Dao.Instance;
        }

        public override void Intercept(ref IInvocation invocation)
        {
            string cacheKey = string.Format("{0}_{1}", invocation.MethodName, string.Join("_", invocation.Arguments));

            object[] args = new object[1];
            args[0] = cacheKey;

            invocation.Response = typeof(Dao).GetMethod("GetByKeyFromCache")
                .MakeGenericMethod(new[] { invocation.ReturnType })
                .Invoke(cacheService, args);

            if (invocation.Response == null)
            {
                object response = invocation.Procceed();

                if (response != null)
                {
                    cacheService.AddToCache(cacheKey, response, DateTime.Now.AddMinutes(10));
                    invocation.Response = response;
                }
            }
        }
    }
        
        
    class Program
    {
        static void Main(string[] args)
        {    
            IProductService proxy = ProxyFactory.GetTransparentProxy<IProductService, ProductService>();
            var product = proxy.GetProduct(1);
            Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}");
            Console.ReadLine();
        }
    }
