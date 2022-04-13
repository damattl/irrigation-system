//
// Created by mattl on 08/04/2022.
//

#include "connection_handlers.h"


WiFiClient espClient;
PubSubClient client(espClient);

void setup_wifi() {
    WiFi.begin(WIFI_SSID, WIFI_PASS);
    while (WiFi.status() != WL_CONNECTED) {
        delay(100);
    }

    Serial.println(WiFi.localIP());
}

void subscribe_to_topics() {
    if (!client.connected()) {
        return;
    }
    for (int i = 0; i < subscriptions_size; i++) {
        client.subscribe(subscriptions[i].c_str());
    }
}

void reconnect() {
    while (!client.connected()) {
        Serial.print("Attempting MQTT connection...");
        String clientId = "ESP8266Client-testing";
        // clientId += String(random(0xffff), HEX);
        // if (client.connect(clientId.c_str(), MQTT_USER, MQTT_PASS)) {
        if (client.connect(clientId.c_str())) {
            Serial.println("connected");
            subscribe_to_topics();
        } else {
            Serial.print("failed, rc=");
            Serial.print(client.state());
            Serial.println(" try again in 5 seconds");
            delay(5000);
        }
    }
}

