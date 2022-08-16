using Core;
using Core.Models;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Department> CreateDepartment(Department newDepartment)
        {
            await _unitOfWork.Departments.AddAsync(newDepartment);
            _unitOfWork.Commit();
            return newDepartment;
        }

        public async Task DeleteDepartment(Department department)
        {
            _unitOfWork.Departments.Remove(department);
            _unitOfWork.Commit();
        }

        public IQueryable<Department> GetAllDepartments()
        {
            return _unitOfWork.Departments.GetAllAsync().Include(d => d.Users);
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await _unitOfWork.Departments.GetByIdAsync(id);
        }

        public async Task UpdateDepartment(Department departmentToBeUpdated, Department department)
        {

            //departmentToBeUpdated = new Department()
            //{
            //    Name = department.Name,
            //};

            departmentToBeUpdated.Name = department.Name;
            _unitOfWork.Commit();
        }
    }
}
