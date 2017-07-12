using System;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Playground.MediatrPipelines.Validation;

namespace Playground.MediatrPipelines
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMediatR(typeof(Startup).GetTypeInfo().Assembly);

            services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(SomePipelineBehavior<,>));

            // Register all validators. Remove if using FluentValidation in model binding.
            AssemblyScanner.FindValidatorsInAssemblyContaining<Startup>()
                .ForEach(result => services.AddTransient(result.InterfaceType, result.ValidatorType));

            services
                .AddMvcCore(config =>
                {
                    config.Filters.Add(typeof(ValidationExceptionAttribute));
                    config.Filters.Add(typeof(ModelStateValidationAttribute));
                })
            // Unccoment to enable DataAnnotations validation in model binding.
            //    .AddDataAnnotations()
                .AddJsonFormatters();
            // Unccoment to enable FluentValidation in model binding (not desirable in most cases).
            //    .AddFluentValidation(config =>
            //    {
            //        config.RegisterValidatorsFromAssemblyContaining(typeof(Startup));
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            // Exception handler
            app.UseExceptionHandler(options =>
            {
                options.Run(ExceptionHandler);
            });

            app.UseMvc();
        }

        private async Task ExceptionHandler(HttpContext context)
        {
            IExceptionHandlerFeature exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptionHandler != null)
            {
                Exception exception = exceptionHandler.Error;

                byte[] responseBody = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
                {
                    eventId = exception.HResult,
                    message = "An unhandled exception occurred while processing the request.",
                    details = exception.Message

                }));

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json; charset=utf-8";
                context.Response.ContentLength = responseBody.Length;
                await context.Response.Body.WriteAsync(responseBody, 0, responseBody.Length).ConfigureAwait(false);
            }
        }
    }
}
