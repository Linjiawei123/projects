using Ganss.Xss;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MicroService.Extend.Middleware
{
    public class XssFilterMiddleware
    {
        private readonly RequestDelegate _next;

        public XssFilterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string[] methods = { "POST", "PUT" };
            if (context.Request.HasFormContentType && methods.Contains(context.Request.Method))
            {
                if (!context.Request.ContentType.Contains("multipart/form-data"))
                {
                    var form = await context.Request.ReadFormAsync();

                    var formFields = new Dictionary<string, StringValues>(form);
                    var sanitizedFormFields = new Dictionary<string, StringValues>();

                    foreach (var formField in formFields)
                    {
                        sanitizedFormFields[formField.Key] = XssFilter(formField.Value);
                    }

                    context.Request.Form = new FormCollection(sanitizedFormFields);
                }
            }
            else if (!context.Request.HasFormContentType && methods.Contains(context.Request.Method))
            {
                var bodyContent = await ReadBodyAsync(context);
                var bytes = Encoding.UTF8.GetBytes(XssFilter(bodyContent));
                context.Request.Body = new MemoryStream(bytes);
                context.Request.ContentLength = bytes.Length;
            }
            await _next(context);
        }

        private async Task<string> ReadBodyAsync(HttpContext context)
        {
            using (StreamReader reader = new StreamReader(context.Request.Body))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private string XssFilter(string value)
        {
            value = value.Trim();
            value = new HtmlSanitizer().Sanitize(value);
            return value;
        }
    }
}
