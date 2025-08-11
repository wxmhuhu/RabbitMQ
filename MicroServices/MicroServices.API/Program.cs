using MicroServices.API;
using MicroServices.Application;
using MicroServices.Application.Services.AI;
using MicroServices.Domain.Bom;
using MicroServices.Infrastructure;
using MicroServices.Models.Dtos.Bom;
using MicroServices.Repository;
using Microsoft.OpenApi.Models;
using MricoServices.Infrastructure.Data;
using SmartConference.Api.Filter;
using SqlSugar;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//配置Redis
var redis = new CSRedis.CSRedisClient(builder.Configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton(redis);

builder.Services.AddScoped<RedisHelp<BomDto>>();

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(d =>
{
    var list = Path.Combine("obj\\Debug\\net6.0\\MicroServices.API.xml");
    d.IncludeXmlComments(list, true);
});

// 注册 AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// 注册 SqlSugar
builder.Services.AddSingleton<ISqlSugarClient>(provider =>
{
    var configuration = provider.GetService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return SqlSugarSetup.GetSqlSugarClient(connectionString);
});
 

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.Configure<CozeSettings>(builder.Configuration.GetSection("CozeSettings"));
builder.Services.AddHttpClient<CozeApiService>(); 

builder.Services.AddScoped<CozeStreamApiService>();
builder.Services.AddHttpClient<CozeStreamApiService>();

// 注册RabbitMQ消费者服务
builder.Services.AddHostedService<MicroServices.Application.Services.Product_Plan.RabbitMQConsumerService>();

// 1. 添加 Swagger/OpenAPI 服务
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // 确保你有一个或多个 SwaggerDoc，比如这里定义了 "v1"
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // 确保这里使用您的API实际运行的地址和端口
    if (builder.Environment.IsDevelopment())
    {
        c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer { Url = "http://localhost:5264" }); //本地
    }
    else
    {
        c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer { Url = "http://8.152.99.73:80" }); //云端
        //c.AddServer(new Microsoft.OpenApi.Models.OpenApiServer { Url = "http://www.littletrappedfish.top" }); //云端
    }
    // 添加您的自定义 DocumentFilter
    c.DocumentFilter<RemoveAiEndpointsFilter>(); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(d => d.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();


app.UseStaticFiles();

app.MapControllers();

app.Run();
