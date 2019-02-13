/*
 * CanFilter.c
 *
 *  Created on: 12.02.2019
 *      Author: MB
 */

#include "CanFilter.h"

#define FILTER_ID_NULL_BROADCAST		( ( uint ) 0 )
#define FILTER_ID_CONFIGURE_INPUTS		( ( uint ) 16 )
#define FILTER_ID_RESET_FLIPFLOP		( ( uint ) 17 )
#define FILTER_ID_GET_INPUT_CONFIG		( ( uint ) 18 )
#define FILTER_ID_MY_ID					( ( uint ) 19 )
#define FILTER_ID_ACTIVE_SENSOR			( ( uint ) 20 )
#define FILTER_ID_REQUEST_TEXT			( ( uint ) 21 )

extern volatile CAN_HandleTypeDef hcan2;

void InitFilter(volatile CAN_HandleTypeDef* hcan, uint32_t filterId, uint filterNumber) {
	CAN_FilterConfTypeDef sFilterConfig;
	sFilterConfig.FilterNumber = filterNumber;
	sFilterConfig.FilterMode = CAN_FILTERMODE_IDMASK;
	sFilterConfig.FilterScale = CAN_FILTERSCALE_16BIT;
	sFilterConfig.FilterMaskIdHigh = 0xFFFF;
	sFilterConfig.FilterMaskIdLow = 0x07FF << 5;
	sFilterConfig.FilterIdHigh = 0x0000;
	sFilterConfig.FilterIdLow = filterId << 5; // cob id range durch geben -> 512 pulse
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

void FilterIdNull(volatile CAN_HandleTypeDef* hcan) {
	InitFilter(&hcan2, FILTER_ID_NULL_BROADCAST, 21);
}

void FilterCanIdResetFlipFlop(volatile CAN_HandleTypeDef* hcan) {
	InitFilter(&hcan2, GetGlobalCanNodeId() + FLIPFLOP_OPENCAN_OFFSET_RESET, FILTER_ID_RESET_FLIPFLOP);
}

void FilterCanIdGetInputConfig(volatile CAN_HandleTypeDef* hcan) {
	InitFilter(&hcan2, GetGlobalCanNodeId() + REQUEST_INPUT_CONFIG, FILTER_ID_GET_INPUT_CONFIG);
}

void FilterCanIdActiveSensor(volatile CAN_HandleTypeDef* hcan) {
	InitFilter(&hcan2, GetGlobalCanNodeId() + SET_ACTIVE_SENSOR, FILTER_ID_ACTIVE_SENSOR);
}

void FilterCanIdRequestText(volatile CAN_HandleTypeDef* hcan) {
	InitFilter(&hcan2, GetGlobalCanNodeId() + REQUEST_TEXT, FILTER_ID_REQUEST_TEXT);
}

void FilterCanIdConfigureInputs(volatile CAN_HandleTypeDef* hcan) {
	InitFilter(&hcan2, GetGlobalCanNodeId() + SEND_INPUT_CONFIG, FILTER_ID_CONFIGURE_INPUTS);
}

void FilterOnlyMyId(volatile CAN_HandleTypeDef* hcan) {
	InitFilter(&hcan2, GetGlobalCanNodeId(), FILTER_ID_MY_ID);
}

void InitCanFilter(void) {
	FilterIdNull(&hcan2);
	FilterOnlyMyId(&hcan2);
	FilterCanIdActiveSensor(&hcan2);
	FilterCanIdResetFlipFlop(&hcan2);
	FilterCanIdGetInputConfig(&hcan2);
	FilterCanIdConfigureInputs(&hcan2);
	FilterCanIdRequestText(&hcan2);
}
