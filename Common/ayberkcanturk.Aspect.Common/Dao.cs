using System;
using System.Collections.Generic;

namespace ayberkcanturk.Aspect.Common
{
    public class Dao : IDao
    {
        public static IDao Instance { get { return lazy.Value; } }
        private static readonly Lazy<IDao> lazy = new Lazy<IDao>(() => new Dao());
        private readonly IDictionary<string, Tuple<object, DateTime>> inMemmoryCache;
        private readonly IDictionary<int, Product> inMemoryDb;

        private Dao()
        {
            inMemmoryCache = new Dictionary<string, Tuple<object, DateTime>>();
            inMemoryDb = new Dictionary<int, Product>();
            inMemoryDb.Add(1, new Product() { Id = 1, Name = "IPhone 7", Price = 3450 });
            inMemoryDb.Add(2, new Product() { Id = 2, Name = "Samsung S7", Price = 2500 });
        }

        public void AddToCache(string key, object value, DateTime expirationDate)
        {
            Tuple<object, DateTime> cache = new Tuple<object, DateTime>(value, expirationDate);
            inMemmoryCache.Add(key, cache);
        }

        public T GetByKeyFromCache<T>(string key)
        {
            Tuple<object, DateTime> cache = null;
            bool exists = inMemmoryCache.TryGetValue(key, out cache);
            object val = null;
            if (exists && cache.Item2 > DateTime.UtcNow)
            {
                val = cache.Item1;
            }

            return (T)val;
        }

        public Product GetByIdFromDb(int id)
        {
            return inMemoryDb[id];
        }
    }
}