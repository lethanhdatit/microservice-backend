using Microsoft.EntityFrameworkCore;
using stepmedia_demo.EntityModels;
using stepmedia_demo.UnitOfWork;
using stepmedia_demo.Services;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "allowSpecificOrigins";
var originWhiteList = builder.Configuration.GetSection("OriginWhiteList")?.Get<string[]>();

if(originWhiteList != null && originWhiteList.Any())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins(builder.Configuration.GetSection("OriginWhiteList").Get<string[]>()).AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                          });
    });
}


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StepmediaDemoContext>
                (options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();

var app = builder.Build();

// Config for Heroku deployment: dynamic port binding
if (app.Environment.IsProduction())
{
    var port = Environment.GetEnvironmentVariable("PORT");
    app.Urls.Add($"http://*:{port}");
}

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

if (originWhiteList != null && originWhiteList.Any())
{
    app.UseCors(MyAllowSpecificOrigins);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
