using StoresAPI.Application.DTOs;
using StoresAPI.Domain;

namespace StoresAPI.Application.Interface
{
    public interface IStoreApplication
    {
        Task<Store> CreateStoreAsync(StoreDTO store);
        Task<Store> GetStoreAsync(int id);
        Task<Store> UpdateStoreAsync(int id, StoreDTO store);
        Task<bool> DeleteStoreAsync(int id);
    }
}
