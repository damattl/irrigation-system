
#ifndef IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
#define IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
#include <Arduino.h>
#include <map>
#include "utilities.h"
#include "moisture_sensor.h"
#include "connection_handlers.h"
#include "task_handler.h"
#include "time_makros.h"
#include "pins.h"
void handleMoistureSensor(String& topic, String& msg);
void handleValve(String& topic, String& msg);
#endif //IRRIGATION_SYSTEM_MQTT_CALLBACK_HANDLERS_H
