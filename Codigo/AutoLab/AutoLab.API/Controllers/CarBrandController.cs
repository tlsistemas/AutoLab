using AutoLab.Application.Interfaces;
using AutoLab.Application.Parameters;
using AutoLab.Application.ViewModel;
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
        /// Listar CarBrand
        /// </summary>
        /// <response code="200">CarBrands.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<CarBrandViewModel>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "CarBrandAsync")]
        public async Task<IActionResult> CarBrandAsync([FromQuery] CarBrandParams CarBrand)
        {
            try
            {
                var result = await _pageApplication.ListCarBrandAsync(CarBrand);
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
        /// Create CarBrand
        /// </summary>
        /// <response code="200">CarBrande criado com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "CarBrandCreate")]
        public async Task<IActionResult> Create([FromBody] CarBrandViewModel create)
        {
            try
            {
                var result = await _pageApplication.Create(create);
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
        /// Update CarBrand
        /// </summary>
        /// <response code="200">CarBrande alterada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "CarBrandUpdate")]
        public async Task<IActionResult> Update([FromBody] CarBrandViewModel update)
        {
            try
            {
                var result = await _pageApplication.Update(update);
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
        /// Remover CarBrand
        /// </summary>
        /// <response code="200">CarBrande removida com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "CarBrandRemove")]
        public async Task<IActionResult> Remove(string key)
        {
            try
            {
                var result = await _pageApplication.Remove(key);
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
