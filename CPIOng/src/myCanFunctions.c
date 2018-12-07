/*
 * myCanFunctions.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "myCanFunctions.h"


extern fn_ptr;

static uint8_t globalCanId = 42;




static void CAN2_init_GPIOInternal(void) {

//	  hcan2.Instance = CAN2;
//	  hcan2.Init.Prescaler = 16;
//	  hcan2.Init.Mode = CAN_MODE_NORMAL;
//	  hcan2.Init.SJW = CAN_SJW_1TQ;
//	  hcan2.Init.BS1 = CAN_BS1_11TQ;
//	  hcan2.Init.BS2 = CAN_BS2_4TQ;
//	  hcan2.Init.TTCM = DISABLE;
//	  hcan2.Init.ABOM = DISABLE;
//	  hcan2.Init.AWUM = DISABLE;
//	  hcan2.Init.NART = DISABLE;
//	  hcan2.Init.RFLM = DISABLE;
//	  hcan2.Init.TXFP = DISABLE;
//	  if (HAL_CAN_Init(&hcan2) != HAL_OK)
//	  {
//	    _Error_Handler(__FILE__, __LINE__);
//	  }

//	// CAN RX
//	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_5;           // PB5=CANRX
//	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;        // Pin Mode
//	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;    // Pin Taktung
//	GPIO_Init(GPIOB, &GPIO_InitStructure);
//
//	// CAN TX
//	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6;             // PB6=CANTX
//	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;       // Pin Mode
//	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;     // Pin Taktung
//	GPIO_Init(GPIOB, &GPIO_InitStructure);
//
//	// CAN2 Periph clock enable
//	RCC_APB1PeriphClockCmd(RCC_APB1Periph_CAN1, ENABLE); // CAN1 Takt freigeben sonst geht can 2 nicht
//	RCC_APB1PeriphClockCmd(RCC_APB1Periph_CAN2, ENABLE);  // CAN2 Takt freigeben
//	RCC_APB2PeriphClockCmd(RCC_APB2Periph_AFIO, ENABLE); // AFIO Takt freigeben (f�r Remapping)
//
//	// Remapping CANRX und CANTX
//	GPIO_PinRemapConfig(GPIO_Remap_CAN2, ENABLE);

}






void PrepareCan(void) {
	//CAN2_init_GPIO();

	CAN2_init_GPIOInternal();

	//init_CAN2();
}

uint8_t GetGlobalCanNodeId() {
	return globalCanId;
}

void SetGlobalCanNodeId(uint8_t canId) {
	// todo mb: einschr�nken
	//SafeGlobalCanId(canId);
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
	val [1]= 0; // muss der auf 0 vorher?
	val [1] = ReadInputsFromRegisterB();
	val [1] = (ReadInputsFromRegisterC() << 2 ) | val [1];
	memcpy(data, val,2);
}

