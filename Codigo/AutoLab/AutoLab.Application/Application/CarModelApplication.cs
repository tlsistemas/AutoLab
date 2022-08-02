using AutoLab.Application.Interfaces;
using AutoLab.Domain.Interfaces.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;

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

    }
}