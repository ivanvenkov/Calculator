﻿using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;

namespace TheCalculator.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (SyntaxErrorException ex) 
            {
                this.logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;

                var resposne = new ProblemDetails
                {
                    Status = 400,
                    Detail = this.env.IsDevelopment() ? ex.StackTrace?.ToString() : null,
                    Title = "Not a valid expression. Only numbers, parentheses and operators:'+, -, *, /, (, )' allowed."
                };

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(resposne, options);

                await context.Response.WriteAsync(json);
            }
            catch (EvaluateException ex)
            {
                this.logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;

                var resposne = new ProblemDetails
                {
                    Status = 400,
                    Detail = this.env.IsDevelopment() ? ex.StackTrace?.ToString() : null,
                    Title = "Not a valid expression. Only numbers, parentheses and operators:'+, -, *, /, (, )' allowed."
                };

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(resposne, options);

                await context.Response.WriteAsync(json);
            }
            catch (OverflowException ex)
            {
              
                this.logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;

                var resposne = new ProblemDetails
                {
                    Status = 400,
                    Detail = this.env.IsDevelopment() ? ex.StackTrace?.ToString() : null,
                    Title = ex.Message
                };

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(resposne, options);

                await context.Response.WriteAsync(json);
            }
            catch (Exception ex) 
            {
                this.logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var resposne = new ProblemDetails
                {
                    Status = 500,
                    Detail = this.env.IsDevelopment() ? ex.StackTrace?.ToString() : null,
                    Title = ex.Message
                };

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(resposne, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
