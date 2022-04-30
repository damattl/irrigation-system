//
// Created by mattl on 08/04/2022.
//

#ifndef IRRIGATION_SYSTEM_MOISTURE_SENSOR_H
#define IRRIGATION_SYSTEM_MOISTURE_SENSOR_H
#include <Arduino.h>
#include <config.h>
#include <PubSubClient.h>

void publishSensorData(float sensor_data, PubSubClient& client, String& sensor_id); // TODO: Don't use String
float readSensor(unsigned int sensor_pin);

#endif //IRRIGATION_SYSTEM_MOISTURE_SENSOR_H
