//
// Created by mattl on 30/04/2022.
//

#ifndef ESP8266_PINS_H
#define ESP8266_PINS_H
#include <Arduino.h>
#include <map>

struct Pin {
    int value = -1;
};

extern std::map<String, Pin> pin_map;

extern std::map<String, u_int8_t> sensor_pins;

extern std::map<String, u_int8_t> valve_pins;
#endif //ESP8266_PINS_H
