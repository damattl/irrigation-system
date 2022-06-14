//
// Created by mattl on 11/06/2022.
//

#include "task.h"



void executeTask(task_t &task) {
    task.cb(task.data);
}