/* Includes */
#include "main.h"
//#include "stm32f1xx_syscfg.h"

/* Private macro */
/* Private variables */
USART_InitTypeDef USART_InitStructure;

/* Private function prototypes */
/* Private functions */




int _write(int file, char *ptr, int len) {
	/* Implement your write code here, this is used by puts and printf for example */
	int i = 0;
	for (i = 0; i < len; i++)
		ITM_SendChar((*ptr++));
	return len;
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
	PrepareCan();
	InitAlive();
	InitInputs();

 	while (1) {
	}
}



