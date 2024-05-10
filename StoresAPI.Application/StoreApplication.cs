using StoresAPI.Application.DTOs;
using StoresAPI.Application.Interface;
using StoresAPI.Domain;
using StoresAPI.Infrastructure.Repositories;

namespace StoresAPI.Application
{
    public class StoreApplication(IStoreRepository storeRepository) : IStoreApplication
    {
        public async Task<Store> CreateStoreAsync(StoreDTO store)
            => await storeRepository.CreateAsync(store.ToStore());

        public async Task<bool> DeleteStoreAsync(int id)
            => await storeRepository.DeleteAsync(id);

        public async Task<Store> GetStoreAsync(int id)
            => await storeRepository.GetAsync(id);

        public async Task<Store> UpdateStoreAsync(int id, StoreDTO store)
            => await storeRepository.UpdateAsync(id, store.ToStore());
    }
}
