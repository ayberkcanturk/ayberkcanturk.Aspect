using System;

namespace ayberkcanturk.Aspect.Console
{
    public interface IDao
    {
        void AddToCache(string key, object value, DateTime expirationDate);
        T GetByKeyFromCache<T>(string key);
        Product GetByIdFromDb(int id);
    }
}
