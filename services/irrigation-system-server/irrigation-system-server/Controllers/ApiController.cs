using Microsoft.AspNetCore.Mvc;

namespace irrigation_system_server.Controllers;

[Route("Home")]
public class ApiController : Controller
{
    private readonly BrokerGrpc.BrokerGrpcClient _client;
    public ApiController(BrokerGrpc.BrokerGrpcClient client)
    {
        _client = client;
    }

    [HttpPost]
    [Route("SendData")]
    public IActionResult SendData([FromBody]SimpleData data)
    {
        Console.WriteLine(data);
        Console.WriteLine("Data received: " + data.Message);
        return Ok();
    }
    
    [HttpPost]
    [Route("OpenValve")]
    public IActionResult OpenValve([FromBody]ValveData data)
    {
        _client.OpenValve(new OpenValveRequest()
        {
            Duration = data.Duration ?? 5,
            Info = new ValveInfo()
            {
                ClientId = data.ClientId,
                ValveId = data.ValveId
            }
        });
        
        return Ok();
    }
    
    [HttpPost]
    [Route("CloseValve")]
    public IActionResult CloseValve([FromBody]ValveData data)
    {
        _client.CloseValve(new CloseValveRequest()
        {
            Info = new ValveInfo()
            {
                ClientId = data.ClientId,
                ValveId = data.ValveId
            }
        });
        
        return Ok();
    }
    
    [HttpPost]
    [Route("ReadSensor")]
    public IActionResult ReadSensor([FromBody]SensorData data)
    {
        _client.ReadSensor(new ReadSensorRequest()
        {
            Info = new SensorInfo()
            {
                ClientId = data.ClientId,
                SensorId = data.SensorId
            }
        });
        
        return Ok();
    }
}

public class SimpleData
{
    public string? Message { get; set; }
}

public class ValveData
{
    public string? ClientId { get; set; }
    public string? ValveId { get; set; }
    public int? Duration { get; set; }
}

public class SensorData
{
    public string? ClientId { get; set; }
    public string? SensorId { get; set; }
}