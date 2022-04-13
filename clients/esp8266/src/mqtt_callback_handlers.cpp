#include <mqtt_callback_handlers.h>

// TODO: Check if these functions are still needed

struct PinInfo {
    int pin = -1;
    bool pwm;
};

std::map<String, PinInfo> pin_map = {
        {"TX", {TX, true}},
        {"RX", {RX, true}},
        // {"A0", {A0, false}},
        {"D0", {16, false}},
        {"D1", {5, true}},
        {"D2", {4, true}},
        {"D3", {0, true}},
        {"D4", {2, true}},
        {"D5", {14, true}},
        {"D6", {12, true}},
        {"D7", {13, true}},
        {"D8", {15, true}}
};




void handle_moisture_sensor(String& topic, String& msg) {
    std::vector<String> topic_vector = split_str(topic, '/');

    String sensor_id = topic_vector[topic_vector.size()]; // TODO: Error handling?
    PinInfo info = pin_map[sensor_id];
    if (info.pin == -1) {
        return;
    }

    std::vector<String> msg_vector = split_str(msg, ':'); // 1:0 // TODO: Implement without Strings, but Ints?

    if (msg_vector[0] == "1") {
        float sensor_data = read_sensor(info.pin);
        publish_sensor_data(sensor_data, client);
    }
}



/*
void handle_write_pin(PinInfo& pin_info, int use_pwm, int pin_state, int duration) {
    if (use_pwm) {
        if (!pin_info.pwm) {
            return;
        }
        analogWrite(pin_info.pin, pin_state);
    } else {
        if (pin_state) {
            digitalWrite(pin_info.pin, LOW);
        } else {
            digitalWrite(pin_info.pin, HIGH);
        }
    }
}

void handle_read_pin() {

}


void handle_pin(std::vector<String>& msgVector) {
    if (!pin_map.count(msgVector[0])) {
        return;
    }
    PinInfo pin_info = pin_map[msgVector[1]]; // TODO: Check what this really does
    int pin_mode = msgVector[2].toInt() == 0 ? INPUT : OUTPUT;
    int use_pwm = msgVector[3].toInt();
    int pin_state = msgVector[4].toInt();
    int duration = msgVector[5].toInt();

    pinMode(pin_info.pin, pin_mode);

    if (pin_mode == OUTPUT) {
        handle_write_pin(pin_info, use_pwm, pin_state, duration);
    } else {
        handle_read_pin();
    }



}


void handle_magnet_pin(int pin, String& msg) {
    int pin_state = msg.toInt();
    digitalWrite(pin, pin_state == 0 ? LOW : HIGH);
}

void handle_message(String& msg) {
    std::vector<String> msgVector = split_str(msg, ':');
    Serial.println(msgVector.size());

    if ( && msgVector[0] == "0") {
        handle_pin(msgVector); //TODO: Might produce undefined behavior
    }  else {
        Serial.println(msg);
    }

} */