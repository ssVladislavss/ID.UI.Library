using System.Net;

namespace ID.UI.Core
{
    public class AjaxResult
    {
        public AjaxResultTypes Result { get; set; }
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public AjaxResult(AjaxResultTypes result, string? message, HttpStatusCode statusCode)
        {
            Result = result;
            Message = message;
            StatusCode = statusCode;
        }

        public static AjaxResult Success()
            => new(AjaxResultTypes.Success, null, HttpStatusCode.OK);
        public static AjaxResult Error(string message, HttpStatusCode? statusCode = null)
            => new(AjaxResultTypes.Error, message, statusCode ?? HttpStatusCode.OK);
    }

    public class AjaxResult<T> : AjaxResult
    {
        public T? Data { get; set; }
        public AjaxResult(AjaxResultTypes result, string? message, HttpStatusCode statusCode) : base(result, message, statusCode) { }

        public static AjaxResult<T> Success(T data)
            => new(AjaxResultTypes.Success, null, HttpStatusCode.OK) { Data = data };
        public static new AjaxResult<T> Error(string message, HttpStatusCode? statusCode = null)
            => new(AjaxResultTypes.Error, message, statusCode ?? HttpStatusCode.OK);
    }
}
