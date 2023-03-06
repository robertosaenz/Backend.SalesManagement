using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.SalesManagement.Models
{
    public class Sales
    {
        public string Id { get; set; }

        public string UserAccountId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime LastUpdatedDateTime { get; set; }
    }
}
