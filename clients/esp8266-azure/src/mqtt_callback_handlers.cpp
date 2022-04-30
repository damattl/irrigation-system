#include <mqtt_callback_handlers.h>

// TODO: Check if these functions are still needed


void handleMoistureSensor(String& topic, String& msg) {
    std::vector<String> topic_vector = splitStr(topic, '/');

    String sensor_id = topic_vector[topic_vector.size() - 1]; // TODO: Error handling?
    u_int8_t pin = sensor_pins[sensor_id];
    if (pin == 0) {
        return;
    }

    std::vector<String> msg_vector = splitStr(msg, ':'); // 1:0 // TODO: Implement without Strings, but Ints?

    if (msg_vector[0] == "1") {
        float sensor_data = readSensor(pin);
        publishSensorData(sensor_data, mqtt_client, sensor_id);
    }
}

void handleValve(String& topic, String& msg) {
    std::vector<String> topic_vector = splitStr(topic, '/');

    String valve_id = topic_vector[topic_vector.size() - 1];
    u_int8_t pin = valve_pins[valve_id];
    if (pin == 0) {
        return;
    }

    std::vector<String> msg_vector = splitStr(msg, ':');


    if (msg_vector[0] == "1") {
        digitalWrite(pin, HIGH);
        if (msg_vector.size() > 1) {
            int time_opened = msg_vector[1].toInt() * 1000;
            delay(time_opened);
        } else {
            delay(1000);
        }
        digitalWrite(pin, LOW);
    }
    if (msg_vector[0] == "0") {
        digitalWrite(pin, LOW);
    }
}



void callbackHandler(char* topic, byte* payload, unsigned int length) {
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

}

