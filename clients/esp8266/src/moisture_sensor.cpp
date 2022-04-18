//
// Created by mattl on 08/04/2022.
//

#include "moisture_sensor.h"


float read_sensor(unsigned int sensor_pin) {
    Serial.println("Reading sensor");
    digitalWrite(sensor_pin, HIGH);

    float sensor_value = 0;
    for (int i = 0; i <= 100; i++) {
        sensor_value = sensor_value + analogRead(A0);
        delay(1);
    }
    digitalWrite(sensor_pin, LOW);
    sensor_value = sensor_value/100.0;
    // Calculates the average value of the measurement

    return sensor_value;
}

void publish_sensor_data(float sensor_data, PubSubClient& client, String& sensor_id) {
    char payload[8];
    dtostrf(sensor_data, 6, 2, payload);
    String topic = "/home/irrigation-system/ESP8266Client-testing/moisture-sensors/" + sensor_id;
    client.publish(topic.c_str(), payload);
}