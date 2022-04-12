using System.Text.RegularExpressions;
using MQTTnet.Packets;
using MQTTnet.Server;

namespace MQTTBroker;

public class RouteHandler
{
    public RouteHandler(string routeTemplate, Regex regex, Action<(RouteHandler handler, string clientId, MqttPublishPacket packet)> action)
    {
        RouteTemplate = routeTemplate;
        Regex = regex;
        Action = action;
    }
    public string RouteTemplate { get; set; }
    public Regex Regex { get; set; }
    public Action<(RouteHandler handler, string clientId, MqttPublishPacket packet)> Action { get; set; }
}

public class MQTTRouter
{
    private readonly Dictionary<string, RouteHandler> _registeredRoutes = new Dictionary<string, RouteHandler>();

    public void RegisterRoute(string routeTemplate, Action<(RouteHandler handler, string clientId, MqttPublishPacket packet)> action)
    {
        var regex = new Regex(routeTemplate, RegexOptions.Compiled);
        var handler = new RouteHandler(routeTemplate, regex, action);
        var id = Guid.NewGuid().ToString();
        _registeredRoutes[id] = handler;
    }

    public Task HandleInbound(InterceptingPacketEventArgs eventArgs)
    {
        if (eventArgs.Packet is MqttPublishPacket packet)
        {
            foreach (var (key, handler) in _registeredRoutes)
            {
                if (handler.Regex.IsMatch(packet.Topic))
                {

                    
                    handler.Action((handler, eventArgs.ClientId, packet));
                }
            }
        }

        return Task.CompletedTask;
    }
}