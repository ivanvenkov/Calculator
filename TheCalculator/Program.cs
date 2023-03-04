using FluentValidation;
using FluentValidation.AspNetCore;
using TheCalculator.Application.Interfaces;
using TheCalculator.Application.Models;
using TheCalculator.Middlewares;
using TheCalculatorAPI.Application.Filters;
using TheCalculatorAPI.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISimpleCalculatorManager,SimpleCalculatorManager>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
}).ConfigureApiBehaviorOptions(options =>
    {
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CalculationRequest>, CalculationRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CalculationRequestValidator>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
