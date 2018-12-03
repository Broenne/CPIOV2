/*
 * helper.h
 *
 *  Created on: 29.11.2018
 *      Author: tbe241
 */

#ifndef HELPER_H_
#define HELPER_H_

#include "main.h"

#define COUNTS_PER_MICROSECOND 12 //f�r die 12 MHz STM32F1

void delay_ms(unsigned int d);
void delay_us(unsigned int d);

#endif /* HELPER_H_ */