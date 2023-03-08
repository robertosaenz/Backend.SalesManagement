using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.SalesManagement.Models
{
    public class User
    {
        public string UserName { get; }

        public User(string userName)
        {
            this.UserName = userName;
        }
    }
}
