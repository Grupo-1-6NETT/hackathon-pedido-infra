using Consumer;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = builder.Configuration;

builder.Services.AddConsumerDI(configuration);
builder.Services.ConfigureRabbitMQ(configuration);

var host = builder.Build();
host.Run();
