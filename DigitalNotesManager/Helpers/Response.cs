using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalNotesManager.Helpers
{
    public class Response<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public Response(bool status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        public static Response<T> Success(T data, string message = "Operation successful")
        {
            return new Response<T>(true, message, data);
        }

        public static Response<T> Failure(string message = "Operation failed")
        {
            return new Response<T>(false, message, default);
        }
    }
}
