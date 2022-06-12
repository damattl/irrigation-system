//
// Created by mattl on 11/06/2022.
//

#include "task.h"



Task::Task(unsigned long scheduled_time, task_cb_t cb, void *data) {
    this->cb = cb;
    this->scheduled_time = scheduled_time;
    this->data = data; // TODO: Check if malloc needed
}

void Task::execute() const {
    this->cb(this->data);
}