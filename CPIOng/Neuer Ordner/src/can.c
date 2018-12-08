/*
 * Can.c
 *
 *  Created on: 22.11.2018
 *      Author: MB
 */

#include "can.h"

void CAN2_init_GPIO(void) {
	GPIO_InitTypeDef GPIO_InitStructure;
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE); // GPIO B Takt

	// CAN RX
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_5;           // PB5=CANRX
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;        // Pin Mode
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;    // Pin Taktung
	GPIO_Init(GPIOB, &GPIO_InitStructure);

	// CAN TX
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6;             // PB6=CANTX
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;       // Pin Mode
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;     // Pin Taktung
	GPIO_Init(GPIOB, &GPIO_InitStructure);

	// CAN2 Periph clock enable
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_CAN1, ENABLE); // CAN1 Takt freigeben sonst geht can 2 nicht
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_CAN2, ENABLE);  // CAN2 Takt freigeben
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_AFIO, ENABLE); // AFIO Takt freigeben (f�r Remapping)

	// Remapping CANRX und CANTX
	GPIO_PinRemapConfig(GPIO_Remap_CAN2, ENABLE);

}

void init_CAN2(void) {
	CAN_InitTypeDef CAN_InitStructure;
	//CAN_FilterInitTypeDef  CAN_FilterInitStructure;
	// CAN deinit
	CAN_DeInit(CAN2);
	// init CAN
	CAN_InitStructure.CAN_TTCM = DISABLE;
	CAN_InitStructure.CAN_ABOM = ENABLE; //Automatic BUS Off Management
	CAN_InitStructure.CAN_AWUM = DISABLE;
	CAN_InitStructure.CAN_NART = DISABLE; //No Automatic Retransmission
	CAN_InitStructure.CAN_RFLM = DISABLE;
	CAN_InitStructure.CAN_TXFP = DISABLE;
	CAN_InitStructure.CAN_Mode = CAN_Mode_Normal;
	CAN_InitStructure.CAN_SJW = CAN_SJW_1tq;

	// CAN Baudrate
	CAN_InitStructure.CAN_BS1 = CAN_BS1_11tq;
	CAN_InitStructure.CAN_BS2 = CAN_BS2_4tq;
	CAN_InitStructure.CAN_Prescaler = 18;
	CAN_Init(CAN2, &CAN_InitStructure);

	NVIC_InitTypeDef NVIC_InitStructure;
	NVIC_InitStructure.NVIC_IRQChannel = CAN2_RX0_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);

	InitCanFilter();

	// Enable Interrupt
	CAN_ITConfig(CAN2, CAN_IT_FMP0, ENABLE);
}

void FilterOnlyIdNull(void){
	CAN_FilterInitTypeDef CAN_FilterInitStructure;
		// except every
		CAN_FilterInitStructure.CAN_FilterNumber = 14; // 0..13 for CAN1, 14..27 for CAN2
		CAN_FilterInitStructure.CAN_FilterMode = CAN_FilterMode_IdMask;
		CAN_FilterInitStructure.CAN_FilterScale = CAN_FilterScale_16bit;
	//	CAN_FilterInitStructure.CAN_FilterIdHigh = 0x0000;
	//	CAN_FilterInitStructure.CAN_FilterIdLow = 0x0000;
	//	CAN_FilterInitStructure.CAN_FilterMaskIdHigh = 0xFFFF;//0x0000;
	//	CAN_FilterInitStructure.CAN_FilterMaskIdLow = 0x0000;

		// 0x110 - 0x11F
	//	CAN_FilterInitStructure.CAN_FilterIdHigh =  0x0000;
	//	CAN_FilterInitStructure.CAN_FilterIdLow = 0x0110 << 5; //     ID   001 0001 0000
	//	CAN_FilterInitStructure.CAN_FilterMaskIdHigh = 0xFFFF;
	//	CAN_FilterInitStructure.CAN_FilterMaskIdLow = 0x07F0 << 5; // Mask 111 1111 0000
		CAN_FilterInitStructure.CAN_FilterIdHigh =  0x0000;
		CAN_FilterInitStructure.CAN_FilterIdLow = 0x0000 << 5;
		CAN_FilterInitStructure.CAN_FilterMaskIdHigh = 0xFFFF;
		CAN_FilterInitStructure.CAN_FilterMaskIdLow = 0x07FF << 5;

		CAN_FilterInitStructure.CAN_FilterFIFOAssignment = 0;
		CAN_FilterInitStructure.CAN_FilterActivation = ENABLE;
		CAN_FilterInit(&CAN_FilterInitStructure);
}

