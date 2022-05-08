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
#define DEVICE_ID "99e2dffb-5957-4d76-abf3-adbd57f738c4"

extern String subscriptions[];
extern unsigned int subscriptions_size;

#endif //ESP8266_CONFIG_H
