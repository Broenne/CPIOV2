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


void CAN2_init_GPIO(void)
{
  GPIO_InitTypeDef  GPIO_InitStructure;
  RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE); // GPIO B Takt

  // CAN RX
  GPIO_InitStructure.GPIO_Pin   = GPIO_Pin_5;           // PB5=CANRX
  GPIO_InitStructure.GPIO_Mode  = GPIO_Mode_IPU;        // Pin Mode
  GPIO_InitStructure.GPIO_Speed =  GPIO_Speed_50MHz;    // Pin Taktung
  GPIO_Init(GPIOB, &GPIO_InitStructure);

  // CAN TX
  GPIO_InitStructure.GPIO_Pin = GPIO_Pin_6;             // PB6=CANTX
  GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF_PP;       // Pin Mode
  GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;     // Pin Taktung
  GPIO_Init(GPIOB, &GPIO_InitStructure);


    // CAN2 Periph clock enable
  	RCC_APB1PeriphClockCmd(RCC_APB1Periph_CAN1, ENABLE);          // CAN1 Takt freigeben sonst geht can 2 nicht
    RCC_APB1PeriphClockCmd(RCC_APB1Periph_CAN2, ENABLE);          // CAN2 Takt freigeben
    RCC_APB2PeriphClockCmd(RCC_APB2Periph_AFIO, ENABLE);          // AFIO Takt freigeben (f�r Remapping)

    // Remapping CANRX und CANTX
    GPIO_PinRemapConfig(GPIO_Remap_CAN2, ENABLE);
}

void init_CAN2(void)
{
  CAN_InitTypeDef        CAN_InitStructure;
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
  CAN_InitStructure.CAN_NART = DISABLE;//No Automatic Retransmission
  CAN_InitStructure.CAN_RFLM = DISABLE;
  CAN_InitStructure.CAN_TXFP = DISABLE;
  CAN_InitStructure.CAN_Mode = CAN_Mode_Normal;
  CAN_InitStructure.CAN_SJW = CAN_SJW_1tq;

  // CAN Baudrate
  CAN_InitStructure.CAN_BS1 = CAN_BS1_11tq;
  CAN_InitStructure.CAN_BS2 = CAN_BS2_4tq;
  CAN_InitStructure.CAN_Prescaler = 18;
  CAN_Init(CAN2, &CAN_InitStructure);
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

	GPIO_InitTypeDef GPIO_InitStructure;
	TIM_TimeBaseInitTypeDef TIM_TimeBase_InitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;

	CAN_InitTypeDef CAN_InitStructure;

	SystemInit();


	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOD, ENABLE);
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM2, ENABLE); // Timer 2 Interrupt enable

	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOD, &GPIO_InitStructure);

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





	CAN2_init_GPIO();
	init_CAN2();


	CanTxMsg canMessage;

	canMessage.StdId = 0x123;
	canMessage.ExtId = 0;
	canMessage.RTR = CAN_RTR_DATA;
	canMessage.IDE = CAN_ID_STD;
	canMessage.DLC = 1;


//	canMessage.Data[1] = 1;
//	canMessage.Data[2] = 2;
//	canMessage.Data[3] = 3;
//	canMessage.Data[4] = 4;
//	canMessage.Data[5] = 5;
//	canMessage.Data[6] = 6;
//	canMessage.Data[7] = 7;

	int i=0;
	while (1) {
		++i;
		canMessage.Data[0] = i;
		while(!(CAN1->TSR & CAN_TSR_TME0 || CAN1->TSR & CAN_TSR_TME1 || CAN1->TSR & CAN_TSR_TME2)){} // todo mb: das ding austimen lassen
		CAN_Transmit(CAN2, &canMessage);

		delay_ms(500);
		// printf("hello");
		/*
		 delay_ms(500);
		 if(GPIO_ReadInputDataBit(GPIOD, GPIO_Pin_2)){
		 GPIO_WriteBit(GPIOD, GPIO_Pin_2, Bit_RESET);
		 }
		 else {
		 GPIO_WriteBit(GPIOD, GPIO_Pin_2, Bit_SET);
		 }
		 */
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

