using AutoLab.Application.Parameters;
using AutoLab.Application.ViewModel;
using AutoLab.Utils.Http.Response;

namespace AutoLab.Application.Interfaces
{
    public interface ICarModelApplication
    {
        Task<BaseResponse<IEnumerable<CarModelViewModel>>> ListCarModelAsync(CarModelParams paran);
        Task<BaseResponse<CarModelViewModel>> Create(CarModelViewModel paranObj);
        Task<BaseResponse<CarModelViewModel>> Update(CarModelViewModel paranObj);
        Task<BaseResponse<CarModelViewModel>> Remove(string key);

    }
}
