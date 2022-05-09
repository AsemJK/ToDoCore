using System.Collections.Generic;

namespace ToDoCore.Models
{
    public class ToDoViewModel
    {
        public ToDo ToDo { get; set; }
        public List<Tenant> Tenants { get; set; }
        public decimal TenantId { get; set; }
    }
}
