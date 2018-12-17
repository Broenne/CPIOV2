/*
 * Can.c
 *
 *  Created on: 22.11.2018
 *      Author: MB
 */

#include "can.h"

extern CAN_HandleTypeDef hcan2;
extern xQueueHandle CanQueueHandle;

/*
 * Created on: 30.11.18
 * Author: MB
 * Grundfunktion zum senden einer standard can-message im normalen
 * ID-Bereich
 * */
void SendCan(uint32_t id, uint8_t data[], uint8_t len) {

	//CanTxMsgTypeDef
	static CanTxMsgTypeDef canMessage;
	canMessage.StdId = id;
	canMessage.ExtId = 0;
	canMessage.RTR = CAN_RTR_DATA;
	canMessage.IDE = CAN_ID_STD;
	canMessage.DLC = len;

	memcpy(canMessage.Data, data, sizeof(data));

	int i = 0;
	while (hcan2.State != HAL_CAN_STATE_READY) {
		++i;
		if (i > 10) {
			SetCanSendError();
			break;
		}

		vTaskDelay(5);
	}

	hcan2.pTxMsg = &canMessage;

	if (HAL_CAN_Transmit(&hcan2, 10) != HAL_OK) {
		SetCanSendError();
	}
}

void HAL_CAN_RxCpltCallback(CAN_HandleTypeDef* hcan) {

	portDISABLE_INTERRUPTS();

	if (xQueueSendFromISR(CanQueueHandle, hcan, 0) != pdTRUE) {
		SetPossiblePulseSendQueueFullError();
	}

	__HAL_CAN_ENABLE_IT(&hcan2, CAN_IT_EWG | CAN_IT_EPV | CAN_IT_BOF | CAN_IT_LEC | CAN_IT_ERR | CAN_IT_TME);
	__HAL_CAN_ENABLE_IT(&hcan2, CAN_IT_FOV0 | CAN_IT_FMP0);

	portENABLE_INTERRUPTS();
}

