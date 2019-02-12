/*
 * CanFilter.c
 *
 *  Created on: 12.02.2019
 *      Author: MB
 */


#include "CanFilter.h"


extern volatile CAN_HandleTypeDef hcan2;

void FilterCanIdResetFlipFlop(CAN_HandleTypeDef* hcan) {
	int globalCanId = GetGlobalCanNodeId();
	CAN_FilterConfTypeDef sFilterConfig;
	sFilterConfig.FilterNumber = 17;
	sFilterConfig.FilterMode = CAN_FILTERMODE_IDMASK;
	sFilterConfig.FilterScale = CAN_FILTERSCALE_16BIT;
	sFilterConfig.FilterMaskIdHigh = 0xFFFF;
	sFilterConfig.FilterMaskIdLow = 0x07FF << 5;
	sFilterConfig.FilterIdHigh = 0x0000;
	sFilterConfig.FilterIdLow = (globalCanId + FLIPFLOP_OPENCAN_OFFSET_RESET) << 5; // cob id range durch geben -> 512 pulse
	sFilterConfig.FilterFIFOAssignment = CAN_FIFO0;
	sFilterConfig.FilterActivation = ENABLE;

	// info mb: der filter muss auf can 1 gesetzt werden, auch wenn nur can 2 genutzt wird (warum auch immer)
	hcan->Instance = CAN1;
	if (HAL_CAN_ConfigFilter(hcan, &sFilterConfig) != HAL_OK) {
		/* Filter configuration Error */
		//Error_Handler();
	}

	hcan->Instance = CAN2;
}

void FilterCanIdGetInputConfig(CAN_HandleTypeDef* hcan) {
	int globalCanId = GetGlobalCanNodeId();
	CAN_FilterConfTypeDef sFilterConfig;
	sFilterConfig.FilterNumber = 18;
	sFilterConfig.FilterMode = CAN_FILTERMODE_IDMASK;
	sFilterConfig.FilterScale = CAN_FILTERSCALE_16BIT;
	sFilterConfig.FilterMaskIdHigh = 0xFFFF;
	sFilterConfig.FilterMaskIdLow = 0x07FF << 5;
	sFilterConfig.FilterIdHigh = 0x0000;
	sFilterConfig.FilterIdLow = (globalCanId + REQUEST_INPUT_CONFIG) << 5;
	sFilterConfig.FilterFIFOAssignment = CAN_FIFO0;
	sFilterConfig.FilterActivation = ENABLE;

	// info mb: der filter muss auf can 1 gesetzt werden, auch wenn nur can 2 genutzt wird (warum auch immer)
	hcan->Instance = CAN1;
	if (HAL_CAN_ConfigFilter(hcan, &sFilterConfig) != HAL_OK) {
		/* Filter configuration Error */
		//Error_Handler();
	}

	hcan->Instance = CAN2;
}

void FilterCanIdActiveSensor(CAN_HandleTypeDef* hcan) {
	int globalCanId = GetGlobalCanNodeId();
	CAN_FilterConfTypeDef sFilterConfig;
	sFilterConfig.FilterNumber = 20;
	sFilterConfig.FilterMode = CAN_FILTERMODE_IDMASK;
	sFilterConfig.FilterScale = CAN_FILTERSCALE_16BIT;
	sFilterConfig.FilterMaskIdHigh = 0xFFFF;
	sFilterConfig.FilterMaskIdLow = 0x07FF << 5;
	sFilterConfig.FilterIdHigh = 0x0000;
	sFilterConfig.FilterIdLow = (globalCanId + SET_ACTIVE_SENSOR) << 5; // cob id range durch geben -> 512 pulse
	sFilterConfig.FilterFIFOAssignment = CAN_FIFO0;
	sFilterConfig.FilterActivation = ENABLE;

	// info mb: der filter muss auf can 1 gesetzt werden, auch wenn nur can 2 genutzt wird (warum auch immer)
	hcan->Instance = CAN1;
	if (HAL_CAN_ConfigFilter(hcan, &sFilterConfig) != HAL_OK) {
		/* Filter configuration Error */
		//Error_Handler();
	}

	hcan->Instance = CAN2;
}

void FilterCanIdConfigureInputs(CAN_HandleTypeDef* hcan) {
	int globalCanId = GetGlobalCanNodeId();
	CAN_FilterConfTypeDef sFilterConfig;
	sFilterConfig.FilterNumber = 16;
	sFilterConfig.FilterMode = CAN_FILTERMODE_IDMASK;
	sFilterConfig.FilterScale = CAN_FILTERSCALE_16BIT;
	sFilterConfig.FilterMaskIdHigh = 0xFFFF;
	sFilterConfig.FilterMaskIdLow = 0x07FF << 5;
	sFilterConfig.FilterIdHigh = 0x0000;
	sFilterConfig.FilterIdLow = (globalCanId + SEND_INPUT_CONFIG) << 5; // cob id range durch geben -> 512 pulse
	sFilterConfig.FilterFIFOAssignment = CAN_FIFO0;
	sFilterConfig.FilterActivation = ENABLE;

	// info mb: der filter muss auf can 1 gesetzt werden, auch wenn nur can 2 genutzt wird (warum auch immer)
	hcan->Instance = CAN1;
	if (HAL_CAN_ConfigFilter(hcan, &sFilterConfig) != HAL_OK) {
		/* Filter configuration Error */
		//Error_Handler();
	}

	hcan->Instance = CAN2;
}

void FilterOnlyMyId(CAN_HandleTypeDef* hcan) {
	int globalCanId = GetGlobalCanNodeId();
	CAN_FilterConfTypeDef sFilterConfig;
	sFilterConfig.FilterNumber = 19;
	sFilterConfig.FilterMode = CAN_FILTERMODE_IDMASK;
	sFilterConfig.FilterScale = CAN_FILTERSCALE_16BIT;
	sFilterConfig.FilterMaskIdHigh = 0xFFFF;
	sFilterConfig.FilterMaskIdLow = 0x07FF << 5;
	sFilterConfig.FilterIdHigh = 0x0000;
	sFilterConfig.FilterIdLow = globalCanId << 5;
	sFilterConfig.FilterFIFOAssignment = CAN_FIFO0;
	sFilterConfig.FilterActivation = ENABLE;

	// info mb: der filter muss auf can 1 gesetzt werden, auch wenn nur can 2 genutzt wird (warum auch immer)
	hcan->Instance = CAN1;
	if (HAL_CAN_ConfigFilter(hcan, &sFilterConfig) != HAL_OK) {
		/* Filter configuration Error */
		//Error_Handler();
	}

	hcan->Instance = CAN2;
}

void InitCanFilter(void) {
	FilterOnlyMyId(&hcan2); // das muss hier expliziet passierne, um den Filter nach dem setzen einer neuen can id und reset diesen zu reintiaisieren
	FilterCanIdActiveSensor(&hcan2);
	FilterCanIdResetFlipFlop(&hcan2);
	FilterCanIdGetInputConfig(&hcan2);
	FilterCanIdConfigureInputs(&hcan2);
}
