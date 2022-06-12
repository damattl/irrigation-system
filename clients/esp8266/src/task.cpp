//
// Created by mattl on 11/06/2022.
//

#include "task.h"

Task::Task(unsigned long scheduled_time, void (*execute)()) {
    this->execute = execute;
    this->scheduled_time = scheduled_time;
}
