using Backend.SalesManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Backend.SalesManagement.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ValidateCredentials(string userName, string passWord, out User user);
    }
}
