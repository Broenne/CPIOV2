/*
 * storages.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "storages.h"

#define ADDRESS_CAN_ID  ((uint16_t)0x0000)
#define ADDRESS_ACTIVE_PULSE_SENSOR  ((uint16_t)0x0004)

void SafeGlobalCanId(uint8_t id) {
	if (!EE_Write(ADDRESS_CAN_ID, id)) {
		// info m: der fehler schent ignoriert werden zu können
		//printf("Error in write eeprrom");
	}
}



uint8_t GetGloablCanIdFromEeeprom(void) {
	uint32_t xxx = 0;
	if (!EE_Read(ADDRESS_CAN_ID, &xxx)) {
		printf("Error in read eeprrom");
	}

	return xxx;
}

void SafeUsedActiveSensorType(uint8_t channelModiType) {
	if (!EE_Write(ADDRESS_ACTIVE_PULSE_SENSOR, (uint8_t)channelModiType)) {
		// info m: der fehler schent ignoriert werden zu können
	}
}

uint8_t GetUsedActiveSensorType(void) {
	uint32_t xxx = 0;
		if (!EE_Read(ADDRESS_ACTIVE_PULSE_SENSOR, &xxx)) {
			printf("Error in read eeprrom");
		}

		return (uint8_t)xxx;
}

