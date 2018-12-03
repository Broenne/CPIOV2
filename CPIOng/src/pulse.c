/*
 * pulse.c
 *
 *  Created on: 29.11.2018
 *      Author: tbe241
 */

#include "pulse.h"

#define MessageSize 100

typedef struct {
	uint8_t channel;
	uint32_t res;
} MessageForSend;

typedef struct {
	MessageForSend messageForSend;
	int isNotSend;
} SendQueueStructType;

static uint16_t oldGpioA;
static uint16_t oldGpioB;
static uint16_t oldGpioC;

static SendQueueStructType SendQueueCounter[MessageSize];
static uint32_t positionInQueueForFill;

/*Initialisierung der Eing�nge auf dem borad*/
void InitInputs(void) {

	GPIO_InitTypeDef GPIO_InitStructure;

	// http://stefanfrings.de/stm32/index.html
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);

	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2
			| GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5 | GPIO_Pin_6 | GPIO_Pin_7);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;
	GPIO_Init(GPIOA, &GPIO_InitStructure);

	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE);
	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;
	GPIO_Init(GPIOB, &GPIO_InitStructure);

	RCC_APB2PeriphClockCmd( RCC_APB2Periph_GPIOC, ENABLE);
	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2
			| GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;
	GPIO_Init(GPIOC, &GPIO_InitStructure);
}

void InitPulse(void) {
	InitInputs();
}

void SendCanTimeDif(uint8_t channel, uint32_t res) {
	uint8_t p[] = { 0, 0, 0, 0 };

	// timestamp
	p[0] = (res >> 24) & 0xFF;
	p[1] = (res >> 16) & 0xFF;
	p[2] = (res >> 8) & 0xFF;
	p[3] = res & 0xFF;

	uint32_t canId = 0x180 + GetGlobalCanNodeId() + channel;
	SendCan(canId, p, 4);
}

/*
 * 30.11.18
 * MB
 * Initialisiert den timer zum versenden der timer can nachrichten aus der message queue.
 * Genutzt wird timer 3.
 * */
void Init_TimerForSendCan(void) {
	TIM_TimeBaseInitTypeDef TIM_TimeBase_InitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE); // Timer 2 Interrupt enable
	TIM_TimeBase_InitStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBase_InitStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBase_InitStructure.TIM_Period = 4000; //1999;
	TIM_TimeBase_InitStructure.TIM_Prescaler = 36; // prescal auf 72 MHz bezogen -> 72Mhz/36 = 2 Mhz  -> 2Mhz = 0,5 us
	TIM_TimeBaseInit(TIM3, &TIM_TimeBase_InitStructure);

	TIM_ITConfig(TIM3, TIM_IT_Update, ENABLE);

	NVIC_InitStructure.NVIC_IRQChannel = TIM3_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x0F;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x0F;
	NVIC_Init(&NVIC_InitStructure);

	TIM_Cmd(TIM3, ENABLE);
}

/*
 * 30.11.18
 * MB
 * Interrupt handler zum leeren der CAN-message queu f�r die counter werte
 * */
void TIM3_IRQHandler(void) {
	__disable_irq();

	// der letzte eintrag ist bei positionInQueueForFill

	// position steht grade bei 15 _> dann r�ckw�rts laufen bis zum ersten nicht send
	// todo mb: �berschlag beachten
	for (int i = positionInQueueForFill; i >= 0; --i) {
		if (SendQueueCounter[i].isNotSend) {
			// gefunden den man wegschicken muss?
			int channel = SendQueueCounter[i].messageForSend.channel;
			int res = SendQueueCounter[i].messageForSend.res;
			SendCanTimeDif(channel, res);
			printf("Rising edge on .... In %d %d \n", channel, res);
			SendQueueCounter[i].isNotSend = 0;
		}
	}

	TIM_ClearITPendingBit(TIM3, TIM_IT_Update); // setz timer zur�ck, achtung dann kann man ihn auch anders nicht mehr benutzen
	__enable_irq();
}

void SendTimeInfo(uint8_t channel) {
	uint32_t actualTimeValue = tickMs;
	uint32_t res;
	if (actualTimeValue > lastTimeValue[channel]) {
		res = actualTimeValue - lastTimeValue[channel];
	} else {
		res = lastTimeValue[channel] - actualTimeValue;
	}

	MessageForSend messageForSendX; // { channel,  res };
	messageForSendX.channel = channel;
	messageForSendX.res = res;
	SendQueueStructType sendQueueStruct;
	sendQueueStruct.messageForSend = messageForSendX;
	sendQueueStruct.isNotSend = 1;

	// wenn der letzte noch nicht weggesendet ist entsteht ein �berlauf
	if (SendQueueCounter[positionInQueueForFill].isNotSend) {
		SetCounterError();
		// todo mb: dann abbruch oder irgendwas. Infos gehen verloren
	}

	SendQueueCounter[positionInQueueForFill] = sendQueueStruct;

	++positionInQueueForFill; // es wird immer von unten wieder aufgef�llt
	if (positionInQueueForFill > (MessageSize)) {
		positionInQueueForFill = 0;
	}

	lastTimeValue[channel] = actualTimeValue;
}

void CheckInputsRegisterA(void) {
	uint16_t gpioA = (GPIO_ReadInputData(GPIOA) & 0xFF); // Inetressant sind nur die untesten 8 bits, siehe Schaltplan

	if (gpioA != oldGpioA) {
		uint16_t dif = gpioA ^ oldGpioA; // PinA0 -> I0 	// PinA1 -> I1	// PinA2 -> I2	// PinA3 -> I3	// PinA4 -> I4	// PinA5 -> I5	// PinA6 -> I6	// PinA7 -> I7
		for (int i = 0; i < 8; ++i) {
			// �nderung bit und steigende Flanke
			if ((dif >> i) & 0x01 && (gpioA >> i & 0x01)) {
				SendTimeInfo(i); // Achtung, nur bei a
			}
		}
	}

	// Wert merken
	oldGpioA = gpioA;
}

void CheckInputsRegisterB(void) {
	uint16_t gpioB = GPIO_ReadInputData(GPIOB) & 0x03; // Pb0 = 10, pb1 = 11

	if (gpioB != oldGpioB) {
		uint16_t dif = gpioB ^ oldGpioB;
		for (int i = 0; i < 2; ++i) {
			if ((dif >> i) & 0x01 && (gpioB >> i & 0x01)) {
				SendTimeInfo(8 + i);
			}
		}
	}

	oldGpioB = gpioB;
}

void CheckInputsRegisterC(void) {
	uint16_t gpioC = GPIO_ReadInputData(GPIOC) & 0x3F;

	if (gpioC != oldGpioC) {
		uint16_t dif = gpioC ^ oldGpioC;
		for (int i = 0; i < 6; ++i) {
			if ((dif >> i) & 0x01 && (gpioC >> i & 0x01)) {
				SendTimeInfo(10 + i);
			}
		}
	}

	oldGpioC = gpioC;
}

/**
 * @brief  This function handles SysTick Handler.
 * @param  None
 * @retval None
 */
void SysTick_Handler(void) {
	__disable_irq();
	++tickMs;

	CheckInputsRegisterA();
	CheckInputsRegisterB();
	CheckInputsRegisterC();
	__enable_irq();
}
