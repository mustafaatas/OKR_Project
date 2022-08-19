using API.DTO;
using API.DTO.Department;
using API.Validators;
using AutoMapper;
using Core;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _departmentService = departmentService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAllDepartments()
        {
            //var claims = HttpContext?.User?.Identities?.FirstOrDefault()?.Claims.ToList();

            var departments = await _departmentService.GetAllDepartments().ToListAsync();
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

        [HttpPost]
        public async Task<ActionResult<DepartmentDTO>> CreateDepartment([FromBody] SaveDepartmentDTO saveDepartmentResource)
        {
            var validator = new SaveDepartmentResourceValidator();
            var validationResult = await validator.ValidateAsync(saveDepartmentResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var departmentToCreate = _mapper.Map<SaveDepartmentDTO, Department>(saveDepartmentResource);
            var role = await _userService.GetAllUsers().Where(x => x.Id == saveDepartmentResource.LeaderId).Select(x => x.Role).FirstOrDefaultAsync();
            var user = await _userService.GetAllUsers().Where(x => x.Id == saveDepartmentResource.LeaderId).FirstOrDefaultAsync();

            var dep = await _departmentService.GetAllDepartments().Where(x => x.LeaderId == saveDepartmentResource.LeaderId).FirstOrDefaultAsync();

            if(user?.DepartmentId != 0)
            {
                return BadRequest(new Response { Status = "Error", Message = "The user is participant of another department." });
            }

            if (role?.Name !="Leader" && role!=null)
            {
                return BadRequest(new Response { Status = "Error", Message = "The user is not leader" });
            }

            var newDepartment = await _departmentService.CreateDepartment(departmentToCreate);
            var department = await _departmentService.GetDepartmentById(newDepartment.Id);
            var departmentResource = _mapper.Map<Department, DepartmentDTO>(department);

            if(user?.DepartmentId == departmentResource.Id)
            {
                return BadRequest(new Response { Status = "Error", Message = "The leader is not participant of custom department." });
            }

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
            var role = await _userService.GetAllUsers().Where(x => x.Id == saveDepartmentResource.LeaderId).Select(x => x.Role).FirstOrDefaultAsync();
            var user = await _userService.GetAllUsers().Where(x => x.Id == saveDepartmentResource.LeaderId).FirstOrDefaultAsync();

            if (user?.DepartmentId != id)
            {
                return BadRequest(new Response { Status = "Error", Message = "The leader is not participant of custom department." });
            }

            if (role?.Name != "Leader" && role != null)
            {
                return BadRequest(new Response { Status = "Error", Message = "The user is not leader" });
            }

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