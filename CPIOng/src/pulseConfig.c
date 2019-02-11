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
}

ChannelModiType GetChannelModiByChannel(int ch){
	return ChannelModiStorage[ch].channelModiType;
}

ChannelModiType GetActiveChannelModiType(void){
	// todo mb: aus eeprom laden bei initialisierung?
	return ActivatedChannelModi;
}

void ChangeChannelModi(uint8_t channel, ChannelModiType channelModiType){
	// printf("ch:%d mod:%d \r\n", channel, channelModiType);
	ChannelModiStorage[channel].channelModiType = channelModiType;
}

/*
 * Created on: 11.02.19
 * Author: MB
 * Function for save channel modi to eeprom
 * */
void SaveChannelToEeprom(void){
	for(int i=0;i<CHANNEL_COUNT;++i){
		SafeChannelConfig(i, ChannelModiStorage[i].channelModiType);
	}
}


void InitChannelModi(void){

		//ActivatedChannelModi = ChannelModiType.//(ChannelModiType)GetUsedActiveSensorType();

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
		ChannelModiStorage[13].channelModiType = FlipFlop;
		ChannelModiStorage[14].channelModiType = FlipFlop;
		ChannelModiStorage[15].channelModiType = FlipFlop;
}

