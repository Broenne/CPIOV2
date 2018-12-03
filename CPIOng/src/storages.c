/*
 * storages.c
 *
 *  Created on: 29.11.2018
 *      Author: tbe241
 */

#include "storages.h"

uint16_t VirtAddVarTab[NumbOfVar] = { 0x0000, 0x0001, 0x0002, 0x003, 0x004,
		0x005, 0x006 };

void InitVirtualEeprom(void) {
	__disable_irq();
	FLASH_Unlock();
	EE_Init();
	FLASH_Lock();
	delay_ms(2);
	__enable_irq();
}

void SafeGlobalCanId(uint8_t id) {
	__disable_irq();
	FLASH_Unlock();
	if (!EE_WriteVariable(VirtAddVarTab[0], id)) {
		printf("Could not write eeprom \n");
	} else {
		printf("Safe global can id to %d \n", id);
	}
	// EEPROM Init
	//EE_Init();
	FLASH_Lock();
	__enable_irq();
	//Reset();
}

uint8_t GetGloablCanIdFromEeeprom() {
	uint16_t id = 0;

	__disable_irq();
	FLASH_Unlock();
	if (EE_ReadVariable(VirtAddVarTab[0], &id)) {
		printf("Variable can id in eeprom not found \n");
	} else {
		printf("Read can id %d from eeprom \n", id);
		SetGlobalCanNodeId(id);
	}

	FLASH_Lock();
	__enable_irq();
	return ((uint8_t) id);
}

