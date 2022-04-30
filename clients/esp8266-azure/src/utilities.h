
#ifndef IRRIGATION_SYSTEM_UTILITIES_H
#define IRRIGATION_SYSTEM_UTILITIES_H
#include <Arduino.h>
std::vector<String> splitStr(String& str, char delimiter);
bool isTopic(String topic_template, String topic);
#endif //IRRIGATION_SYSTEM_UTILITIES_H

