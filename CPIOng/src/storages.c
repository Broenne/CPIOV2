/*
 * storages.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "storages.h"

#define ADDRESS_CAN_ID  ((uint16_t)0x0000)
#define ADDRESS_ACTIVE_PULSE_SENSOR  ((uint16_t)0x0004)
#define ADDRESS_CHANNEL_MODI  ((uint16_t)0x0008)

void SafeGlobalCanId(uint16_t id) {
	if (!EE_Write(ADDRESS_CAN_ID, id)) {
		// info m: der fehler schent ignoriert werden zu können
		//printf("Error in write eeprrom");
	}
}

uint16_t GetGloablCanIdFromEeeprom(void) {
	uint32_t xxx = 0;
	if (!EE_Read(ADDRESS_CAN_ID, &xxx)) {
		printf("Error in read eeprrom");
	}

	return xxx;
}

void SafeUsedActiveSensorType(uint8_t channelModiType) {
	if (!EE_Write(ADDRESS_ACTIVE_PULSE_SENSOR, (uint8_t) channelModiType)) {
		// info m: der fehler schent ignoriert werden zu können
	}
}

uint8_t GetUsedActiveSensorType(void) {
	uint32_t xxx = 0;
	if (!EE_Read(ADDRESS_ACTIVE_PULSE_SENSOR, &xxx)) {
		printf("Error in read eeprrom");
	}

	return (uint8_t) xxx;
}

uint CalculateChannelStorageAddress(uint channel) {
	uint storageAddress = ADDRESS_CHANNEL_MODI + (channel * 4);
	return storageAddress;
}

/*
 * Created on: 11.02.19
 * Author: MB
 * Function for save channel modi to eeprom
 * */
void SafeChannelConfig(uint channel, uint8_t channelModiType) {
	uint storageAddress = CalculateChannelStorageAddress(channel);
	if (!EE_Write(storageAddress, (uint8_t) channelModiType)) {
	}
}

/*
 * Created on: 11.02.19
 * Author: MB
 * Function get channel modi from eeprom
 * */
uint8_t GetStoredChannelModi(uint channel) {
	uint32_t modi = 0;
	uint storageAddress = CalculateChannelStorageAddress(channel);
	if (!EE_Read(storageAddress, &modi)) {
		printf("Error in read eeprrom channel modi"); // todo mb: richtige fehlerbehandlung
	}

	return modi;
}

