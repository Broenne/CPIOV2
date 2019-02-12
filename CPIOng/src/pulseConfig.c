/*
 * pulseConfig.c
 *
 *  Created on: 19.12.2018
 *      Author: MB
 */

#include "pulseConfig.h"

static ChannelModi ChannelModiStorage[CHANNEL_COUNT];

static ChannelModiType ActivatedChannelModi = Licht;

void SetActiveChannelModiType(ChannelModiType val) {
	ActivatedChannelModi = val;
	SafeUsedActiveSensorType((uint8_t) ActivatedChannelModi);
}

ChannelModiType GetChannelModiByChannel(int ch) {
	return ChannelModiStorage[ch].channelModiType;
}

ChannelModiType GetActiveChannelModiType(void) {
	// todo mb: aus eeprom laden bei initialisierung?
	return ActivatedChannelModi;
}

void ChangeChannelModi(uint8_t channel, ChannelModiType channelModiType) {
	// printf("ch:%d mod:%d \r\n", channel, channelModiType);
	ChannelModiStorage[channel].channelModiType = channelModiType;
}

/*
 * Created on: 11.02.19
 * Author: MB
 * Function for save channel modi to eeprom
 * */
void SaveChannelToEeprom(void) {
	for (int i = 0; i < CHANNEL_COUNT; ++i) {
		SafeChannelConfig(i, ChannelModiStorage[i].channelModiType);
	}
}

void InitChannelModi(void) {

	// todo mb: active channel bei start laden

	ActivatedChannelModi =(ChannelModiType)GetUsedActiveSensorType();


	for (int i = 0; i < CHANNEL_COUNT; ++i) {
		ChannelModiStorage[i].channelModiType = GetStoredChannelModi(i);
	}
}
