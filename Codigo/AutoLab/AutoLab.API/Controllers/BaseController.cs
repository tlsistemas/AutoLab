using AutoLab.Utils.Http.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoLab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : ControllerBase
    {
        public IActionResult BaseResponse<T>(BaseResponse<T> response)
        {
            return StatusCode((int)response.GetStatusCode(), response);
        }
    }
}
