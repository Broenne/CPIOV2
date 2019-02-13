/*
 * myCanFunctions.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "myCanFunctions.h"

static uint8_t globalCanId = 2;

#define QUEUE_SIZE_FOR_CAN		( ( unsigned short ) 32 )

osThreadId canInputTaskHandle;

xQueueHandle CanRxQueueHandle = NULL;
xQueueHandle CanQueueSenderHandle = NULL;

/*
 * Created on: 04.02.19
 * Author: MB
 * Service for send actual channel modi.
 * */
void SendActualChannelModi(uint8_t* data) {
	data[3] = 0xFF;
	uint32_t canId = GetGlobalCanNodeId() + SEND_INPUT_CONFIG;
	SendCan(canId, data, 8);
}

/*
 * Created on: 04.02.19
 * Author: MB
 * Function build the answer for channel modi response
 * */
void CreateResponseForRequestChannelModi(uint8_t* data) {
	uint8_t inputChannelNumber = data[1];
	data[0] = 0x01; // zeit es kommt die konfiguration der Eingänge
	data[1] = inputChannelNumber; //Eingangsnummer
	data[2] = GetChannelModiByChannel(inputChannelNumber); //0x03;
	SendActualChannelModi(data);
}

/*
 * Created on: 17.12.18
 * Author: MB
 * Set the difffernt modi for the cahnnel from outside by can. *
 * */
void SetChannelModiFromExternal(uint8_t* data) {
	uint8_t channel = data[1];
	uint8_t mode = data[2];
	ChannelModiType channelModiType = (ChannelModiType) mode;
	ChangeChannelModi(channel, channelModiType);

	// als bestätigung wegsenden
	CreateResponseForRequestChannelModi(data);
}

/*
 * Created on: 07.02.19
 * Author: MB
 * function for build answer to inform about active selected sensor modi
 * */
void CreateResponseActiveSensor(uint8_t* data) {
	// in data[0] steht die Kennung 0x02 für die Anfrage
	data[1] = GetActiveChannelModiType();
	SendActualChannelModi(data);
}

void WorkerCanId0(uint8_t* data) {
	uint8_t dataByte0 = data[0];
	switch (dataByte0) {
	case 0x01:
		SetGlobalCanNodeId(data[1]);
		// printf("Incoming id 0x00 %d", GetGlobalCanNodeId());
		break;
	case 0x02:
		ActivateDebug(data[1]);
		break;
	case 0x03:
		SetChannelModiFromExternal(data);
		break;
	case 0x04:
		SaveChannelToEeprom();
		break;
	default:
		break;
	}
}

void ReadAndSendDigitalInputStates() {
	uint8_t data[8];
	GetInputs(data);
	SendCan(GetGlobalCanNodeId(), data, 8); // ab in ide ander queu un einfach weg
}

void StatusOfActualConfiguredInputs(uint8_t* data) {
	//uint8_t* data = &hcan->pRxMsg->Data[0];
	switch (data[0]) {
	case 0x01:
		CreateResponseForRequestChannelModi(data);
		break;
	case 0x02:
		// einstellung, welcher Sensor grade am Knoten aktiv ist
		CreateResponseActiveSensor(data);
	default:
		break;
	}
}

void SetActiveChannel(uint8_t* data){
	ChannelModiType channelModiType = (ChannelModiType)data[0];
	SetActiveChannelModiType(channelModiType);
}

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

		// Empfänger
		if (xQueueReceive(CanRxQueueHandle, hcan, 10) == pdTRUE) {

			if (hcan->Instance == CAN2) {
				uint32_t stdid = hcan->pRxMsg->StdId;
				uint8_t* pData = &hcan->pRxMsg->Data[0];
				// default id 0
				if (0x00 == stdid) {
					WorkerCanId0(pData);
				}

				uint8_t globalCanId = GetGlobalCanNodeId();
				// Funktion zum abfragen der Einganszustände
				if (globalCanId == stdid) {
					if (0x02 == hcan->pRxMsg->RTR) {
						ReadAndSendDigitalInputStates();
					}
				}

				// Funktion zum Abfrage der Einstellung der aktuellen Eingänge
				if ((globalCanId + REQUEST_INPUT_CONFIG) == stdid) {
					StatusOfActualConfiguredInputs(pData);
				}

				// funktion zum setzen des aktuellen channel modi
				if ((globalCanId + SET_ACTIVE_SENSOR) == stdid) {
					SetActiveChannel(pData);
				}

				if ((globalCanId + FLIPFLOP_OPENCAN_OFFSET_RESET) == stdid) {
					ResetFlipFlop(pData);
				}
			}
		}

		// zum senden
		if (xQueueReceive(CanQueueSenderHandle, hcan, 100) == pdTRUE) {
			SendCan(hcan->pTxMsg->StdId, hcan->pTxMsg->Data, 8);
		}
	}

	SetCanWorkerTaskError();
}

/*
 * Created on: 30.11.18
 * Author: MB
 * Diese Funktion initialisiert die Queue
 * Diese und startet den task um diese abzuarbeiten.
 * */
void InitCanInputTask(void) {

	CanRxQueueHandle = xQueueCreate(QUEUE_SIZE_FOR_CAN, sizeof(CAN_HandleTypeDef));

	CanQueueSenderHandle = xQueueCreate(6, sizeof(CAN_HandleTypeDef));

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
	SafeGlobalCanId(canId);
	Reset();
}

/*
 * Created on: 12.02.19
 * Author: MB
 * Service for send the time difference betwenn pulses on can bus.
 * */
void SendCanTimeDif(uint8_t channel, uint32_t res, uint8_t checkSum) {
	uint8_t p[] = { 0, 0, 0, 0, 0, 0, 0, 0 };

// cast timestamp to can info
	p[0] = (res >> 24) & 0xFF;
	p[1] = (res >> 16) & 0xFF;
	p[2] = (res >> 8) & 0xFF;
	p[3] = res & 0xFF;

	p[7] = checkSum;

	uint32_t canId = PULSE_OPENCAN_OFFSET + GetGlobalCanNodeId() + channel;
	SendCan(canId, p, 8);
}

/*
 * Created on: 12.02.19
 * Author: MB
 * Service for send the actual flip flop state via can.
 * Always send the complete process state of flip flops.
 * */
void SendFlipFlopStateViaCan(uint16_t flipFlopState) {
	uint8_t p[] = { 0, 0 };

	p[0] = flipFlopState & 0xFF;
	p[1] = (flipFlopState >> 8) & 0xFF;

	uint32_t canId = FLIPFLOP_OPENCAN_OFFSET + GetGlobalCanNodeId();
	SendCan(canId, p, 2);
}

