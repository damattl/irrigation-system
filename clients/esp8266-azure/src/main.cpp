#include <Arduino.h>
#include "connection_handlers.h"


void setup() {
    Serial.begin(115200);
    establishConnection();
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
    if (!mqtt_client.connected()) {
        establishConnection();
    }
    // float sensor_read = readSensor();

    // delay(5000);

    mqtt_client.loop();
    delay(500); // TODO: Test if ok
}

