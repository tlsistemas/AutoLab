using AutoLab.Application.Interfaces;
using AutoLab.Application.Parameters;
using AutoLab.Application.ViewModel;
using AutoLab.Domain.Entities;
using AutoLab.Domain.Interfaces.Services;
using AutoLab.Utils.Events;
using AutoLab.Utils.Http.Response;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AutoLab.Application.Application
{
    public class CarModelApplication : ICarModelApplication
    {
        private readonly ICarModelService _service;
        private readonly ILogger<CarModelApplication> _logger;
        private readonly IMapper _mapper;

        public CarModelApplication(ICarModelService service,
            ILogger<CarModelApplication> logger,
            IMapper mapper)
        {
            this._service = service;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<CarModelViewModel>>> ListCarModelAsync(CarModelParams paran)
        {
            var response = new BasePaginationResponse<IEnumerable<CarModelViewModel>>();

            try
            {

                var obj = await _service.GetByParamsAsync(paran.Filter(), paran.OrderBy, paran.Include);

                response.Count = obj.Count();

                if (paran.Skip.HasValue)
                {
                    obj = obj.Skip(paran.Skip.Value);
                }

                if (paran.Take.HasValue && paran.Take.Value > 0)
                {
                    obj = obj.Take(paran.Take.Value);
                }

                response.Data = _mapper.Map<IEnumerable<CarModelViewModel>>(obj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, paran), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            return response;
        }

        public async Task<BaseResponse<CarModelViewModel>> Create(CarModelViewModel paranObj)
        {
            var response = new BaseResponse<CarModelViewModel>();

            try
            {
                if (paranObj.KeyCarBrand is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.KeyCarBrand));
                    return response;
                }
                if (paranObj.Model is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Model));
                    return response;
                }

                var carBrand = new CarBrand { Key = paranObj.KeyCarBrand };
                var existingObj = _service.Get(x => x.CarBrandId == carBrand.Id && x.Model == paranObj.Model).ToList();

                if (existingObj != null && existingObj.Count > 0)
                {
                    response.AddError(Events.INVALID_VALUE, "Brand");
                    return response;
                }

                var obj = new CarModel
                {
                    CarBrand = carBrand,
                    CarBrandId = carBrand.Id,
                    Model = paranObj.Model,
                    Year = paranObj.Year
                };

                _service.Add(obj);
                response.Data = _mapper.Map<CarModelViewModel>(obj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, paranObj), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        public async Task<BaseResponse<CarModelViewModel>> Update(CarModelViewModel paranObj)
        {
            var response = new BaseResponse<CarModelViewModel>();

            try
            {
                if (paranObj.KeyCarBrand is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.KeyCarBrand));
                    return response;
                }
                if (paranObj.Model is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Model));
                    return response;
                }

                var existingObj = _service.Get(x => x.Id == new CarModel { Key = paranObj.Key }.Id).FirstOrDefault();
                if (existingObj == null)
                {
                    response.AddError(Events.INVALID_VALUE, "Brand");
                    return response;
                }

                var carBrand = new CarBrand { Key = paranObj.KeyCarBrand };

                existingObj.CarBrand = carBrand;
                existingObj.CarBrandId = carBrand.Id;
                existingObj.Model = paranObj.Model;
                existingObj.Year = paranObj.Year;
                existingObj.Updated = DateTime.Now;
                _service.Update(existingObj);
                response.Data = _mapper.Map<CarModelViewModel>(existingObj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, paranObj), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            return response;
        }

        public async Task<BaseResponse<CarModelViewModel>> Remove(string key)
        {
            var response = new BaseResponse<CarModelViewModel>();

            try
            {
                if (key == "")
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, key));
                    return response;
                }

                var obj = new CarModel { Key = key };
                if (obj.Id == 0)
                    obj = new CarModel { Key = key };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    return response;
                }

                _service.Remove(existingObj);

                response.Data = _mapper.Map<CarModelViewModel>(existingObj);
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.AddError(ex.Message);
                response.AddError(ex.StackTrace);
                _logger.LogCritical(string.Format(Events.SYSTEM_ERROR_NOT_HANDLED.Message, key), ex);
                response.Error = true;
                response.SetStatusCode(HttpStatusCode.InternalServerError);
            }
            return response;
        }

    }
}