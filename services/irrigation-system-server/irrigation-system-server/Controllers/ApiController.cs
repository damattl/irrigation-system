using Microsoft.AspNetCore.Mvc;

namespace irrigation_system_server.Controllers;

public class ApiController : Controller
{
    private readonly BrokerGrpc.BrokerGrpcClient _client;
    public ApiController(BrokerGrpc.BrokerGrpcClient client)
    {
        _client = client;
    }

    [HttpPost]
    [Route("Home/SendData")]
    public IActionResult SendData([FromBody]SimpleData data)
    {
        Console.WriteLine(data);
        Console.WriteLine("Data received: " + data.Message);
        return Ok();
    }
    
    [HttpPost]
    [Route("Home/CallValve")]
    public IActionResult SendData([FromBody]ValveData data)
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