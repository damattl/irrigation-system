//
// Created by mattl on 08/04/2022.
//

#ifndef IRRIGATION_SYSTEM_CONNECTION_HANDLERS_H
#define IRRIGATION_SYSTEM_CONNECTION_HANDLERS_H
#include <Arduino.h>
#include <secrets.h>
#include <ESP8266WiFi.h>
#include <PubSubClient.h>
#include <config.h>

void reconnect();
void setupWifi();
extern WiFiClient espClient;
extern PubSubClient client;
#endif //IRRIGATION_SYSTEM_CONNECTION_HANDLERS_H
