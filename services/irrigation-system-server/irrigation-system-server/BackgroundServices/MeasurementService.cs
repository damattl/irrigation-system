using IrrigationSystemServer.Data;
using Microsoft.EntityFrameworkCore;

namespace IrrigationSystemServer.BackgroundServices;

public class MeasurementService : IHostedService, IDisposable
{
    private Timer? _timer = null;
    private IServiceProvider _serviceProvider;

    public MeasurementService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        ReadAllSensors();

        await Task.Delay(TimeSpan.FromSeconds(60));
        
        OpenValves();
    }

    private async void ReadAllSensors()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var brokerGrpc = scope.ServiceProvider.GetService<BrokerGrpc.BrokerGrpcClient>();

            if (context == null || brokerGrpc == null)
            {
                Console.WriteLine("Dependency Injection not working");
                return;
            }

            var profiles = await context.IrrigationProfiles
                .Include(p => p.MoistureSensor)
                .ToListAsync();
            foreach (var irrigationProfile in profiles)
            {
                if (irrigationProfile.MoistureSensor == null)
                {
                    continue;
                }

                var request = new ReadSensorRequest
                {
                    Info = new SensorInfo
                    {
                        ClientId = irrigationProfile.DeviceId.ToString(),
                        SensorPin = irrigationProfile.MoistureSensor.PinId
                    }
                };

                await brokerGrpc.ReadSensorAsync(request);
            }
        }
        
    }

    private async void OpenValves()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var brokerGrpc = scope.ServiceProvider.GetService<BrokerGrpc.BrokerGrpcClient>();

            if (context == null || brokerGrpc == null)
            {
                Console.WriteLine("Dependency Injection not working");
                return;
            }
        
            var profiles = await context.IrrigationProfiles
                .Include(p => p.Valve)
                .Include(p => p.PlantProfile)
                .ToListAsync();
        
            foreach (var irrigationProfile in profiles)
            {
                if (irrigationProfile.Valve == null || irrigationProfile.PlantProfile == null)
                {
                    continue;
                }
            
                // TODO: Check for threshold

                var request = new OpenValveRequest
                {
                    Info = new ValveInfo
                    {
                        ClientId = irrigationProfile.DeviceId.ToString(),
                        ValvePin = irrigationProfile.Valve.PinId
                    },
                    Duration = irrigationProfile.PlantProfile.WaterConsumption * 10 // Opening factor
                };

                await brokerGrpc.OpenValveAsync(request);
            }
        }
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}