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
        /// <response code="200">CarModels.</response>
        /// <response code="400">
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<CarModelViewModel>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [ProducesResponseType(401)]
        [SwaggerOperation(OperationId = "CarModelAsync")]
        public async Task<IActionResult> CarModelAsync([FromQuery] CarModelParams CarModel)
        {
            try
            {
                var result = await _pageApplication.ListCarModelAsync(CarModel);
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
        /// Create CarModel
        /// </summary>
        /// <response code="200">CarModele criado com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "CarModelCreate")]
        public async Task<IActionResult> Create([FromBody] CarModelViewModel create)
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
        /// Update CarModel
        /// </summary>
        /// <response code="200">CarModele alterada com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "CarModelUpdate")]
        public async Task<IActionResult> Update([FromBody] CarModelViewModel update)
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
        /// Remover CarModel
        /// </summary>
        /// <response code="200">CarModele removida com sucesso.</response>
        /// <response code="400">
        /// </response>
        [HttpDelete]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        [ProducesResponseType(typeof(BaseResponse<bool>), 400)]
        [SwaggerOperation(OperationId = "CarModelRemove")]
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