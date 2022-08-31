using API.DTO;
using API.DTO.Department;
using API.Validators;
using AutoMapper;
using Core;
using Core.Auth;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper, IUserService userService, UserManager<User> userManager)
        {
            _mapper = mapper;
            _departmentService = departmentService;
            _userService = userService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAllDepartments()
        {
            var claims = HttpContext?.User?.Identities?.FirstOrDefault()?.Claims.ToList();

            var departments = await _departmentService.GetAllDepartments().ToListAsync();
            var departmentResources = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDTO>>(departments);

            return Ok(departmentResources);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDTO>> GetDeparmentById(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            var departmentResource = _mapper.Map<Department, DepartmentDTO>(department);

            return Ok(departmentResource);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartmentByUser()
        {
            var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
            var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();
            if (user.DepartmentId.HasValue)
            {

                var department = await _departmentService.GetDepartmentById(user.DepartmentId.Value);
                var departmentResource = _mapper.Map<Department, DepartmentDTO>(department);

                return Ok(departmentResource);
            }

            //departman bulunamadı
            return BadRequest();

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetDepartmentByLeader()
        {
            var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
            var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();
            var department = await _userService.GetAllUsers().Select(k => k.Department).Where(k => k.LeaderId == user.Id).FirstOrDefaultAsync();

            if(department == null)
            {
                return BadRequest(new Response { Status = "Error", Message = "The user is not leader" });
            }

            var departmentResource = _mapper.Map<Department, DepartmentDTO>(department);

            return Ok(departmentResource);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<DepartmentDTO>> CreateDepartment([FromBody] SaveDepartmentDTO saveDepartmentResource)
        {
            var validator = new SaveDepartmentResourceValidator();
            var validationResult = await validator.ValidateAsync(saveDepartmentResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // this needs refining, but for demo it is ok

            var departmentToCreate = _mapper.Map<SaveDepartmentDTO, Department>(saveDepartmentResource);
            var user = await _userService.GetAllUsers().Where(x => x.Id == saveDepartmentResource.LeaderId).FirstOrDefaultAsync();
            //userı bulduktan sonra userın rolünü bul
            var role = await _userManager.GetRolesAsync(user);

            var dep = await _departmentService.GetAllDepartments().Where(x => x.LeaderId == saveDepartmentResource.LeaderId).FirstOrDefaultAsync();

            if (role.FirstOrDefault() != "Leader" && role != null)
            {
                return BadRequest(new Response { Status = "Error", Message = "The user is not leader" });
            }

            if (user != null)
            {
                return BadRequest(new Response { Status = "Error", Message = "The user is participant of another department." });
            }

            var newDepartment = await _departmentService.CreateDepartment(departmentToCreate);
            var department = await _departmentService.GetDepartmentById(newDepartment.Id);
            var departmentResource = _mapper.Map<Department, DepartmentDTO>(department);

            return Ok(departmentResource);
        }

        //[Authorize(Roles = "Admin")]
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
            var user = await _userService.GetAllUsers().Where(x => x.Id == saveDepartmentResource.LeaderId).FirstOrDefaultAsync();
            //userı bulduktan sonra userın rolünü bul
            var role = await _userManager.GetRolesAsync(user);

            if (role.FirstOrDefault() != "Leader" && role != null)
            {
                return BadRequest(new Response { Status = "Error", Message = "The user is not leader" });
            }

            if (user?.DepartmentId != id)
            {
                return BadRequest(new Response { Status = "Error", Message = "The leader is not participant of current department." });
            }

            await _departmentService.UpdateDepartment(departmentToBeUpdated, department);
            var updatedDepartment = await _departmentService.GetDepartmentById(id);
            var updatedDepartmentResource = _mapper.Map<Department, DepartmentDTO>(updatedDepartment);

            return Ok(updatedDepartmentResource);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _departmentService.GetDepartmentById(id);
            await _departmentService.DeleteDepartment(department);

            return NoContent();
        }
    }
}