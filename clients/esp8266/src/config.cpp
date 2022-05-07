//
// Created by mattl on 12/04/2022.
//

#include "config.h"

String subscriptions[] = {"/home/irrigation-system/"+ String(DEVICE_ID) +"/#"};
unsigned int subscriptions_size = sizeof(subscriptions) / sizeof(subscriptions[0]);