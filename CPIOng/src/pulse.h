/*
 * pulse.h
 *
 *  Created on: 29.11.2018
 *      Author: tbe241
 */

#ifndef PULSE_H_
#define PULSE_H_

#include "include.h"

volatile static uint32_t tickMs;
volatile static uint32_t lastTimeValue[8];

void SetTimerPulseCorrecturFactor(uint16_t value);
void InitPulse(void);
void InitPulseSender(void);



// todo mb: eigenes file f�r channel modi
typedef enum  { None, Read, Namur, Licht, FlipFlop } ChannelModiType;
typedef struct {
	ChannelModiType channelModiType;
} ChannelModi;
void ChangeChannelModi(uint8_t channel, ChannelModiType channelModiType);


#endif /* PULSE_H_ */
