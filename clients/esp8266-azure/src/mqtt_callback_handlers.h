
#ifndef IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
#define IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
#include <Arduino.h>
#include <map>
#include "utilities.h"
#include "moisture_sensor.h"
#include "connection_handlers.h"
#include "pins.h"

void handleMoistureSensor(String& topic, String& msg);
void handleValve(String& topic, String& msg);
void callbackHandler(char* topic, byte* payload, unsigned int length);
#endif //IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H