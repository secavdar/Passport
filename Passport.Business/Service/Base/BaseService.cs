using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace Passport.Business.Service.Base
{
    public class BaseService
    {
        protected T ReadBody<T>(HttpContext httpContext, string httpMethod)
        {
            var httpRequest = httpContext.Request;

            if (httpRequest.ContentType != "application/json")
                throw new InvalidOperationException("Invalid Content-Type!!");

            if (httpRequest.Method != httpMethod)
                throw new InvalidOperationException("Invalid Http Method!!");

            using (var memoryStream = new MemoryStream())
            {
                httpRequest.Body.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memoryStream))
                {
                    var json = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
        }
        protected void WriteResponse<T>(HttpContext httpContext, T data)
        {
            var httpResponse = httpContext.Response;

            var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            httpResponse.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(httpResponse.Body))
            {
                streamWriter.Write(json);
            }
        }
        protected void WriteErrorResponse(HttpContext httpContext, Exception ex)
        {
            var httpResponse = httpContext.Response;

            while (ex.InnerException != null)
                ex = ex.InnerException;

            var result = new
            {
                ex.Message
            };

            httpResponse.StatusCode = 500;

            WriteResponse(httpContext, result);
        }
    }
}