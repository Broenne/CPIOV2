/*
 * alive.c
 *
 *  Created on: 29.11.2018
 *      Author: tbe241
 */

#include "alive.h"

void SwitchMainLed(void) {
	TIM_ClearITPendingBit(TIM2, TIM_IT_Update); // setz timer zur�ck, achtung dann kann man ihn auch anders nicht mehr benutzen
	if (GPIO_ReadOutputDataBit(GPIOD, GPIO_Pin_2)) {
		GPIO_WriteBit(GPIOD, GPIO_Pin_2, RESET);
	} else {
		GPIO_WriteBit(GPIOD, GPIO_Pin_2, SET);
	}
}

void SanCanAlive(void) {
	uint8_t p[] = { 0x01, 0, 0, 0, 0, 0, 0, 0 };
	SendCan(AliveCanId, p, 8);
}

void Init_Timer(void) {
	TIM_TimeBaseInitTypeDef TIM_TimeBase_InitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM2, ENABLE); // Timer 2 Interrupt enable
	TIM_TimeBase_InitStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBase_InitStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBase_InitStructure.TIM_Period = 19460; //1999;
	TIM_TimeBase_InitStructure.TIM_Prescaler = 720; // prescal auf 72 MHz bezogen -> 72Mhz/36 = 2 Mhz  -> 2Mhz = 0,5 us
	TIM_TimeBaseInit(TIM2, &TIM_TimeBase_InitStructure);

	TIM_ITConfig(TIM2, TIM_IT_Update, ENABLE);

	NVIC_InitStructure.NVIC_IRQChannel = TIM2_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x0F;
	NVIC_Init(&NVIC_InitStructure);

	TIM_Cmd(TIM2, ENABLE);
}

void PrepareStatusLed(void) {
	GPIO_InitTypeDef GPIO_InitStructure;
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOD, ENABLE);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOD, &GPIO_InitStructure);
}

void InitAlive(void) {
	PrepareStatusLed();
	Init_Timer();
}

void TIM2_IRQHandler(void) {
	SwitchMainLed();
	SanCanAlive();
}
