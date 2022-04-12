
#ifndef IRRIGATION_SYSTEM_UTILITIES_H
#define IRRIGATION_SYSTEM_UTILITIES_H
#include <Arduino.h>
std::vector<String> split_str(String& str, char delimiter);
bool is_topic(String topic_template, String topic);
#endif //IRRIGATION_SYSTEM_UTILITIES_H

