using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//ASP.NET Core Web API exception handling
//https://stackoverflow.com/questions/38630076/asp-net-core-web-api-exception-handling?answertab=modifieddesc#tab-top
//https://www.gushiciku.cn/pl/g2Ne/zh-tw
//about problem details
//https://timdeschryver.dev/blog/translating-exceptions-into-problem-details-responses#problem-details-with-aspnet
app.UseExceptionHandler(a => a.Run(async context =>
{
    var exceptionFeat = context.Features.Get<IExceptionHandlerFeature>();
    var error = exceptionFeat.Error;
    //截取部份要處理的response
    var isApi = exceptionFeat.Path.Contains("Home");
    if (isApi)
    {
        var problem = new ProblemDetails { Title = "Critical Error" };
        if (error != null)
        {
            if (app.Environment.IsDevelopment())
            {
                problem.Title = error.Message;
                problem.Detail = error.StackTrace;
            }
            else
                problem.Detail = error.Message;
        }
        await context.Response.WriteAsJsonAsync(problem);
    }
}));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
