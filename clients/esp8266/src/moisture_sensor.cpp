//
// Created by mattl on 08/04/2022.
//

#include "moisture_sensor.h"


float read_sensor(unsigned int sensor_pin) {
    float sensor_value = 0;
    for (int i = 0; i <= 100; i++) {
        sensor_value = sensor_value + analogRead(sensor_pin);
        delay(1);
    }
    sensor_value = sensor_value/100.0;
    // Calculates the average value of the measurement

    return sensor_value;
}

void publish_sensor_data(float sensor_data, PubSubClient& client) {
    char payload[8];
    dtostrf(sensor_data, 6, 2, payload);
    client.publish("/home/irrigation-system/moisture-sensors/1", payload);
}