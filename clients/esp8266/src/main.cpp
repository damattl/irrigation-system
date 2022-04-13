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


    if (is_topic("/home/irrigation-system/ESP8266Client-testing/valves/#", topic_str)) {
        Serial.println(msg);
    }
    if (is_topic("/home/irrigation-system/ESP8266Client-testing/moisture-sensors/#", topic_str)) {
        handle_moisture_sensor(topic_str, msg);
    }

    /* if (String(topic) == "/home/irrigation-system/magnet-pin") {
        handle_magnet_pin(MAGNET_PIN, msg);
    } else {
        handle_message(msg);
    } */

}

void setup() {
    Serial.begin(115200);
    setup_wifi();
    client.setServer(MQTT_BROKER, MQTT_PORT);
    client.setCallback(callback);
    pinMode(MAGNET_PIN, OUTPUT);
    // digitalWrite(MAGNET_PIN, HIGH);
}


void loop() {
// write your code here
    if (!client.connected()) {
        reconnect();
    }

    // float sensor_read = read_sensor();

    // delay(5000);

    client.loop();

}


// TODO: Establish secure https connection
