/*
 * myCanFunctions.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "myCanFunctions.h"


extern fn_ptr;

static uint8_t globalCanId = 2;

extern CAN_HandleTypeDef hcan2;
osThreadId canInputTaskHandle;

/*
 * Created on: 30.11.18
 * Author: MB
 * Diese Funktion arbeitet die queue ab.
 * Es besteht keine Priorit�t und es ist darauf zu achten, das de Prozessor ausreichend Zeit hat, die Information los zu werden.
 * */
void CanWorkerTask(void * pvParameters) {
	/* The parameter value is expected to be 1 as 1 is passed in the  pvParameters value in the call to xTaskCreate() below.*/
	//configASSERT(((uint32_t ) pvParameters) == 1);
	//int i = 0;
	while (1) {
		//printf("run %d \r\n", ++i);

		CAN_HandleTypeDef* hcan = &hcan2;

		// todo mb: das ist schlecht, am besten in queue und koioe anlegen
		if (hcan->Instance == CAN2) {
				printf("hello can interrupt 2 id: %d  data1  %d \r\n ", hcan->pRxMsg->StdId, hcan->pRxMsg->Data[0]);

				////	// default id 0
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
				////
				// eigene can id
				if (GetGlobalCanNodeId() == hcan->pRxMsg->StdId) {
					if (0x02 == hcan->pRxMsg->RTR) {
						// todo mb: �ber que wegschreiben NIEMALS IN ITERRUPT!!!!!
						uint8_t data[2];
						GetInputs(data);
						SendCan(GetGlobalCanNodeId(), data, 2);
						//printf("ddd %i", data[1]);
					}

					if (SET_TIMER_CALIBRATION_CMD == hcan->pRxMsg->Data[0]) {
						SetTimerPulseCorrecturFactor(0); // todo mb: datnfed in int 16 wandeln
					}

				}

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

	osThreadDef(canInputTask, CanWorkerTask, osPriorityNormal, 0, 128);
	canInputTaskHandle = osThreadCreate(osThread(canInputTask), NULL);

	// todo mb: wann und wie den task deleten?
}


void PrepareCan(void) {
	globalCanId = GetGloablCanIdFromEeeprom(); // set Methode kan wegen reset nicht genutzt weden
	InitCanInputTask();
}

uint8_t GetGlobalCanNodeId() {
	return globalCanId;
}

void SetGlobalCanNodeId(uint8_t canId) {
	// todo mb: einschr�nken
	SafeGlobalCanId(canId);
	Reset();
	//globalCanId = canId;
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

