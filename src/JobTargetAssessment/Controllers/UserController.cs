using JobTargetAssessment.Domain;
using JobTargetAssessment.Application;

using Microsoft.AspNetCore.Mvc;

namespace JobTargetAssessment.Presentation;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    public readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        return await _userService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUsersById(int id)
    {

        try
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        catch (System.Exception)
        {

            return NotFound();
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteUser(int id)
    {
        try
        {
            var result = await _userService.DeleteUserAsync(id);

            return Ok(result);
        }
        catch (System.Exception)
        {

            return NotFound();
        }

    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateUser(UserRequest user)
    {
        try
        {
            var result = await _userService.UpdateAsync(user.ToDomain());

            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid Model");
            }

            return Created("", result);
        }
        catch (Exception)
        {
            return BadRequest();
        }

    }

    [HttpPost]
    public async Task<ActionResult<bool>> CreateUser(UserRequest user)
    {
        try
        {
            var result = await _userService.CreateAsync(user.ToDomain());

            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid Model");
            }

            return Created("", result);
        }
        catch (Exception)
        {
            return BadRequest();
        }

    }
}
