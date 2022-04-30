//
// Created by mattl on 30/04/2022.
//

#ifndef ESP8266_AZURE_SECURITY_H
#define ESP8266_AZURE_SECURITY_H
#include <Arduino.h>
#include <az_core.h>
#include <az_iot.h>
#include <azure_ca.h>
#include <base64.h>
#include <bearssl/bearssl.h>
#include <bearssl/bearssl_hmac.h>
#include <libb64/cdecode.h>
#include "secrets.h"

int generateSasToken(az_iot_hub_client &client, char* sas_token, size_t size);

#endif //ESP8266_AZURE_SECURITY_H
