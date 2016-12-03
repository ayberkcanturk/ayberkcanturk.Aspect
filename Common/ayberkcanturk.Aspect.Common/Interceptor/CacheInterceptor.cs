using System;

namespace ayberkcanturk.Aspect.Common
{
    using Core;
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
}