void FilterOnlyMyId(void){
	CAN_FilterInitTypeDef CAN_FilterInitStructure;
		// except every
		CAN_FilterInitStructure.CAN_FilterNumber = 14; // 0..13 for CAN1, 14..27 for CAN2
		CAN_FilterInitStructure.CAN_FilterMode = CAN_FilterMode_IdMask;
		CAN_FilterInitStructure.CAN_FilterScale = CAN_FilterScale_16bit;
	//	CAN_FilterInitStructure.CAN_FilterIdHigh = 0x0000;
	//	CAN_FilterInitStructure.CAN_FilterIdLow = 0x0000;
	//	CAN_FilterInitStructure.CAN_FilterMaskIdHigh = 0xFFFF;//0x0000;
	//	CAN_FilterInitStructure.CAN_FilterMaskIdLow = 0x0000;

		// 0x110 - 0x11F
	//	CAN_FilterInitStructure.CAN_FilterIdHigh =  0x0000;
	//	CAN_FilterInitStructure.CAN_FilterIdLow = 0x0110 << 5; //     ID   001 0001 0000
	//	CAN_FilterInitStructure.CAN_FilterMaskIdHigh = 0xFFFF;
	//	CAN_FilterInitStructure.CAN_FilterMaskIdLow = 0x07F0 << 5; // Mask 111 1111 0000
		CAN_FilterInitStructure.CAN_FilterIdHigh =  0x0000;
		CAN_FilterInitStructure.CAN_FilterIdLow = GetGloablCanIdFromEeeprom() << 5;
		CAN_FilterInitStructure.CAN_FilterMaskIdHigh = 0xFFFF;
		CAN_FilterInitStructure.CAN_FilterMaskIdLow = 0x07FF << 5;

		CAN_FilterInitStructure.CAN_FilterFIFOAssignment = 0;
		CAN_FilterInitStructure.CAN_FilterActivation = ENABLE;
		CAN_FilterInit(&CAN_FilterInitStructure);
}



void InitCanFilter(void) {
	FilterOnlyIdNull();
	//FilterOnlyMyId();
}

/*
 * Created on: 30.11.18
 * Author: MB
 * Grundfunktion zum senden einer standard can-message im normalen
 * ID-Bereich
 * */
void SendCan(uint32_t id, uint8_t data[], uint8_t len) {
	portDISABLE_INTERRUPTS();
	//printf("Send can \r\n");
	CanTxMsg canMessage;
	canMessage.StdId = id;
	canMessage.ExtId = 0;
	canMessage.RTR = CAN_RTR_DATA;
	canMessage.IDE = CAN_ID_STD;
	canMessage.DLC = len;

	memcpy(canMessage.Data, data, len * sizeof(uint8_t));

	int i=0;
	while (!(CAN2->TSR & CAN_TSR_TME0 || CAN2->TSR & CAN_TSR_TME1
			|| CAN2->TSR & CAN_TSR_TME2)) {
		printf("Send can %d \r\n", i);
		++i;
		if(i > 20){
			SetCanSendError(); // time out after....
		}

		//delay_us(500);
	}


	//printf("Send can end\r\n");
	CAN_Transmit(CAN2, &canMessage);
	portENABLE_INTERRUPTS();
}



void CAN2_RX0_IRQHandler(void) {

	portDISABLE_INTERRUPTS();
	__disable_irq();

	CanRxMsg RxMessage;
	CAN_Receive(CAN2, CAN_FIFO0, &RxMessage);
	printf("receive can 2 interrupt %d \n", (int)RxMessage.StdId);
	// default id 0
	if (0x00 == RxMessage.StdId) {
			switch (RxMessage.Data[0]) {
				case 0x01:
					SetGlobalCanNodeId(RxMessage.Data[1]);
					printf("Incoming id 0x00 %d", GetGlobalCanNodeId());
					break;
				case 0x02:
					ActivateDebug(RxMessage.Data[1]);
				default:
					break;
			}
	}

//	// eigene can id
//	if(GetGloablCanIdFromEeeprom()==RxMessage.StdId){
//			if(0x02 == RxMessage.RTR){
//				// todo mb: �ber que wegschreiben NIEMALS IN ITERRUPT!!!!!
//				uint8_t data[2];
//				GetInputs(data);
//				SendCan(GetGloablCanIdFromEeeprom(), data, 2);
//				//printf("ddd %i", data[1]);
//			}
//	}


	__enable_irq();
	portENABLE_INTERRUPTS();
}