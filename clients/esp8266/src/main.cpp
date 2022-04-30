#include <Arduino.h>
#include <connection_handlers.h>
#include <mqtt_callback_handlers.h>
#include <moisture_sensor.h>


void callback(char* topic, byte* payload, unsigned int length) {
    String topic_str = String(topic);
    String msg;
     for (byte i = 0; i < length; i++) {
        char tmp = char(payload[i]);
        msg += tmp;
    }
    // Serial.println(msg);


    if (isTopic("/home/irrigation-system/ESP8266Client-testing/valves/#", topic_str)) {
        handleValve(topic_str, msg);
    }
    if (isTopic("/home/irrigation-system/ESP8266Client-testing/moisture-sensors/#", topic_str)) {
        handleMoistureSensor(topic_str, msg);
    }

    /* if (String(topic) == "/home/irrigation-system/magnet-pin") {
        handle_magnet_pin(MAGNET_PIN, msg);
    } else {
        handle_message(msg);
    } */

}

void setup() {
    Serial.begin(115200);
    setupWifi();
    client.setServer(MQTT_BROKER, MQTT_PORT);
    client.setCallback(callback);
    // Configuring Valve-Pins
    pinMode(D5, OUTPUT);
    pinMode(D6, OUTPUT);
    pinMode(D7, OUTPUT);

    // Configuring Sensor-Pins
    pinMode(D1, OUTPUT);
    pinMode(D2, OUTPUT);
    pinMode(D4, OUTPUT);
    // digitalWrite(MAGNET_PIN, HIGH);
}


void loop() {
// write your code here
    if (!client.connected()) {
        reconnect();
    }
    // float sensor_read = readSensor();

    // delay(5000);

    client.loop();

}


// TODO: Establish secure https connection
