﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoCore.Models;

namespace ToDoCore.Services
{
    public interface IRestServices
    {
        Task<List<ToDo>> GetToDoList(int tenantId);
        Task<List<Tenant>> GetTenantsList();
        Task<User> CheckLogin(string userName, string password);
        Task<ToDo> AddToDo(ToDo todo);
        Task<ToDo> ToDoDetail(int id);
    }
}
