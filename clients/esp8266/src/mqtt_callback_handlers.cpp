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
        publishSensorData(sensor_data, client, sensor_id);
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
        Serial.print("Opening valve at pin: ");
        Serial.println(pin);
        if (msg_vector.size() > 1) {
            int time_opened = msg_vector[1].toInt() * 1000;
            taskHandler.add([](void *data) {
                digitalWrite(*(uint8_t*)data, LOW);
            }, &pin,time_opened);
        } else {
            delay(1000);
        }

    }
    if (msg_vector[0] == "0") {
        digitalWrite(pin, LOW);
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
    std::vector<String> msgVector = splitStr(msg, ':');
    Serial.println(msgVector.size());

    if ( && msgVector[0] == "0") {
        handle_pin(msgVector); //TODO: Might produce undefined behavior
    }  else {
        Serial.println(msg);
    }

} */