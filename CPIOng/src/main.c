/* Includes */
#include <stddef.h>
#include "stm32f10x.h"
#include "can.h"
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

//
//void InitExtiLine(uint32_t value) {
//	EXTI_InitTypeDef EXTI_InitStructure;
//	EXTI_InitStructure.EXTI_Line = value; //EXTI_Line0;
//	EXTI_InitStructure.EXTI_Mode = EXTI_Mode_Interrupt;
//	EXTI_InitStructure.EXTI_Trigger = EXTI_Trigger_Rising;
//	EXTI_InitStructure.EXTI_LineCmd = ENABLE;
//	EXTI_Init(&EXTI_InitStructure);
//}
//
//void InitNvicExti(uint8_t value) {
//	NVIC_InitTypeDef NVIC_InitStructure;
//	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_4);
//	NVIC_InitStructure.NVIC_IRQChannel = value; //EXTI0_IRQn;
//	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
//	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x0F;
//	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x0F;
//	NVIC_Init(&NVIC_InitStructure);
//}
//
//
void InitInputs(void) {

	GPIO_InitTypeDef GPIO_InitStructure;


	// http://stefanfrings.de/stm32/index.html

	// Setzen der Pins auf deefinierte Konfiguration

	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);

	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2	| GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5 | GPIO_Pin_6 | GPIO_Pin_7);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;//GPIO_Mode_IN_FLOATING;
	GPIO_Init(GPIOA, &GPIO_InitStructure);
//	GPIO_EXTILineConfig(GPIO_PortSourceGPIOA,
//			(GPIO_PinSource0 | GPIO_PinSource1 | GPIO_PinSource2
//					| GPIO_PinSource3 | GPIO_PinSource4 | GPIO_PinSource5
//					| GPIO_PinSource6 | GPIO_PinSource7));




//	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE);
//	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1);
//	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
//	GPIO_Init(GPIOB, &GPIO_InitStructure);
//	GPIO_EXTILineConfig(GPIO_PortSourceGPIOB, (GPIO_PinSource0 ));
//	//GPIO_EXTILineConfig(GPIO_PortSourceGPIOB, (GPIO_PinSource1 ));



//	RCC_APB2PeriphClockCmd( RCC_APB2Periph_GPIOC, ENABLE);
//	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2
////	| GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5);
////	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IN_FLOATING;
//	GPIO_Init(GPIOC, &GPIO_InitStructure);
//	GPIO_EXTILineConfig(GPIO_PortSourceGPIOC, (GPIO_PinSource0 ));


//	SYSCFG_EXTILineConfig(EXTI_PortSourceGPIOA, EXTI_PinSource0);
//	SYSCFG_EXTILineConfig(EXTI_PortSourceGPIOB, EXTI_PinSource0);
//	SYSCFG_EXTILineConfig(EXTI_PortSourceGPIOC, EXTI_PinSource0);

//
//	InitExtiLine(EXTI_Line0);
//	InitNvicExti(EXTI0_IRQn);
//
//	InitExtiLine(EXTI_Line1);
//	InitNvicExti(EXTI1_IRQn);
//
//	InitExtiLine(EXTI_Line2);
//	InitNvicExti(EXTI2_IRQn);
//
//	InitExtiLine(EXTI_Line3);
//	InitNvicExti(EXTI3_IRQn);
//
//	InitExtiLine(EXTI_Line4);
//	InitNvicExti(EXTI4_IRQn);

	//InitExtiLine(EXTI_Line5);
	//InitNvicExti(EXTI9_5_IRQn);
//
//	InitExtiLine(EXTI_Line6);
//	InitNvicExti(EXTI6_IRQn);
//
//	InitExtiLine(EXTI_Line7);
//	InitNvicExti(EXTI7_IRQn);
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

	InitSysTicker();

	PrepareStatusLed();
	PrepareCan();

	Init_Timer(); // interrupted

	InitInputs();

	while (1) {
	}
}

#define COUNTS_PER_MICROSECOND 12 //für die 12 MHz STM32F1
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

