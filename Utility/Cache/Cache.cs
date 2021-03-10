using System.Collections.Generic;

namespace Utility.Cache
{
    public class Cache : ICache
    {
        private Dictionary<string, object> _cache = new Dictionary<string, object>();
        private static readonly object _lock = new object();

        public bool IsValid { get { return this._cache.Count == 1; } set { } }

        public void AddUpdateCache(string key, object value)
        {
            lock (_lock)
            {
                if (!this._cache.ContainsKey(key))
                {
                    this._cache.Add(key, value);
                }
                this._cache[key] = value;
            }
        }

        public void DeleteFromCache(string key)
        {
            lock (_lock)
            {
                this._cache.Remove(key);
            }
        }

        public object GetValue(string key)
        {
            lock (_lock)
            {
                return this._cache[key];
            }
        }

        public void EmptyCache()
        {
            lock (_lock)
            {
                this._cache.Clear();
            }
        }
    }
}
