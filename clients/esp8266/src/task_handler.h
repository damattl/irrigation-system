//
// Created by mattl on 11/06/2022.
//
#ifndef ESP8266_TASK_HANDLER_H
#define ESP8266_TASK_HANDLER_H
#include <Arduino.h>
#include "task.h"

class TaskHandler {
public:
    int add(task_cb_t cb, void *data, unsigned long execute_in);
    int add(Task &task);
    void remove(int index);
    void loop();
private:
    static const int MAX_TASKS = 100;
    Task* scheduled_tasks[MAX_TASKS] = { nullptr }; // TODO: Is this enough space or even to much?
};

extern TaskHandler taskHandler;
#endif //ESP8266_TASK_HANDLER_H
