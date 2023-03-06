using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.SalesManagement.Models;
using Backend.SalesManagement.Repositories;
using Backend.SalesManagement.Repositories.Interfaces;
using Backend.SalesManagement.Services.Interfaces;

namespace Backend.SalesManagement.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _repository;

        public async Task<Sales> Create(Sales sales)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(string salesId)
        {
            var success = await _repository.Delete(salesId);

            return success;
        }

        public  Sales Get(string salesId)
        {
            var result = _repository.Get(salesId);

            return result;
        }

        public IOrderedQueryable<Sales> GetAll()
        {
            var result = _repository.GetAll();

            return result;
        }

        public IOrderedQueryable<Sales> GetAllByUserAccountId(string userAccountId)
        {
            var result = _repository.GetAllByUserAccountId(userAccountId);

            return result;
        }

        public async Task<Sales> Update(Sales sales)
        {
            sales.LastUpdatedDateTime = DateTime.UtcNow;

            var success = await _repository.Update(sales);

            if (success)
                return sales;
            else
                return null;
        }
    }
}
