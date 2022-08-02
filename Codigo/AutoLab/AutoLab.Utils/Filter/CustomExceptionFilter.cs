using AutoLab.Utils.Http.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace AutoLab.Utils.Filter
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public CustomExceptionFilter(
            IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            int StatusCode = (int)HttpStatusCode.InternalServerError;

            context.HttpContext.Response.StatusCode = StatusCode;

            BaseResponse<Exception> result = new BaseResponse<Exception>();

            if (hostingEnvironment.IsProduction())
            {
                result.Data = null;
                result.Messages = new List<Error>
                {
                    new Error(Events.Events.SYSTEM_ERROR_NOT_HANDLED.Message)
                };
            }
            else
            {
                result.Data = context.Exception;
                result.Messages = new List<Error>
                {
                    new Error( Events.Events.SYSTEM_ERROR_NOT_HANDLED.Message)
                };
            }

            context.Result = new JsonResult(result);
        }
    }
}
