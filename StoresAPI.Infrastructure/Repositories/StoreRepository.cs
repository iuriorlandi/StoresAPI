using Microsoft.EntityFrameworkCore;
using StoresAPI.Domain;
using StoresAPI.Infrastructure.Data;

namespace StoresAPI.Infrastructure.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreDbContext _context;

        public StoreRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<Store> CreateAsync(Store store)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == store.CompanyId);
            if (company == null)
            {
                throw new ArgumentException("Invalid CompanyId");
            }

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task<Store> GetAsync(int id)
        {
            return await _context.Stores.FindAsync(id);
        }

        public async Task<Store> UpdateAsync(int id, Store updatedStore)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == updatedStore.CompanyId);
            if (company == null)
            {
                throw new ArgumentException("Invalid CompanyId");
            }

            var store = await GetAsync(id);
            if (store != null)
            {
                store.Name = updatedStore.Name;
                store.CompanyId = updatedStore.CompanyId;
                store.Country = updatedStore.Country;
                store.Address = updatedStore.Address;
                await _context.SaveChangesAsync();
            }
            return store;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var store = await GetAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
