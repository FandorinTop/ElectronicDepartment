using System.Text.Json.Serialization;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using ElectronicDepartment.Common.Exceptions;

namespace Company.WebApplication1
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var exception = contextFeature.Error;
                        var path = context.Request.Path;
                        var message = string.Empty;
                        var result = string.Empty;
                        dynamic response = null;

                        switch (exception)
                        {
                            case DbNullReferenceException controllerException:
                                logger.LogError("Invalid data send no elements with such indexes");
                                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                                message = exception.Message;
                                break;
                                break;
                            default:
                                logger.LogError(exception, context.Request.Path);
                                message = exception.Message;
                                break;
                        }

                        if (env.EnvironmentName == "Test" || env.IsDevelopment())
                        {
                            response = new
                            {
                                StatusCode = context.Response.StatusCode,
                                ExMessage = message,
                                response,
                                exception
                            };
                        }
                        else
                        {
                            response = new
                            {
                                StatusCode = context.Response.StatusCode,
                                ExMessage = message,
                                response
                            };
                        }
                    }
                });
            });
        }
    }
}