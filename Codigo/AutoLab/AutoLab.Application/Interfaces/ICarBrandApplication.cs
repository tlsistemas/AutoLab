using AutoLab.Application.Parameters;
using AutoLab.Application.ViewModel;
using AutoLab.Utils.Http.Response;

namespace AutoLab.Application.Interfaces
{
    public interface ICarBrandApplication
    {
        Task<BaseResponse<IEnumerable<CarBrandViewModel>>> ListCarBrandAsync(CarBrandParams paran);
        Task<BaseResponse<IEnumerable<CarBrandViewModel>>> ListCarBrandAsync();
        Task<BaseResponse<CarBrandViewModel>> Create(CarBrandViewModel paranObj);
        Task<BaseResponse<CarBrandViewModel>> Update(CarBrandViewModel paranObj);
        Task<BaseResponse<CarBrandViewModel>> Remove(string key);
    }
}
