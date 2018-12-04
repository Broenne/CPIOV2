/* Includes */

#include <stdint.h>

#include "main.h"

/**
 **===========================================================================
 **
 **  Abstract: main program
 **
 **===========================================================================
 */

int main(void) {

	// http://www.diller-technologies.de/stm32_wide.html#takt
	// https://iotexpert.com/2017/05/25/psoc-freertos-queue-example/

	SystemInit();

	UART_Init();

	InitVirtualEeprom();
	GetGloablCanIdFromEeeprom(); // todo mb: das zusammen fassen
	InitQueue();

	PrepareCan();

	InitPulse();
	Init_TimerForPulsTime();


	InitAlive();
	vTaskStartScheduler(); /* run RTOS */

	SetApplicationEndError();

}

