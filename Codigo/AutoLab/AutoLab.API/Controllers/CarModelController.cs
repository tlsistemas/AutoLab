using AutoLab.API.Controllers;
using AutoLab.Application.Interfaces;
using AutoLab.Application.Parameters;
using AutoLab.Application.ViewModel;
using AutoLab.Utils.Http.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace AssetManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : BaseController
    {
        private readonly ILogger<CarModelController> _logger;
        private readonly ICarModelApplication _pageApplication;

        public CarModelController(
            ILogger<CarModelController> logger,
            ICarModelApplication PageApplication)
        {
            this._logger = logger;
            this._pageApplication = PageApplication;
        }

        /// <summary>
        /// Listar CarModel
        /// </summary>
        /// <response code="200">CarModeles.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<CarModelViewModel>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "ListCarModelAsync")]
        public async Task<IActionResult> ListCarModelAsync([FromQuery] CarModelParams CarModel)
        {
            try
            {
                var result = new { Teste = "Teste" }; // await _pageApplication.ListCarModelAsync(CarModel);
                return new OkObjectResult(result);

            }
            catch (Exception ex)
            {
                var result = new BaseResponse<Object>
                {
                    Data = null,
                    Success = false,
                    Error = true,
                };
                result.AddError(ex.Message);
                return BadRequest(result);
            }
        }
    }
}