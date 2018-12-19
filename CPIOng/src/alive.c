/*
 * alive.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "alive.h"



#define AliveOffset ((uint32_t)0x200)

static TIM_HandleTypeDef s_TimerInstance;

extern xQueueHandle CanQueueSenderHandle;


void SwitchMainLed(void) {
	HAL_GPIO_TogglePin(LED_S_GPIO_Port, LED_S_Pin);

}

static uint32_t AliveCanId = 0;

void SanCanAlive(void) {
	uint8_t p[] = { 0x01, 0, 0, 0, 0, 0, 0, 0 };

	// add error frames
	// GetApplicationStatus(&p[3]);


	static CAN_HandleTypeDef hcan;
	hcan.Instance=CAN2;
	static CanTxMsgTypeDef TxMessage;
	static CanRxMsgTypeDef RxMessage;
	hcan.pTxMsg = &TxMessage;
	hcan.pRxMsg = &RxMessage;
	hcan.pTxMsg->StdId = AliveCanId;
	memcpy(hcan.pTxMsg->Data, p, sizeof(p));

	// es muss sicher gestellt sein, das der worker tak schon läuft
	if (xQueueSendFromISR(CanQueueSenderHandle, &hcan, 0) != pdTRUE) {
		SetSendAliveError();
	}
}

static void Init_TimerInternal() {
	TIM_ClockConfigTypeDef sClockSourceConfig;
	TIM_SlaveConfigTypeDef sSlaveConfig;
	TIM_MasterConfigTypeDef sMasterConfig;

	__HAL_RCC_TIM2_CLK_ENABLE()	;
	s_TimerInstance.Instance = TIM2;
	s_TimerInstance.Init.Prescaler = 3600;
	s_TimerInstance.Init.CounterMode = TIM_COUNTERMODE_UP;
	s_TimerInstance.Init.Period = 19460;
	s_TimerInstance.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
	s_TimerInstance.Init.RepetitionCounter = 0;
	if (HAL_TIM_Base_Init(&s_TimerInstance) != HAL_OK) {
		//_Error_Handler(__FILE__, __LINE__);
	}

	HAL_TIM_Base_Start(&s_TimerInstance);
	HAL_NVIC_SetPriority(TIM2_IRQn, 15, 0);
	HAL_NVIC_EnableIRQ(TIM2_IRQn);

	__HAL_TIM_ENABLE_IT(&s_TimerInstance, TIM_IT_UPDATE);
}

void Init_Timer(void) {
	Init_TimerInternal();
}

void InitAlive(void) {
	AliveCanId = (uint32_t)GetGlobalCanNodeId() + AliveOffset;
	Init_Timer();
}

void TIM2_IRQHandler(void) {

	portDISABLE_INTERRUPTS();

	SwitchMainLed();

	__HAL_TIM_CLEAR_FLAG(&s_TimerInstance, TIM_FLAG_UPDATE);

	SanCanAlive();

	portENABLE_INTERRUPTS();

}
