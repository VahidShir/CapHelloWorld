using CapHelloWorldPublisher;
using CapHelloWorldPublisher.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCap(options =>
{
    options.UseEntityFramework<UserDbContext>();
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseDashboard(path => path.PathMatch = "/cap-dashboard");
    options.UseRabbitMQ(options =>
    {
        options.ConnectionFactoryOptions = options =>
        {
            options.Ssl.Enabled = false;
            options.HostName = "localhost";
            options.UserName = "guest";
            options.Password = "guest";
            options.Port = 5672;
        };
    });
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
