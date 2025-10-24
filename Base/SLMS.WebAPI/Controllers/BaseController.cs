using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SLMS.WebAPI.Controllers
{
    /// <summary>
    /// Controlador base con métodos comunes
    /// SOLID: Don't Repeat Yourself
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        protected string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        protected string GetUserEmail()
        {
            return User.FindFirst(ClaimTypes.Email)?.Value;
        }

        protected string GetUserRole()
        {
            return User.FindFirst(ClaimTypes.Role)?.Value;
        }

        protected IActionResult HandleResponse<T>(ApiResponse<T> response)
        {
            if (!response.Success)
            {
                if (response.Message.Contains("no encontrad"))
                    return NotFound(response);

                if (response.Message.Contains("ya existe"))
                    return Conflict(response);

                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
