using Core.Enums;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController] 
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IObjectiveService _objectiveService;
        private readonly IKeyResultService _keyResultService;

        public ProfileController(IObjectiveService objectService, IUserService userService, IKeyResultService keyResultService)
        {
            _objectiveService = objectService;
            _userService = userService;
            _keyResultService = keyResultService;
        }

        [HttpGet]
        public async Task<IActionResult> NumberOfObjectivesCurrentUser()
        {
            var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
            var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();

            var objectivesNumber = _objectiveService.GetAllObjectives().Where(k => k.UserId == user.Id).Count();

            return Ok(objectivesNumber);
        }

        [HttpGet]
        public async Task<IActionResult> NumberOfKeyResultsWhichAreCompleted()
        {
            var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
            var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();


            var keyResults = await _keyResultService.GetAllKeyResults().Where(k => k.SurObjective.UserId == user.Id).ToListAsync();
            var keyResultsWhichAreCompleted = keyResults.Where(k => k.Status == Status.Completed).Count();

            return Ok(keyResultsWhichAreCompleted);
        }

        [HttpGet]
        public async Task<IActionResult> NumberOfKeyResultsWhichAreCompletedSuccessfully()
        {
            var mail = HttpContext.User.Identities.Select(k => k.Name).FirstOrDefault();
            var user = await _userService.GetAllUsers().Where(x => x.Email == mail).FirstOrDefaultAsync();

            var keyResults = await _keyResultService.GetAllKeyResults().Where(k => k.SurObjective.UserId == user.Id).ToListAsync();
            var keyResultsWhichAreCompleted = keyResults.Where(k => k.Status == Status.Completed).ToList();
            var keyResultsWhichAreCompletedSuccessfully = keyResultsWhichAreCompleted.Where(k => k.ActualValue == 100).Count();

            return Ok(keyResultsWhichAreCompletedSuccessfully);
        }
    }
}
