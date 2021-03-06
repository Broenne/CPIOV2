/*
 * pulse.h
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#ifndef PULSE_H_
#define PULSE_H_

#include "include.h"


volatile static uint32_t lastTimeValue[CHANNEL_COUNT];

void SetTimerPulseCorrecturFactor(uint16_t value);
void InitPulse(void);
void InitPulseSender(void);
uint32_t GetTicks();

#endif /* PULSE_H_ */
