# ayberkcanturk.Aspect
ayberkcanturk.Aspect is a provider of a proxy between the woven class and the consumer. It uses the same mechanism as in remoting: the client 'see' the remote object, but it actually talks to its proxy. All accesses to the aspected object go through the proxy class. The aspect is implemented as a transparent proxy, derived from the System.Runtime.Remoting.Proxies.RealProxy class.

#Usage

    public class CacheInterceptor : Attribute, IInterceptor
    {
        public int DurationInMinute { get; set; }

        private readonly IDao cacheService;

        public CacheInterceptor()
        {
            cacheService = Dao.Instance;
        }

        public void Intercept(ref IInvocation invocation)
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
        
        
    class Program
    {
        static void Main(string[] args)
        {    
            IProductService productService2 = TransparentProxy<ProductService, IProductService>.GenerateProxy();
            var product2 = productService2.GetProduct(1);
            Console.WriteLine($"Id: {product2.Id}, Name: {product2.Name}, Price: {product2.Price}");
            Console.ReadLine();
        }
    }
