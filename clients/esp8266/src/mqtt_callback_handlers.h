
#ifndef IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
#define IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
#include <Arduino.h>
#include <utilities.h>
#include <moisture_sensor.h>
#include <connection_handlers.h>
#include <map>
void handle_message(const String& msg);
void handle_magnet_pin(int pin, String& msg);
void handle_pin(std::vector<String>& msgVector);
void handle_moisture_sensor(String& topic, String& msg);
#endif //IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
