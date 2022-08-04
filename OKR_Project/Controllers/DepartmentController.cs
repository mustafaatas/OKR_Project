using API.DTO.Department;
using API.Validators;
using AutoMapper;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _mapper = mapper;
            _departmentService = departmentService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAllDepartments()
        {
            var departments = _departmentService.GetAllDepartments().ToList();
            var departmentResources = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDTO>>(departments);

            return Ok(departmentResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDeparmentById(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            var departmentResource = _mapper.Map<Department, DepartmentDTO>(department);

            return Ok(departmentResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<DepartmentDTO>> CreateDepartment([FromBody] SaveDepartmentDTO saveDepartmentResource)
        {
            var validator = new SaveDepartmentResourceValidator();
            var validationResult = await validator.ValidateAsync(saveDepartmentResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var departmentToCreate = _mapper.Map<SaveDepartmentDTO, Department>(saveDepartmentResource);
            var newDepartment = await _departmentService.CreateDepartment(departmentToCreate);
            var department = await _departmentService.GetDepartmentById(newDepartment.Id);
            var departmentResource = _mapper.Map<Department, DepartmentDTO>(department);
            return Ok(departmentResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentDTO>> UpdateDepartment(int id, [FromBody] SaveDepartmentDTO saveDepartmentResource)
        {
            var validator = new SaveDepartmentResourceValidator();
            var validationResult = await validator.ValidateAsync(saveDepartmentResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var departmentToBeUpdated = await _departmentService.GetDepartmentById(id);

            if (departmentToBeUpdated == null)
                return NotFound();

            var department = _mapper.Map<SaveDepartmentDTO, Department>(saveDepartmentResource);
            await _departmentService.UpdateDepartment(departmentToBeUpdated, department);
            var updatedDepartment = await _departmentService.GetDepartmentById(id);
            var updatedDepartmentResource = _mapper.Map<Department, DepartmentDTO>(updatedDepartment);
            return Ok(updatedDepartmentResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            await _departmentService.DeleteDepartment(department);

            return NoContent();
        }
    }
}