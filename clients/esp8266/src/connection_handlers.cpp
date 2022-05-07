//
// Created by mattl on 08/04/2022.
//

#include "connection_handlers.h"


WiFiClient espClient;
PubSubClient client(espClient);

void setupWifi() {
    WiFi.begin(WIFI_SSID, WIFI_PASS);
    while (WiFi.status() != WL_CONNECTED) {
        delay(100);
    }

    Serial.println(WiFi.localIP());
}

void subscribeToTopics() {
    if (!client.connected()) {
        return;
    }
    for (int i = 0; i < subscriptions_size; i++) {
        client.subscribe(subscriptions[i].c_str());
        Serial.print("Subscribing to: ");
        Serial.println(subscriptions[i]);
    }
}

void reconnect() {
    while (!client.connected()) {
        Serial.print("Attempting MQTT connection...");
        // clientId += String(random(0xffff), HEX);
        // if (client.connect(clientId.c_str(), MQTT_USER, MQTT_PASS)) {
        if (client.connect(DEVICE_ID)) {
            Serial.println("connected");
            subscribeToTopics();
        } else {
            Serial.print("failed, rc=");
            Serial.print(client.state());
            Serial.println(" try again in 5 seconds");
            delay(5000);
        }
    }
}

