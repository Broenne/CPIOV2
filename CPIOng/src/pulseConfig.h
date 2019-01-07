/*
 * pulseConfig.h
 *
 *  Created on: 19.12.2018
 *      Author: MB
 */

#ifndef PULSECONFIG_H_
#define PULSECONFIG_H_


#include "include.h"

typedef enum  { None, Read, Namur, Licht, FlipFlop } ChannelModiType;

typedef struct {
	ChannelModiType channelModiType;
} ChannelModi;

void ChangeChannelModi(uint8_t channel, ChannelModiType channelModiType);
void InitChannelModi(void);
void SetActiveChannelModiType(ChannelModiType val);
ChannelModiType GetActiveChannelModiType(void);
ChannelModiType GetChannelModiByChannel(int ch);

#endif /* PULSECONFIG_H_ */
