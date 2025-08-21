using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EmployeeManagementSystem.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionLoggingMiddleware> _logger;

        public ExceptionLoggingMiddleware(RequestDelegate next, ILogger<ExceptionLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string logPath = Path.Combine("Logs", $"log-{DateTime.Now:yyyy-MM-dd}.json");
                var logEntry = new { Time = DateTime.Now, Exception = ex.ToString() };
                Directory.CreateDirectory("Logs");
                File.AppendAllText(logPath, JsonSerializer.Serialize(logEntry) + Environment.NewLine);

                context.Response.Redirect("/Home/Error");
            }
        }
    }
}
