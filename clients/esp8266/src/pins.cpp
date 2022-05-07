//
// Created by mattl on 07/05/2022.
//
#include "pins.h"

std::map<String, Pin> pin_map = {
        {"TX", {TX}},
        {"RX", {RX}},
        {"A0", {A0}},
        {"D0", {D0}},
        {"D1", {D1}},
        {"D2", {D2}},
        {"D3", {D3}},
        {"D4", {D4}},
        {"D5", {D5}},
        {"D6", {D6}},
        {"D7", {D7}},
        {"D8", {D8}}
};

std::map<String, u_int8_t> sensor_pins = {
        {"1", D1},
        {"2", D2},
        {"3", D4},
};

std::map<String, u_int8_t> valve_pins = {
        {"1", D5},
        {"2", D6},
        {"3", D7},
};