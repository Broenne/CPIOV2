/*
 * myCanFunctions.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "myCanFunctions.h"

static uint8_t globalCanId;

void PrepareCan(void) {
	CAN2_init_GPIO();
	init_CAN2();
}

uint8_t GetGlobalCanNodeId() {
	return globalCanId;
}

void SetGlobalCanNodeId(uint8_t canId) {
	// todo mb: einschr�nken
	SafeGlobalCanId(canId);
	globalCanId = canId;
}

void SendCanTimeDif(uint8_t channel, uint32_t res) {
	uint8_t p[] = { 0, 0, 0, 0 };

	// cast timestamp to can info
	p[0] = (res >> 24) & 0xFF;
	p[1] = (res >> 16) & 0xFF;
	p[2] = (res >> 8) & 0xFF;
	p[3] = res & 0xFF;

	uint32_t canId = 0x180 + GetGlobalCanNodeId() + channel;
	SendCan(canId, p, 4);
}



void GetInputs(uint8_t* data){
	uint8_t val[2];
	val [0] = ReadInputsFromRegisterA();
	val [1]= 0;
	val [1] = ReadInputsFromRegisterB();
	val [1] = (ReadInputsFromRegisterC() << 2 ) | val [1];
	memcpy(data, val,2);
}
