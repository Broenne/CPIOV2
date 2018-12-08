/*
 * storages.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
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
	//portDISABLE_INTERRUPTS();
	__disable_irq();
	FLASH_Unlock();
	if (!EE_WriteVariable(VirtAddVarTab[0], id)) {
		SetCouldNotSafeGlobalCanIdError();
	} else {
		printf("Safe global can id to %d \n", id);
	}

	FLASH_Lock();
	__enable_irq();
	//portENABLE_INTERRUPTS();
}

uint8_t GetGloablCanIdFromEeeprom(void) {
	uint16_t id = 0;

	portDISABLE_INTERRUPTS();
	FLASH_Unlock();
	if (EE_ReadVariable(VirtAddVarTab[0], &id)) {
		SetCouldNotReadGlobalCanIdError();
	} else {
		printf("Read can id %d from eeprom \n", id);
		SetGlobalCanNodeId(id);
	}

	FLASH_Lock();
	portENABLE_INTERRUPTS();
	return ((uint8_t) id);
}
