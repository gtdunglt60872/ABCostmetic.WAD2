using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Response<T> : BaseResponse
    {
        public Response() : base()
        {

        }

        public T Data { get; set; }

        public IList<T> ListData { get; set; }
    }

    public class BaseResponse
    {
        public string Msg { get; set; }

        public string RidirectUrl { get; set; }

        public BaseResponse()
        {
            Msg = Constants.MsgSuccess;
        }
    }

    public class Constants
    {
        public const string MsgSuccess = "Success";

        public const string MsgFail = "Fail";
    }
}