/*
 * pulseConfig.c
 *
 *  Created on: 19.12.2018
 *      Author: MB
 */


#include "pulseConfig.h"



static ChannelModi ChannelModiStorage[CHANNEL_COUNT];

static ChannelModiType ActivatedChannelModi = Licht;



void SetActiveChannelModiType(ChannelModiType val){
	ActivatedChannelModi = val;
	SafeUsedActiveSensorType((uint8_t)ActivatedChannelModi);
	// ins eeprom speichern, wenn egändert
}

ChannelModiType GetChannelModiByChannel(int ch){
	return ChannelModiStorage[ch].channelModiType;
}

ChannelModiType GetActiveChannelModiType(void){
	// todo mb: aus eeprom laden bei initialisierung?
	return ActivatedChannelModi;
}

void ChangeChannelModi(uint8_t channel, ChannelModiType channelModiType){
	printf("ch:%d mod:%d \r\n", channel, channelModiType);
	ChannelModiStorage[channel].channelModiType = channelModiType; // todo mb: das ist scheiße. hier kommt die Gefahr das man was durcheinader wirft
}

void InitChannelModi(void){

		//ActivatedChannelModi = ChannelModiType.//(ChannelModiType)GetUsedActiveSensorType();

	    // todo mb: von außen initialisieren und in eeprom abspeichern
		// der channel wird extra abgespeichert und nicht über die Position im Array behandelt. Überscihtlicher!
		ChannelModiStorage[0].channelModiType = Licht;
		ChannelModiStorage[1].channelModiType = Licht;
		ChannelModiStorage[2].channelModiType = Licht;
		ChannelModiStorage[3].channelModiType = Licht;
		ChannelModiStorage[4].channelModiType = Licht;
		ChannelModiStorage[5].channelModiType = Licht;
		ChannelModiStorage[6].channelModiType = Licht;
		ChannelModiStorage[7].channelModiType = Licht;
		ChannelModiStorage[8].channelModiType = Licht;
		ChannelModiStorage[9].channelModiType = Licht;
		ChannelModiStorage[10].channelModiType = Licht;
		ChannelModiStorage[11].channelModiType = Licht;
		ChannelModiStorage[12].channelModiType = Licht;
		ChannelModiStorage[13].channelModiType = Licht;
		ChannelModiStorage[14].channelModiType = Licht;
		ChannelModiStorage[15].channelModiType = Licht;
}

