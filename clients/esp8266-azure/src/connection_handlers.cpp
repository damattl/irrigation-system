//
// Created by mattl on 08/04/2022.
//
#include "connection_handlers.h"


#define AZURE_SDK_CLIENT_USER_AGENT "c/" AZ_SDK_VERSION_STRING "(ard;esp8266)"
#define sizeofarray(a) (sizeof(a) / sizeof(a[0]))
#define MQTT_PACKET_SIZE 1024

static const char* ssid = CONFIG_WIFI_SSID;
static const char* password = CONFIG_WIFI_PASSWORD;
static const char* device_id = CONFIG_DEVICE_ID;

WiFiClientSecure wifi_client;
PubSubClient mqtt_client(wifi_client);

static X509List cert((const char*)ca_pem);
static az_iot_hub_client az_client;
static char sas_token[200];


void subscribeToTopics() {
    if (!mqtt_client.connected()) {
        return;
    }
    for (int i = 0; i < subscriptions_size; i++) {
        mqtt_client.subscribe(subscriptions[i].c_str());
    }
}


void connectToWifi() {
    if (WiFi.status() == WL_CONNECTED) {
        return;
    }
    WiFi.mode(WIFI_STA);
    WiFi.begin(ssid, password);
    while (WiFi.status() != WL_CONNECTED) {
        delay(100);
    }
    Serial.println(WiFi.localIP());
}


void setupClients() {
    az_iot_hub_client_options  options = az_iot_hub_client_options_default();
    options.user_agent = AZ_SPAN_FROM_STR(AZURE_SDK_CLIENT_USER_AGENT);

    wifi_client.setTrustAnchors(&cert);
    if (az_result_failed(az_iot_hub_client_init(
            &az_client,
            az_span_create((uint8_t*)AZURE_IOT_HUB, strlen(AZURE_IOT_HUB)),
            az_span_create((uint8_t*)device_id, strlen(device_id)),
            &options
            ))) {
        Serial.println("Failed initializing Azure IoT Hub client");
        return;
    }

    mqtt_client.setServer(AZURE_IOT_HUB, AZURE_IOT_PORT);
    mqtt_client.setCallback(callbackHandler);
}


int connectToAzureIotHub() {
    size_t az_client_id_length;
    char mqtt_client_id[128];
    if (az_result_failed(az_iot_hub_client_get_client_id(
            &az_client,
            mqtt_client_id,
            sizeof(mqtt_client_id) - 1,
            &az_client_id_length
            ))) {
        Serial.println("Failed getting client id");
        return 1;
    }
    mqtt_client_id[az_client_id_length] = '\0';

    char mqtt_username[128];
    if (az_result_failed(az_iot_hub_client_get_user_name(
            &az_client,
            mqtt_username,
            sizeofarray(mqtt_username),
            NULL
            ))) {
        printf("Failed to get MQTT clientId, return code\n");
        return 1;
    }

    Serial.print("Client ID: ");
    Serial.println(mqtt_client_id);
    Serial.print("Username: ");
    Serial.println(mqtt_username);

    mqtt_client.setBufferSize(MQTT_PACKET_SIZE);

    while(!mqtt_client.connected()) {
        time_t now = time(NULL);

        Serial.println("MQTT connecting ...");

        if (mqtt_client.connect(mqtt_client_id, mqtt_username, sas_token)) {
            Serial.println("connected.");
        } else {
            Serial.print("failed, status code =");
            Serial.print(mqtt_client.state());
            Serial.println(". Trying again in 5 seconds.");
            delay(5000);
        }
    }

    mqtt_client.subscribe(AZ_IOT_HUB_CLIENT_C2D_SUBSCRIBE_TOPIC);
    return 0;
}


void establishConnection() {
    connectToWifi();
    setupClients();

    if (generateSasToken(az_client ,sas_token, sizeofarray(sas_token)) != 0)
    {
        Serial.println("Failed generating MQTT password");
    }
    else
    {
        connectToAzureIotHub();
    }
}


