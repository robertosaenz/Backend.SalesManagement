using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.SalesManagement.Models;

namespace Backend.SalesManagement.Repositories.Interfaces
{
    public interface ISalesRepository
    {
        Task<bool> Create(Sales sales);

        Task<bool> Update(Sales sales);

        Sales Get(string salesId);

        IOrderedQueryable<Sales> GetAll();

        IOrderedQueryable<Sales> GetAllByUserAccountId(string userAccountId);

        Task<bool> Delete(string salesId);
    }
}
