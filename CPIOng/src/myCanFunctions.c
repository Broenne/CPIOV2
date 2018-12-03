/*
 * myCanFunctions.c
 *
 *  Created on: 29.11.2018
 *      Author: tbe241
 */

#include "myCanFunctions.h"

static uint8_t globalCanId; // todo mb: ab in festen speicher

void PrepareCan(void) {
	CAN2_init_GPIO(); // das intern machen
	init_CAN2();
}

/*void InitGloablCanIDFromEeprom() {
	SafeGlobalCanId();
}*/

uint8_t GetGlobalCanNodeId() {
	return globalCanId;
}

void SetGlobalCanNodeId(uint8_t canId) {
	// todo mb: einschränken
	SafeGlobalCanId(canId);
	globalCanId = canId;
}
