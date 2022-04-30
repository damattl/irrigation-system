//
// Created by mattl on 08/04/2022.
//

#ifndef IRRIGATION_SYSTEM_CONNECTION_HANDLERS_H
#define IRRIGATION_SYSTEM_CONNECTION_HANDLERS_H
#include <Arduino.h>
#include <ESP8266WiFi.h>
#include <PubSubClient.h>
#include <az_core.h>
#include <az_iot.h>
#include <azure_ca.h>
#include "config.h"
#include "secrets.h"
#include "security.h"
#include "mqtt_callback_handlers.h"

void establishConnection();
void connectToWifi();
// extern WiFiClientSecure espClient;
extern WiFiClientSecure wifi_client;
extern PubSubClient mqtt_client;
#endif //IRRIGATION_SYSTEM_CONNECTION_HANDLERS_H
