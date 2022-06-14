//
// Created by mattl on 11/06/2022.
//
#include "task_handler.h"

int TaskHandler::add(task_t *task) {
    for (int i = 0; i < MAX_TASKS; i++) {
        task_t* t = this->scheduled_tasks[i];
        if (t == nullptr) {
            this->scheduled_tasks[i] = task;
            return i;
        }
    }
    return -1; // TODO: Handle overflow error;
}

int TaskHandler::add(task_cb_t cb, void *data, size_t data_size, unsigned long execute_in) {
    time_t scheduled_time = millis() + execute_in;

    task_t* task = (task_t*)(malloc(sizeof(task_t)));
    task->scheduled_time = scheduled_time;
    task->cb = cb;

    void* data_ptr = malloc(data_size);
    memcpy(data_ptr, data, data_size);

    task->data = data_ptr;

    return this->add(task);
}

void TaskHandler::remove(int index) {
    free(this->scheduled_tasks[index]);
    this->scheduled_tasks[index] = nullptr;
}

void TaskHandler::loop() {
    for (int i = 0; i < MAX_TASKS; i++) {
        unsigned long now = millis();
        task_t* task = this->scheduled_tasks[i];
        if (task == nullptr) {
            continue;
        }
        if (now >= task->scheduled_time) {
            executeTask(*task);
            this->remove(i);
        }
    }
}

TaskHandler taskHandler;


