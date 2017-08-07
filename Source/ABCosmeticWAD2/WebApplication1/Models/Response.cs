using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Response<T> : BaseResponse
    {
        public T Data { get; set; }

        public IList<T> ListData { get; set; }
    }

    public class BaseResponse
    {
        private const string MsgSuccess = "Success";

        private const string MsgFail = "Fail";

        public string Msg { get; set; } = MsgSuccess;

        public string RidirectUrl { get; set; }
    }
}