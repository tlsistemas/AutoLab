using AutoLab.Application.Interfaces;
using AutoLab.Application.Parameters;
using AutoLab.Utils.Http.Response;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

namespace AutoLab.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandController : BaseController
    {
        private readonly ILogger<CarBrandController> _logger;
        private readonly ICarBrandApplication _pageApplication;

        public CarBrandController(
            ILogger<CarBrandController> logger,
            ICarBrandApplication PageApplication)
        {
            this._logger = logger;
            this._pageApplication = PageApplication;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <response code="200">Item criado com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "CreateAsync")]
        public async Task<IActionResult> CreateAsync([FromQuery] CarBrandParams CarBrand)
        {
            try
            {
                var result = new { Teste="Teste" }; //await _pageApplication.ListCarBrandClickAsync(CarBrand);
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

        /// <summary>
        /// Update
        /// </summary>
        /// <response code="200">Item atualizado com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromQuery] CarBrandParams CarBrand)
        {
            try
            {
                var result = new { Teste = "Teste" };// await _pageApplication.ListCarBrandErrorAsync(CarBrand);
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
