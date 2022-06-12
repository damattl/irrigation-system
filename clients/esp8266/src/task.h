//
// Created by mattl on 11/06/2022.
//

#ifndef ESP8266_TASK_H
#define ESP8266_TASK_H

typedef void (*task_cb_t)(void *data);

class Task {
public:
    Task(unsigned long scheduled_time, task_cb_t cb, void *data);
    void execute() const;
    unsigned long scheduled_time;
    task_cb_t cb;
    void *data;
};



#endif //ESP8266_TASK_H
