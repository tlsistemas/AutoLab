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
    public class CarBrandApplication : ICarBrandApplication
    {
        private readonly ICarBrandService _service;
        private readonly ILogger<CarBrandApplication> _logger;
        private readonly IMapper _mapper;

        public CarBrandApplication(ICarBrandService service,
            ILogger<CarBrandApplication> logger,
            IMapper mapper)
        {
            this._service = service;
            this._logger = logger;
            this._mapper = mapper;
        }

        public async Task<BaseResponse<IEnumerable<CarBrandViewModel>>> ListCarBrandAsync(CarBrandParams paran)
        {
            var response = new BasePaginationResponse<IEnumerable<CarBrandViewModel>>();

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

                response.Data = _mapper.Map<IEnumerable<CarBrandViewModel>>(obj);
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

        public async Task<BaseResponse<CarBrandViewModel>> Create(CarBrandViewModel paranObj)
        {
            var response = new BaseResponse<CarBrandViewModel>();

            try
            {
                if (paranObj.Brand is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Brand));
                    return response;
                }

                var existingObj = _service.Get(x => x.Brand == paranObj.Brand).ToList();

                if (existingObj != null && existingObj.Count > 0)
                {
                    response.AddError(Events.INVALID_VALUE, "Brand");
                    return response;
                }

                var obj = new CarBrand
                {
                     Brand = paranObj.Brand
                };

                _service.Add(obj);
                response.Data = _mapper.Map<CarBrandViewModel>(obj);
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

        public async Task<BaseResponse<CarBrandViewModel>> Update(CarBrandViewModel paranObj)
        {
            var response = new BaseResponse<CarBrandViewModel>();

            try
            {
                if (paranObj.Brand is null)
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, paranObj.Brand));
                    return response;
                }

                var existingObj = _service.Get(x => x.Id == new CarBrand { Key = paranObj.Key }.Id).FirstOrDefault();
                if (existingObj == null)
                {
                    response.AddError(Events.INVALID_VALUE, "Brand");
                    return response;
                }

                existingObj.Brand = paranObj.Brand;
                existingObj.Updated = DateTime.Now;
                _service.Update(existingObj);
                response.Data = _mapper.Map<CarBrandViewModel>(existingObj);
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

        public async Task<BaseResponse<CarBrandViewModel>> Remove(string key)
        {
            var response = new BaseResponse<CarBrandViewModel>();

            try
            {
                if (key == "")
                {
                    response.AddErrors(string.Format(Events.CREATE_PARTICIPANT_ERROR.Message, key));
                    return response;
                }

                var obj = new CarBrand { Key = key };
                if (obj.Id == 0)
                    obj = new CarBrand { Key = key };
                var existingObj = _service.Get(x => x.Id.Equals(obj.Id)).FirstOrDefault();

                if (existingObj == null)
                {
                    return response;
                }

                _service.Remove(existingObj);

                response.Data = _mapper.Map<CarBrandViewModel>(existingObj);
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