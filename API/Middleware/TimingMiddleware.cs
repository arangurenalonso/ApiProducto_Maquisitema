using Newtonsoft.Json;
using System.Diagnostics;

namespace API.Middleware
{
    public class TimingMiddleware
    {
        private readonly Serilog.ILogger _logger;
        private readonly RequestDelegate _next;

        public TimingMiddleware(RequestDelegate next, Serilog.ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            await _next(context);

            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;

            var controllerName = context.GetRouteValue("controller")?.ToString();
            var actionName = context.GetRouteValue("action")?.ToString();

            var logEntry = new LogEntry
            {
                Message = $"Request Controller: {controllerName} - Action: {actionName} | completed in {elapsedMilliseconds} ms.",
                ElapsedMilliseconds = elapsedMilliseconds
            };

            _logger.Information(JsonConvert.SerializeObject(logEntry));

        }
    }
}
