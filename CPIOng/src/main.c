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

/**
 **===========================================================================
 **
 **  Abstract: main program
 **
 **===========================================================================
 */
int main(void) {
	GPIO_InitTypeDef GPIO_InitStructure;

	SystemInit();

	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOD, ENABLE);

	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_Out_PP;
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_2;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOD, &GPIO_InitStructure);

	while (1) {
		delay_ms(500);
		if(GPIO_ReadInputDataBit(GPIOD, GPIO_Pin_2)){
			GPIO_WriteBit(GPIOD, GPIO_Pin_2, Bit_RESET);
		}
		else {
			GPIO_WriteBit(GPIOD, GPIO_Pin_2, Bit_SET);
		}
	}
}


#define COUNTS_PER_MICROSECOND 12 //f�r die 12 MHz STM32F1
inline void delay_us(unsigned int d)
{
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
void delay_ms(unsigned int d)
{
  while (d--) delay_us(999);
}

