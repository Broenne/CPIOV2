/* Includes */
#include <stddef.h>
#include "stm32f10x.h"
#include "can.h"
#include "eeprom.h"
//#include "stm32f1xx_syscfg.h"

/* Private macro */
/* Private variables */
USART_InitTypeDef USART_InitStructure;

/* Private function prototypes */
/* Private functions */

void delay_us(unsigned int d);
void delay_ms(unsigned int d);


void Init_Timer(void) {
	TIM_TimeBaseInitTypeDef TIM_TimeBase_InitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM2, ENABLE); // Timer 2 Interrupt enable
	TIM_TimeBase_InitStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBase_InitStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBase_InitStructure.TIM_Period = 1946; //1999;
	TIM_TimeBase_InitStructure.TIM_Prescaler = 36; // prescal auf 72 MHz bezogen -> 72Mhz/36 = 2 Mhz  -> 2Mhz = 0,5 us
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

int _write(int file, char *ptr, int len) {
	/* Implement your write code here, this is used by puts and printf for example */
	int i = 0;
	for (i = 0; i < len; i++)
		ITM_SendChar((*ptr++));
	return len;
}

void PrepareCan(void) {
	CAN2_init_GPIO(); // das intern machen
	init_CAN2();
}


/*Initialisierung der Eing�nge auf dem borad*/
void InitInputs(void) {

	GPIO_InitTypeDef GPIO_InitStructure;

	// http://stefanfrings.de/stm32/index.html
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);

	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2	| GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5 | GPIO_Pin_6 | GPIO_Pin_7);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;
	GPIO_Init(GPIOA, &GPIO_InitStructure);

	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE);
	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;
	GPIO_Init(GPIOB, &GPIO_InitStructure);

	RCC_APB2PeriphClockCmd( RCC_APB2Periph_GPIOC, ENABLE);
	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2	| GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;
	GPIO_Init(GPIOC, &GPIO_InitStructure);
}

void InitSysTicker(void) {
	// create 1ms tick
	uint32_t init = SysTick_Config(SystemCoreClock/*72000000*// 1000);
	if (init != 0) {
		printf("Error in init system tick"); // noch erro per can?! allgemeine status bits?
	}
}





/**
 **===========================================================================
 **
 **  Abstract: main program
 **
 **===========================================================================
 */
int main(void) {

	// http://www.diller-technologies.de/stm32_wide.html#takt
	SystemInit();

	InitVirtualEeprom();
	GetGloablCanIdFromEeeprom();

	InitSysTicker();

	PrepareStatusLed();
	PrepareCan();

	Init_Timer(); // interrupted
	InitInputs();

	while (1) {
	}
}

#define COUNTS_PER_MICROSECOND 12 //f�r die 12 MHz STM32F1
inline void delay_us(unsigned int d) {
	unsigned int count = d * COUNTS_PER_MICROSECOND - 2;
	__asm volatile(" mov r0, %[count]  \n\t"
			"1: subs r0, #1            \n\t"
			"   bhi 1b                 \n\t"
			:
			: [count] "r" (count)
			: "r0");

}

//--------------------------------------------
// for 168 MHz @ Optimization-Level -OS
//--------------------------------------------
void delay_ms(unsigned int d) {
	while (d--)
		delay_us(999);
}

