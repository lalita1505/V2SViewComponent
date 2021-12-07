using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace V2SViewComponent.Middleware
{
    public class DiagnosticsMiddleware
    {
        private readonly RequestDelegate _next;

        public DiagnosticsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            // Start the Timer using Stopwatch  
            var watch = new Stopwatch();
            watch.Start();

            context.Response.OnStarting(() =>
            {
                // Stop the timer information and calculate the time   
                watch.Stop();
                var routeValues = context.Request.RouteValues;
                var actionName = context.Request.Method;
                if (routeValues != null)
                {
                    if (routeValues.ContainsKey("action"))
                        actionName = routeValues["action"].ToString();
                }

                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;

                // Add the Response time information in the log file.
                string outputTemplate = string.Format("Request ended for {0} in {1} ms {2}", actionName, responseTimeForCompleteRequest, Environment.NewLine);
                Log.Logger = new LoggerConfiguration()
                                .MinimumLevel.Debug()
                                .WriteTo.File(@"Logs\ResponseLog.log", outputTemplate: outputTemplate, shared: true)
                                .CreateLogger();

                return Task.CompletedTask;
            });
            // Call the next delegate/middleware in the pipeline   
            return this._next(context);
        }
    }
}
