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
    opt.ReportApiVersions = true;//�p�G�o�ӳ] true ���ܡA�N�|�N�䴩�������C�b header �����C
    opt.AssumeDefaultVersionWhenUnspecified = true;//�]�ӳ] true ���ܡA�ϥήɦp�G�S�S�O���w���ܡA�N�|�� DefaultApiVersion �]�w�������C
    opt.DefaultApiVersion = new ApiVersion(1, 0);//�]�w API �w�]�������C 
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
    //�I�������n�B�z��response
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
