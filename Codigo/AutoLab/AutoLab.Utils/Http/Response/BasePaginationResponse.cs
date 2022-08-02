using System.Net;

namespace AutoLab.Utils.Http.Response
{
    public class BasePaginationResponse<TData> : BaseResponse<TData>
    {
        public BasePaginationResponse()
        {

        }

        public BasePaginationResponse(TData data) : base(data)
        {

        }

        public BasePaginationResponse(TData data, HttpStatusCode Status) : base(data, Status)
        {
        }

        public int Count { get; set; }
    }
}
