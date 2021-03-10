
namespace Utility.Cache
{
    public static class CacheSinglton
    {
        private static Cache _cache;
        private static object _lock = new object();

        public static Cache GetCache()
        {
            if (_cache == null)
            {
                lock (_lock)
                {
                    if(_cache == null)
                    {
                        _cache = new Cache();
                    }
                }
            }

            return _cache;
        }
    }
}
