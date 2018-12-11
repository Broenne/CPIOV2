/*
 * myCanFunctions.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "myCanFunctions.h"

extern fn_ptr;

static uint8_t globalCanId = 2;

#define QUEUE_SIZE_FOR_CAN		( ( unsigned short ) 1 )

//extern CAN_HandleTypeDef hcan2;
osThreadId canInputTaskHandle;

xQueueHandle CanQueueHandle = NULL;
xQueueHandle CanQueueSenderHandle = NULL;

/*
 * Created on: 30.11.18
 * Author: MB
 * Diese Funktion arbeitet die queue ab.
 * Es besteht keine Priorität und es ist darauf zu achten, das de Prozessor ausreichend Zeit hat, die Information los zu werden.
 * */
void CanWorkerTask(void * pvParameters) {

	while (1) {
		CAN_HandleTypeDef can;
		CAN_HandleTypeDef* hcan = &can;
		static CanTxMsgTypeDef TxMessage;
		static CanRxMsgTypeDef RxMessage;
		hcan->pTxMsg = &TxMessage;
		hcan->pRxMsg = &RxMessage;

		// mepfänger
		if (xQueueReceive(CanQueueHandle, hcan, 100) == pdTRUE) {
			if (hcan->Instance == CAN2) {

				// default id 0
				if (0x00 == hcan->pRxMsg->StdId) {
					switch (hcan->pRxMsg->Data[0]) {
					case 0x01:
						SetGlobalCanNodeId(hcan->pRxMsg->Data[1]);
						printf("Incoming id 0x00 %d", GetGlobalCanNodeId());
						break;
					case 0x02:
						ActivateDebug(hcan->pRxMsg->Data[1]);
					default:
						break;
					}
				}
				// eigene can id
				if (GetGlobalCanNodeId() == hcan->pRxMsg->StdId) {
					if (0x02 == hcan->pRxMsg->RTR) {
						uint8_t data[8];
						GetInputs(&data);
						SendCan(GetGlobalCanNodeId(), data, 8); // ab in ide ander queu un einfach weg
					}

					if (SET_TIMER_CALIBRATION_CMD == hcan->pRxMsg->Data[0]) {
						SetTimerPulseCorrecturFactor(0); // todo mb: datnfed in int 16 wandeln
					}
				}
			}
		}

		// zum senden
		if (xQueueReceive(CanQueueSenderHandle, hcan, 100) == pdTRUE) {
			SendCan(hcan->pTxMsg->StdId, hcan->pTxMsg->Data, 8);
		}
	}

	printf("Sender task error \r\n");
}

/*
 * Created on: 30.11.18
 * Author: MB
 * Diese Funktion initialisiert die Queue
 * Diese und startet den task um diese abzuarbeiten.
 * */
void InitCanInputTask(void) {

	CanQueueHandle = xQueueCreate(QUEUE_SIZE_FOR_CAN, sizeof(CAN_HandleTypeDef));
	CanQueueSenderHandle = xQueueCreate(1, sizeof(CAN_HandleTypeDef));

	osThreadDef(canInputTask, CanWorkerTask, osPriorityNormal, 0, 256);
	canInputTaskHandle = osThreadCreate(osThread(canInputTask), NULL);
}

void PrepareCan(void) {
	globalCanId = GetGloablCanIdFromEeeprom(); // set Methode kan wegen reset nicht genutzt weden
	InitCanInputTask();
}

uint8_t GetGlobalCanNodeId() {
	return globalCanId;
}

void SetGlobalCanNodeId(uint8_t canId) {
	// todo mb: einschränken
	SafeGlobalCanId(canId);
	Reset();
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

void GetInputs(uint8_t* data) {
	uint8_t val[2];
	ReadInputs(&val[0]);
	memcpy(data, &val, sizeof(val));
}

