using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoCore.Models;

namespace ToDoCore.Services
{
    public interface IRestServices
    {
        Task<List<ToDo>> GetToDoList(int tenantId);
        Task<List<Tenant>> GetTenantsList();
    }
}
