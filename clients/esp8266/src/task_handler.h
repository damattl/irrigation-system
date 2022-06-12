//
// Created by mattl on 11/06/2022.
//
#ifndef ESP8266_TASK_HANDLER_H
#define ESP8266_TASK_HANDLER_H
#include <Arduino.h>
#include <task.h>

class TaskHandler {
public:
    int add(Task &task);
    int add(void (*callback_ptr)(), unsigned long execute_in);
    void remove(int index);
    void loop();
private:
    static const int MAX_TASKS = 100;
    Task* scheduled_tasks[MAX_TASKS] = { nullptr }; // TODO: Is this enough space or even to much?
};

#endif //ESP8266_TASK_HANDLER_H
