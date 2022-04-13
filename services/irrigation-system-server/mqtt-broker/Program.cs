using Grpc.Net.Client;
using MQTTBroker;
using MQTTnet.Server;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682




var brokerOptions = new MqttServerOptionsBuilder()
    .WithDefaultEndpointPort(1884)
    .WithDefaultEndpoint()
    .Build();

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddGrpcClient<ServerGrpc.ServerGrpcClient>(opt =>
{
    opt.Address = new Uri("https://localhost:7019");
});
builder.Services.AddSingleton<MqttRouter>();
builder.Services.AddSingleton<IMqttBroker>(x => 
    new MqttBroker(x.GetRequiredService<MqttRouter>(), brokerOptions));

var app = builder.Build();



// Configure the HTTP request pipeline.
app.MapGrpcService<BrokerGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

var broker = app.Services.GetRequiredService<IMqttBroker>();
var brokerThread = new Thread(broker.StartAsync)
{
    IsBackground = true
};
brokerThread.Start();

app.Run();







