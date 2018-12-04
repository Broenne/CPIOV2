/*
 * rtosActions.h
 *
 *  Created on: 04.12.2018
 *      Author: MB
 */

#ifndef RTOSACTIONS_H_
#define RTOSACTIONS_H_

#include "main.h"

void vApplicationIdleHook(void);

void vApplicationMallocFailedHook(void) ;
void vApplicationStackOverflowHook(xTaskHandle pxTask, signed char *pcTaskName) ;
void vApplicationTickHook(void);

#endif /* RTOSACTIONS_H_ */
