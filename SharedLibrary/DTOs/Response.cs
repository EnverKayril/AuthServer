using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.DTOs
{
    public class Response<T> where T : class
    {

        public T Data { get; private set; }
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccess { get; private set; }
        public ErrorDTO Error { get; private set; }

        public static Response<T> Success(T data, int statuscode)
        {
            return new Response<T> { Data = data, StatusCode = statuscode, IsSuccess = true };
        }

        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default, StatusCode = statusCode, IsSuccess = true };
        }

        public static Response<T> Fail(ErrorDTO error, int statusCode)
        {
            return new Response<T> { Error = error, StatusCode = statusCode, IsSuccess = false };
        }

        public static Response<T> Fail(string errorMessage, int statusCode, bool isShow) 
        {
            var errorDTO = new ErrorDTO(errorMessage, isShow);

            return new Response<T> { Error = errorDTO, StatusCode = statusCode, IsSuccess = false };
        }
    }
}