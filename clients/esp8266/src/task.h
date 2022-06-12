//
// Created by mattl on 11/06/2022.
//

#ifndef ESP8266_TASK_H
#define ESP8266_TASK_H

class Task {
public:
    Task(unsigned long scheduled_time, void (*execute)());
    unsigned long scheduled_time;
    void (*execute)();
};

#endif //ESP8266_TASK_H
