//
// Created by mattl on 11/06/2022.
//

#ifndef ESP8266_TASK_H
#define ESP8266_TASK_H

typedef void (*task_cb_t)(void *data);
typedef struct task_t task_t;
struct task_t {
    unsigned long scheduled_time;
    task_cb_t cb;
    void *data;
};

extern void executeTask(task_t &task);



#endif //ESP8266_TASK_H
