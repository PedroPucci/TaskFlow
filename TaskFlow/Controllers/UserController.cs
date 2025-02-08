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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateAccount([FromBody] UserEntity userEntity)
        {
            var result = await _serviceUoW.UserService.UpdateUserAsync(userEntity);
            return result.Success ? Ok(result) : BadRequest(userEntity);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _serviceUoW.UserService.DeleteUserAsync(id);
            return Ok();
        }

        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserEntity>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _serviceUoW.UserService.GetAllUsersAsync();
            return Ok(accounts);
        }
    }
}
