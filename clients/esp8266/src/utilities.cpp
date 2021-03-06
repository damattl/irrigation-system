#include <utilities.h>

std::vector<String> splitStr(String& str, char delimiter) {
    std::vector<String> str_vector{};
    String sub_str;

    for(char i : str) {
        if (i == delimiter) {
            str_vector.push_back(String(sub_str));
            sub_str = "";
            continue;
        } else {
            sub_str += i;
        }
    }
    str_vector.push_back(String(sub_str));

    return str_vector;
}

// TODO: Do all this with C Strings
bool isTopic(String topic_template, String topic) {

    auto topicVector = splitStr(topic, '/');
    auto topicTemplateVector = splitStr(topic_template, '/');

    unsigned int tv_size = topicVector.size();
    unsigned int ttv_size = topicTemplateVector.size();

    if (tv_size < 2 || ttv_size < 2) {
        return false;
    }

    for (unsigned int i = 1; i < ttv_size; i++) {
        if (i > tv_size) {
            return false;
        }
        if (topicVector[i] == topicTemplateVector[i]) {
            continue;
        } else if(topicTemplateVector[i] == "+") {
            continue;
        } else if(topicTemplateVector[i] == "#") {
            return true;
        } else {
            return false;
        }
    }

    if (ttv_size < tv_size) {
        return topicTemplateVector[ttv_size - 1] == "#";
    }

    return true;
}
