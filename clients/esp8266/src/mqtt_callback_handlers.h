
#ifndef IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
#define IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
#include <Arduino.h>
#include <utilities.h>
#include <moisture_sensor.h>
#include <connection_handlers.h>
#include <map>
void handle_moisture_sensor(String& topic, String& msg);
void handle_valve(String& topic, String& msg);
#endif //IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
