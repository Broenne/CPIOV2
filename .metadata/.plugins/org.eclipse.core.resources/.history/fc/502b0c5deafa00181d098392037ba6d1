/*
 * storages.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "storages.h"

//uint16_t VirtAddVarTab[NumbOfVar] = { 0x0000, 0x0001, 0x0002, 0x003, 0x004,
//		0x005, 0x006 };

void InitVirtualEeprom(void) {
//	//__disable_irq();
//	portDISABLE_INTERRUPTS();
//	FLASH_Unlock();
//	EE_Init();
//	FLASH_Lock();
//	delay_ms(2);
//	portENABLE_INTERRUPTS();
//	//__enable_irq();
}

#define ADDRESS_CAN_ID  ((uint16_t)0x0000)

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

