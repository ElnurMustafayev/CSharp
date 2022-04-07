using Consumer.Services;
using Helper.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// options 
builder.Services.AddOptions();

var serviceClientSettingsConfig = builder.Configuration.GetSection("RabbitMq");
builder.Services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

// services
builder.Services.AddSingleton<IUserService, UserService>();

// background
builder.Services.AddHostedService<UserReceiver>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
