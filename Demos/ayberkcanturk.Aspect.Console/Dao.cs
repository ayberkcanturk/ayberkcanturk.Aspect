using System;
using System.Collections.Generic;

namespace ayberkcanturk.Aspect.Console
{
    public class Dao : IDao
    {
        private static readonly Lazy<IDao> lazy = new Lazy<IDao>(() => new Dao());
        private readonly IDictionary<string, Tuple<object, DateTime>> _InMemCache;
        private readonly IDictionary<int, Product> _InMemDb;

        public static IDao Instance { get { return lazy.Value; } }

        private Dao()
        {
            _InMemCache = new Dictionary<string, Tuple<object, DateTime>>();

            _InMemDb = new Dictionary<int, Product>();
            _InMemDb.Add(1, new Product() { Id = 1, Name = "MacBook Air", Price = 3000 });
            _InMemDb.Add(2, new Product() { Id = 2, Name = "Sony Xperia Z Ultra", Price = 1400 });
        }

        public void AddToCache(string key, object value, DateTime expirationDate)
        {
            Tuple<object, DateTime> cache = new Tuple<object, DateTime>(value, expirationDate);

            _InMemCache.Add(key, cache);
        }

        public T GetByKeyFromCache<T>(string key)
        {
            Tuple<object, DateTime> cache = null;
            bool exists = _InMemCache.TryGetValue(key, out cache);
            object val = null;

            if (exists && cache.Item2 > DateTime.UtcNow)
            {
                val = cache.Item1;
            }

            return (T)val;
        }

        public Product GetByIdFromDb(int id)
        {
            return _InMemDb[id];
        }
    }
}