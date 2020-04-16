using DK.Domain.DTO.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DK.Utility;
using System.Data.SqlClient;

namespace DK.API.Middlewares.ExceptionHandler
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var response = new GeneralResponse<object>() { xResult = null, xStatus = false };

            if (ex is ErrorMessageException)
            {
                response.xMessages = ex.Data["Messages"] as List<string>;
                code = HttpStatusCode.OK;
            }
            else if (ex is SqlException)
            {
                // response.xMessages = new List<string> { ex.ToFullMessage() };
                response.xMessages = new List<string> { "خطا در ذخیره اطلاعات. لطفا با پشتیبانی تماس بگیرید" };
            }
            else
            {
                // response.xMessages = new List<string> { ex.ToFullMessage() };
                response.xMessages = new List<string> { "خطا در پردازش اطلاعات. لطفا با پشتیبانی تماس بگیرید" };
            }

            var result = JsonConvert.SerializeObject(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

    public static class ResponseExtension
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}
