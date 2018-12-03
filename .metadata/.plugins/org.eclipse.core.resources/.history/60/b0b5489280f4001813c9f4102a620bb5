/* Includes */
#include "main.h"
//#include "stm32f1xx_syscfg.h"

/* Private macro */
/* Private variables */
USART_InitTypeDef USART_InitStructure;

/* Private function prototypes */
/* Private functions */

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

	UART_Init();

	InitVirtualEeprom();
	GetGloablCanIdFromEeeprom(); // todo mb: das zusammen fassen

	InitSysTicker();
	PrepareCan();
	InitAlive();
	InitPulse();

 	while (1) {
	}
}



