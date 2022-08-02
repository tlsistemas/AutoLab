using AutoLab.Application.Interfaces;
using AutoLab.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;

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


    }
}