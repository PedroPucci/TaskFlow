using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.UnitOfWork;
using TaskFlow.Domain.Entity;

namespace TaskFlow.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : Controller
    {
        private readonly IUnitOfWorkService _serviceUoW;

        public UserController(IUnitOfWorkService unitOfWorkService)
        {
            _serviceUoW = unitOfWorkService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddAccount([FromBody] UserEntity userEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceUoW.UserService.AddUserAsync(userEntity);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
