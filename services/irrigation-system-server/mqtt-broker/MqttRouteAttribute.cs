using System.Text.RegularExpressions;

namespace MQTTBroker;

[AttributeUsage(AttributeTargets.Class |  
                       AttributeTargets.Method)  
]  
public class MqttRouteAttribute : System.Attribute
{
    // private string _route;
    public Regex Regex;
    public MqttRouteAttribute(string route)
    {
        Regex = new Regex(route, RegexOptions.Compiled);
    }  
}