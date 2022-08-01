using Core;
using Core.Models;
using Core.Services;
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
            await _unitOfWork.CommitAsync();
            return newDepartment;
        }

        public async Task DeleteDepartment(Department department)
        {
            _unitOfWork.Departments.Remove(department);
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<Department> GetAllDepartments()
        {
            return _unitOfWork.Departments.GetAllAsync();
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
            await _unitOfWork.CommitAsync();
        }
    }
}
