using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.SalesManagement.Models;

namespace Backend.SalesManagement.Services.Interfaces
{
    public interface ISalesService
    {
        Task<Sales> Create(Sales sales);

        Task<Sales> Update(Sales sales);

        Sales Get(string salesId);

        IOrderedQueryable<Sales> GetAll();

        IOrderedQueryable<Sales> GetAllByUserAccountId(string userAccountId);

        Task<bool> Delete(string salesId);
    }
}
