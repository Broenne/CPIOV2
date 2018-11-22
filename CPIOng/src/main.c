/* Includes */
#include <stddef.h>
#include "stm32f10x.h"

/* Private macro */
/* Private variables */
USART_InitTypeDef USART_InitStructure;

/* Private function prototypes */
/* Private functions */

void delay_us(unsigned int d);
void delay_ms(unsigned int d);

void CAN2_init_GPIO(void) {
	GPIO_InitTypeDef GPIO_InitStructure;
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE); // GPIO B Takt

	// CAN RX
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_5;           // PB5=CANRX
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;        // Pin Mode
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;    // Pin Taktung
	GPIO_Init(GPIOB, &GPIO_InitStructure);

	// CAN TX
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6;             // PB6=CANTX
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;       // Pin Mode
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;     // Pin Taktung
	GPIO_Init(GPIOB, &GPIO_InitStructure);

	// CAN2 Periph clock enable
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_CAN1, ENABLE); // CAN1 Takt freigeben sonst geht can 2 nicht
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_CAN2, ENABLE);  // CAN2 Takt freigeben
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_AFIO, ENABLE); // AFIO Takt freigeben (für Remapping)

	// Remapping CANRX und CANTX
	GPIO_PinRemapConfig(GPIO_Remap_CAN2, ENABLE);


}

void init_CAN2(void) {
	CAN_InitTypeDef CAN_InitStructure;
	//CAN_FilterInitTypeDef  CAN_FilterInitStructure;
	uint8_t n;

	// CAN Clock enable
	//RCC_APB1PeriphClockCmd(RCC_APB1Periph_CAN1, ENABLE);

	// CAN deinit
	CAN_DeInit(CAN2);
	// init CAN
	CAN_InitStructure.CAN_TTCM = DISABLE;
	CAN_InitStructure.CAN_ABOM = ENABLE; //Automatic BUS Off Management
	CAN_InitStructure.CAN_AWUM = DISABLE;
	CAN_InitStructure.CAN_NART = DISABLE; //No Automatic Retransmission
	CAN_InitStructure.CAN_RFLM = DISABLE;
	CAN_InitStructure.CAN_TXFP = DISABLE;
	CAN_InitStructure.CAN_Mode = CAN_Mode_Normal;
	CAN_InitStructure.CAN_SJW = CAN_SJW_1tq;

	// CAN Baudrate
	CAN_InitStructure.CAN_BS1 = CAN_BS1_11tq;
	CAN_InitStructure.CAN_BS2 = CAN_BS2_4tq;
	CAN_InitStructure.CAN_Prescaler = 18;
	CAN_Init(CAN2, &CAN_InitStructure);




	NVIC_InitTypeDef NVIC_InitStructure;
	NVIC_InitStructure.NVIC_IRQChannel = CAN2_RX0_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);

	CAN_FilterInitTypeDef CAN_FilterInitStructure;
	// except every
	CAN_FilterInitStructure.CAN_FilterNumber = 14; // 0..13 for CAN1, 14..27 for CAN2
		CAN_FilterInitStructure.CAN_FilterMode = CAN_FilterMode_IdMask;
		CAN_FilterInitStructure.CAN_FilterScale = CAN_FilterScale_32bit;
		CAN_FilterInitStructure.CAN_FilterIdHigh = 0x0000;
		CAN_FilterInitStructure.CAN_FilterIdLow = 0x0000;
		CAN_FilterInitStructure.CAN_FilterMaskIdHigh = 0x0000;
		CAN_FilterInitStructure.CAN_FilterMaskIdLow = 0x0000;
		CAN_FilterInitStructure.CAN_FilterFIFOAssignment = 0;
		CAN_FilterInitStructure.CAN_FilterActivation = ENABLE;
		CAN_FilterInit(&CAN_FilterInitStructure);
/*

	  CAN_FilterInitStructure.CAN_FilterNumber = 0;
	  CAN_FilterInitStructure.CAN_FilterMode = CAN_FilterMode_IdMask;
	  CAN_FilterInitStructure.CAN_FilterScale = CAN_FilterScale_32bit;//CAN_FilterScale_32bit;
	  CAN_FilterInitStructure.CAN_FilterIdHigh = 0x0123;// << 5;
	  CAN_FilterInitStructure.CAN_FilterIdLow = 0x0000;
	  CAN_FilterInitStructure.CAN_FilterMaskIdHigh = 0xFFFF;
	  CAN_FilterInitStructure.CAN_FilterMaskIdLow = 0xFFFF;
	  CAN_FilterInitStructure.CAN_FilterFIFOAssignment = 0;
	  CAN_FilterInitStructure.CAN_FilterActivation = ENABLE;
	  CAN_FilterInit(&CAN_FilterInitStructure);
*/


		// Enable Interrupt
		CAN_ITConfig(CAN2, CAN_IT_FMP0, ENABLE);

}

void Init_Timer(void) {

	TIM_TimeBaseInitTypeDef TIM_TimeBase_InitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM2, ENABLE); // Timer 2 Interrupt enable
	TIM_TimeBase_InitStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBase_InitStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBase_InitStructure.TIM_Period = 1999;
	TIM_TimeBase_InitStructure.TIM_Prescaler = 17999;
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

/**
 **===========================================================================
 **
 **  Abstract: main program
 **
 **===========================================================================
 */
int main(void) {

	// http://www.diller-technologies.de/stm32_wide.html#takt

	//GPIO_InitTypeDef GPIO_InitStructure;

	//CAN_InitTypeDef CAN_InitStructure;

	SystemInit();
	//PrepareStatusLed();


	CAN2_init_GPIO(); // das intern machen
	init_CAN2();

	//NVIC_EnableIRQ(CAN2_TX_IRQn);



	Init_Timer(); // interrupted


	//CAN_ITConfig(CAN2, CAN_IT_FMP0, ENABLE);

	int i = 0;
	while (1) {
		printf("hello world %d \n", i);
		//++i;
		//delay_ms(500);
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

