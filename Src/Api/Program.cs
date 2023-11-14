using Asp.Versioning;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//https://dotblogs.com.tw/rainmaker/2017/03/12/130759
//NuGet package Asp.Versioning.Mvc
builder.Services.AddApiVersioning(opt =>
{
    opt.ReportApiVersions = true;//如果這個設 true 的話，就會將支援的版本列在 header 之中。
    opt.AssumeDefaultVersionWhenUnspecified = true;//設個設 true 的話，使用時如果沒特別指定的話，就會用 DefaultApiVersion 設定的版本。
    opt.DefaultApiVersion = new ApiVersion(1, 0);//設定 API 預設的版本。 
});

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
    //var isApi = exceptionFeat.Path.Contains("Home");
    //if (isApi)
    //{
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
    //}
}));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
