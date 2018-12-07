/*
 * alive.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "alive.h"



static TIM_HandleTypeDef s_TimerInstance;// = { .Instance = TIM2 };

void SwitchMainLed(void) {
	 HAL_GPIO_TogglePin(LED_S_GPIO_Port, LED_S_Pin);

}

void SanCanAlive(void) {
	uint8_t p[] = { 0x01, 0, 0, 0, 0, 0, 0, 0 };

	// add error frames
	//GetApplicationStatus(&p[3]);

	SendCan(AliveCanId, p, 8);
}


static void Init_TimerInternal(){
	 TIM_ClockConfigTypeDef sClockSourceConfig;
		  TIM_SlaveConfigTypeDef sSlaveConfig;
		  TIM_MasterConfigTypeDef sMasterConfig;


	//	  __TIM2_CLK_ENABLE();

		  __HAL_RCC_TIM2_CLK_ENABLE();
		  s_TimerInstance.Instance = TIM2;
		  s_TimerInstance.Init.Prescaler = 720;
		  s_TimerInstance.Init.CounterMode = TIM_COUNTERMODE_UP;
		  s_TimerInstance.Init.Period = 9460;
		  s_TimerInstance.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
		  s_TimerInstance.Init.RepetitionCounter = 0;
		  //s_TimerInstance.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;//TIM_AUTORELOAD_PRELOAD_ENABLE;//
		  if (HAL_TIM_Base_Init(&s_TimerInstance) != HAL_OK)
		  {
		    _Error_Handler(__FILE__, __LINE__);
		  }

		  HAL_TIM_Base_Start(&s_TimerInstance);
		  HAL_NVIC_SetPriority(TIM2_IRQn, 15, 0);
		  HAL_NVIC_EnableIRQ(TIM2_IRQn);

		  __HAL_TIM_ENABLE_IT(&s_TimerInstance, TIM_IT_UPDATE );
}




void Init_Timer(void) {
	Init_TimerInternal();
}

static void PrepareStatusLed(void) {
	__HAL_RCC_GPIOD_CLK_ENABLE();

	/*Configure GPIO pin Output Level */
	  HAL_GPIO_WritePin(LED_S_GPIO_Port, LED_S_Pin, GPIO_PIN_RESET);

	  GPIO_InitTypeDef GPIO_InitStruct;
	/*Configure GPIO pin : LED_S_Pin */
	  GPIO_InitStruct.Pin = LED_S_Pin;
	  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
	  GPIO_InitStruct.Pull = GPIO_NOPULL;
	  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
	  HAL_GPIO_Init(LED_S_GPIO_Port, &GPIO_InitStruct);


}

void InitAlive(void) {
	//PrepareStatusLed();
	Init_Timer();
}

 void TIM2_IRQHandler(void) {

	portDISABLE_INTERRUPTS();

	SwitchMainLed();

	__HAL_TIM_CLEAR_FLAG(&s_TimerInstance, TIM_FLAG_UPDATE);

	SanCanAlive();

	portENABLE_INTERRUPTS();

}
