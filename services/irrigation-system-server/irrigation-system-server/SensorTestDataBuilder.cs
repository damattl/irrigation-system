using System.Globalization;
using IrrigationSystemServer.Models;

namespace IrrigationSystemServer;

public static class SensorTestDataBuilder
{
    public static List<MoistureSensorData> Build(Guid sensorId, int count = 10)
    {
        var sensorDataList = new List<MoistureSensorData>();
        var random = new Random();
        for (int i = 0; i < count; i++)
        {
            var sensorReading = (float)random.Next(50, 400);
            var timeStamp = DateTime.Now.AddHours(-12 * (count - i)).ToBinary();
            var sensorData = new MoistureSensorData
            {
                MoistureSensorId = sensorId,
                SensorReading = sensorReading,
                TimeStamp = timeStamp
            };
            sensorDataList.Add(sensorData);
        }

        return sensorDataList;
    }
    
}