using AutoLab.Utils.Events;
using FluentValidation.Results;
using System.Net;

namespace AutoLab.Utils.Http.Response
{
    public class BaseResponse<T>
    {
        public BaseResponse()
        {
            Messages = new List<Error>();
        }
        public BaseResponse(string code, string message)
        {
            if (code != null && message != null)
            {
                Success = false;
                Messages = new List<Error>();
                Messages.Add(
                    new Error(message)
                    {
                        Message = message
                    });
            }
        }
        public BaseResponse(T data)
        {
            Data = data;
            Messages = new List<Error>();
        }
        public BaseResponse(List<Error> messages)
        {
            Messages = messages;
        }
        public BaseResponse(T data, HttpStatusCode Status)
        {
            Data = data;
            StatusCode = Status;
            Messages = new List<Error>();
        }

        #region Members
        private HttpStatusCode? StatusCode { get; set; }
        public bool Success { get; set; } = true;
        public bool Error { get; set; } = false;
        public T Data { get; set; }
        public List<Error> Messages { get; set; }
        #endregion

        #region Public Methods
        public void AddErrors(IEnumerable<ValidationFailure> errors)
        {
            foreach (var error in errors)
                AddError(new Error(error.ErrorMessage));
        }

        public void AddErrors(IEnumerable<String> errors)
        {
            foreach (var error in errors)
                AddError(new Error(error));
        }

        public HttpStatusCode GetStatusCode()
        {
            if (Messages.Any())
                return StatusCode ?? HttpStatusCode.NotFound;

            return StatusCode ?? HttpStatusCode.OK;
        }

        public void SetStatusCode(HttpStatusCode Status)
        {
            StatusCode = Status;
        }


        public void AddError(String errorMessage)
        {
            AddError(new Error(errorMessage));
        }

        public void AddError(Event errorEvent, params object[] errorParameters)
        {
            AddError(new Error(String.Format(errorEvent.Message, errorParameters)));
        }
        #endregion

        #region Private Methods
        private void AddError(Error errorResponse, HttpStatusCode Status = HttpStatusCode.NotFound)
        {
            if (StatusCode == null)
                StatusCode = Status;

            if (typeof(T) == typeof(bool))
                Data = default;

            Messages.Add(errorResponse);
        }

        public void AddErrors(String v)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public class BaseResponse
    {
        public BaseResponse()
        {
            Messages = new List<Error>();
        }

        public BaseResponse(string code, string message)
        {
            if (code != null && message != null)
            {
                Success = false;
                Messages = new List<Error>();
                Messages.Add(
                    new Error(message)
                    {
                        Message = message
                    });
            }
        }

        public BaseResponse(object data)
        {
            Data = data;
            Messages = new List<Error>();
        }

        public BaseResponse(List<Error> messages)
        {
            Messages = messages;
        }

        public BaseResponse(object data, HttpStatusCode Status)
        {
            Data = data;
            StatusCode = Status;
            Messages = new List<Error>();
        }

        #region Members
        private HttpStatusCode? StatusCode { get; set; }
        public bool Success { get; set; } = true;
        public bool Error { get; set; } = false;
        public object Data { get; set; }
        public List<Error> Messages { get; set; }
        public int Count { get; set; }
        public string JsonAux { get; set; }
        #endregion

        #region Public Methods
        public void SetStatusCode(HttpStatusCode Status)
        {
            StatusCode = Status;
        }

        #endregion

        #region Private Methods
        private void AddError(Error errorResponse, HttpStatusCode Status)
        {
            if (StatusCode == null)
                StatusCode = Status;

            if (typeof(object) == typeof(bool))
                Data = default;

            Messages.Add(errorResponse);
        }

        public void AddErrors(String v)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
