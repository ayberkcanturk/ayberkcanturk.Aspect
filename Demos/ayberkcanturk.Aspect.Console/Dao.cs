using System;
using System.Collections.Generic;

namespace ayberkcanturk.Aspect.Console
{
    public class Dao : IDao
    {
        public static IDao Instance { get { return lazy.Value; } }
        private static readonly Lazy<IDao> lazy = new Lazy<IDao>(() => new Dao());
        private readonly IDictionary<string, Tuple<object, DateTime>> inMemCache;
        private readonly IDictionary<int, Product> inMemDb;

        private Dao()
        {
            inMemCache = new Dictionary<string, Tuple<object, DateTime>>();

            inMemDb = new Dictionary<int, Product>();
            inMemDb.Add(1, new Product() { Id = 1, Name = "MacBook Air", Price = 3000 });
            inMemDb.Add(2, new Product() { Id = 2, Name = "Sony Xperia Z Ultra", Price = 1400 });
        }

        public void AddToCache(string key, object value, DateTime expirationDate)
        {
            Tuple<object, DateTime> cache = new Tuple<object, DateTime>(value, expirationDate);

            inMemCache.Add(key, cache);
        }

        public T GetByKeyFromCache<T>(string key)
        {
            Tuple<object, DateTime> cache = null;
            bool exists = inMemCache.TryGetValue(key, out cache);
            object val = null;

            if (exists && cache.Item2 > DateTime.UtcNow)
            {
                val = cache.Item1;
            }

            return (T)val;
        }

        public Product GetByIdFromDb(int id)
        {
            return inMemDb[id];
        }
    }
}