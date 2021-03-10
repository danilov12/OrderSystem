
namespace Utility.Cache
{
    public interface ICache
    {
        bool IsValid { get; set; }
        void AddUpdateCache(string key, object value);
        object GetValue(string key);
        void DeleteFromCache(string key);
        void EmptyCache();
    }
}
