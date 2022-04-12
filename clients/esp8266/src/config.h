//
// Created by mattl on 12/04/2022.
//

#ifndef ESP8266_CONFIG_H
#define ESP8266_CONFIG_H
#include <Arduino.h>

#define MAGNET_PIN 4
#define SENSOR_PIN A0
#define MQTT_BROKER "192.168.2.100"
#define MQTT_PORT 1884

extern String subscriptions[];
extern int subscriptions_size;


#endif //ESP8266_CONFIG_H
