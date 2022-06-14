//
// Created by mattl on 11/06/2022.
//

#ifndef ESP8266_TASK_H
#define ESP8266_TASK_H
#include <Arduino.h>

typedef void (*task_cb_t)(void *data);
typedef struct task_t task_t;
struct task_t {
    task_cb_t cb;
    void *data;
    time_t scheduled_time;
};

extern void executeTask(task_t &task);



#endif //ESP8266_TASK_H
