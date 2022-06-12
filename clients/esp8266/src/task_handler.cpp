//
// Created by mattl on 11/06/2022.
//
#include "task_handler.h"

int TaskHandler::add(Task &task) {
    for (int i = 0; i < MAX_TASKS; i++) {
        Task* t = this->scheduled_tasks[i];
        if (t != nullptr) {
            this->scheduled_tasks[i] = &task;
            return i;
        }
    }
    return -1; // TODO: Handle overflow error;
}

int TaskHandler::add(void (*callback_ptr)(), unsigned long execute_in) {
    Task task(millis() + execute_in, callback_ptr);
    return this->add(task);
}

void TaskHandler::remove(int index) {
    this->scheduled_tasks[index] = nullptr;
}

void TaskHandler::loop() {
    for (int i = 0; i < MAX_TASKS; i++) {
        unsigned long now = millis();
        Task* task = this->scheduled_tasks[i];
        if (now >= task->scheduled_time) {
            task->execute();
            this->remove(i);
        }
    }
}





