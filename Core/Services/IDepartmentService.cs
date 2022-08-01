using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDepartmentService
    {
        IQueryable<Department> GetAllDepartments();
        Task<Department> GetDepartmentById(int id);
        Task<Department> CreateDepartment(Department newDepartment);
        Task UpdateDepartment(Department departmentToBeUpdated, Department department);
        Task DeleteDepartment(Department department);
    }
}
