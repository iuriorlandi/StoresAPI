using StoresAPI.Domain;

namespace StoresAPI.Infrastructure.Repositories
{
    public interface IStoreRepository
    {
        Task<Store> CreateAsync(Store store);
        Task<Store> GetAsync(int id);
        Task<Store> UpdateAsync(int id, Store updatedStore);
        Task<bool> DeleteAsync(int id);
    }
}
