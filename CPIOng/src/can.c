/*
 * Can.c
 *
 *  Created on: 22.11.2018
 *      Author: MB
 */

#include "can.h"

extern CAN_HandleTypeDef hcan2;

void CAN2_init_GPIO(void) {
	CAN2_init_GPIOInternal();
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

	//CanTxMsgTypeDef
	CanTxMsgTypeDef canMessage;
	canMessage.StdId = id;
	canMessage.ExtId = 0;
	canMessage.RTR = CAN_RTR_DATA;
	canMessage.IDE = CAN_ID_STD;
	canMessage.DLC = len;

	memcpy(canMessage.Data, data, len * sizeof(uint8_t));

	int i = 0;
	while (hcan2.State != HAL_CAN_STATE_READY) {
		++i;
		if (i > 10) {
			SetCanSendError();
			break;
		}

		vTaskDelay(20);
	}

	hcan2.pTxMsg = &canMessage;

	//printf("Send can end\r\n");
	if (HAL_CAN_Transmit(&hcan2, 10) != HAL_OK) {
		SetCanSendError();
	}
}

/**
 * @brief This function handles CAN2 RX0 interrupt.
 */
//void CAN2_RX0_IRQHandler(void) {
//	portDISABLE_INTERRUPTS();
//
//	/* USER CODE BEGIN CAN2_RX0_IRQn 0 */
//	printf("hello can interrupt 2 \r\n");
//	/* USER CODE END CAN2_RX0_IRQn 0 */
//
//	CanRxMsgTypeDef value;
//	hcan2.pRxMsg = &value;
//
//	HAL_CAN_IRQHandler(&hcan2);
//
//	__HAL_CAN_ENABLE_IT(&hcan2, CAN_IT_FMP0);
//
//	/* USER CODE BEGIN CAN2_RX0_IRQn 1 */
//
//	/* USER CODE END CAN2_RX0_IRQn 1 */
////	  __HAL_UNLOCK(&hcan2);
//	portENABLE_INTERRUPTS();
//}





 void HAL_CAN_RxCpltCallback(CAN_HandleTypeDef* hcan)
{


	 if(hcan->Instance == CAN2){
		printf("hello can interrupt 2  %d \r\n ", hcan->pRxMsg->Data[0]);

	 }

	 __HAL_CAN_ENABLE_IT(&hcan2, CAN_IT_EWG |
	 		                            CAN_IT_EPV |
	 		                            CAN_IT_BOF |
	 		                            CAN_IT_LEC |
	 		                            CAN_IT_ERR |
	 		                            CAN_IT_TME  );

	 __HAL_CAN_ENABLE_IT(&hcan2, CAN_IT_FOV0 | CAN_IT_FMP0);

	 //__HAL_CAN_ENABLE_IT(hcan, CAN_IT_FMP0);

  /* Prevent unused argument(s) compilation warning */
  //UNUSED(hcan);
  /* NOTE : This function Should not be modified, when the callback is needed,
            the HAL_CAN_RxCpltCallback can be implemented in the user file
   */
}





//
//
//void CAN2_RX0_IRQHandler(void) {
//
//	portDISABLE_INTERRUPTS();
//	__disable_irq();
//
////	CanRxMsg RxMessage;
////	CAN_Receive(CAN2, CAN_FIFO0, &RxMessage);
////	printf("receive can 2 interrupt %d \n", (int)RxMessage.StdId);
////	// default id 0
////	if (0x00 == RxMessage.StdId) {
////			switch (RxMessage.Data[0]) {
////				case 0x01:
////					SetGlobalCanNodeId(RxMessage.Data[1]);
////					printf("Incoming id 0x00 %d", GetGlobalCanNodeId());
////					break;
////				case 0x02:
////					ActivateDebug(RxMessage.Data[1]);
////				default:
////					break;
////			}
////	}
////
//////	// eigene can id
//////	if(GetGloablCanIdFromEeeprom()==RxMessage.StdId){
//////			if(0x02 == RxMessage.RTR){
//////				// todo mb: �ber que wegschreiben NIEMALS IN ITERRUPT!!!!!
//////				uint8_t data[2];
//////				GetInputs(data);
//////				SendCan(GetGloablCanIdFromEeeprom(), data, 2);
//////				//printf("ddd %i", data[1]);
//////			}
//////	}
//
//
//	__enable_irq();
//	portENABLE_INTERRUPTS();
//}
