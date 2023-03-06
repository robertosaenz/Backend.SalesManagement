using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Backend.SalesManagement.Context;
using Backend.SalesManagement.Models;
using Backend.SalesManagement.Repositories;
using Backend.SalesManagement.Repositories.Interfaces;

namespace Backend.SalesManagement.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly IServiceScope _scope;
        private readonly SalesDatabaseContext _databaseContext;

        public SalesRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();

            _databaseContext = _scope.ServiceProvider.GetRequiredService<SalesDatabaseContext>();
        }

        public async Task<bool> Create(Sales sales)
        {
            var success = false;

            _databaseContext.Sales.Add(sales);

            var numberOfItemsCreated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsCreated == 1)
                success = true;

            return success;
        }

        public async Task<bool> Delete(string salesId)
        {
            var success = false;

            var existingSales = Get(salesId);

            if (existingSales != null)
            {
                _databaseContext.Sales.Remove(existingSales);

                var numberOfItemsDeleted = await _databaseContext.SaveChangesAsync();

                if (numberOfItemsDeleted == 1)
                    success = true;
            }

            return success;
        }

        public Sales Get(string salesId)
        {
            var result = _databaseContext.Sales
                               .Where(x => x.Id == salesId)
                               .FirstOrDefault();

            return result;
        }

        public IOrderedQueryable<Sales> GetAll()
        {
            var result = _databaseContext.Sales
                                .OrderByDescending(x => x.LastUpdatedDateTime);

            return result;
        }

        public IOrderedQueryable<Sales> GetAllByUserAccountId(string userAccountId)
        {
            var result = _databaseContext.Sales
                                 .Where(x => x.UserAccountId == userAccountId)
                                 .OrderByDescending(x => x.LastUpdatedDateTime);

            return result;
        }

        public async Task<bool> Update(Sales sales)
        {
            var success = false;

            var existingSales = Get(sales.Id);

            if (existingSales != null)
            {
                existingSales.Title = sales.Title;
                existingSales.Description = sales.Description;
                existingSales.LastUpdatedDateTime = sales.LastUpdatedDateTime;

                _databaseContext.Sales.Attach(existingSales);

                var numberOfItemsUpdated = await _databaseContext.SaveChangesAsync();

                if (numberOfItemsUpdated == 1)
                    success = true;
            }

            return success;
        }
    }
}
