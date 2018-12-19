/*
 * helper.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "helper.h"

extern UART_HandleTypeDef huart1;

void myPrintf(char* resString){

	// länger ermitteln, maximal 200 zeichen. string endet mit /n
	char* remberCharAddress = resString;
	int size=0;
	for(size=0;size<200;++size){
		if((*resString) == '\n'){
			break;
		}

		++(resString);
	}

	resString = remberCharAddress;

	HAL_UART_Transmit(&huart1, (uint8_t*) resString, size, 100);
}

volatile static int printMode = 1; // todo mb: während der entwicklung an
/*
 * 30.11.2017
 * MB
 * 0 = deactivate debug
 * 1 = debug on uart
 *
 */
void ActivateDebug(uint activate) {
	printMode = activate;
}

int GetDebugStatusInfo() {
	return printMode;
}


int _write(int file, char *ptr, int len) {
	if (GetDebugStatusInfo()) {
		int i = 0;
		for (i = 0; i < len; i++)
		//	ITM_SendChar((*ptr++));
		__io_putchar((*ptr++));
		return len;
	}

	return 0;
}

void Reset(void) {
	printf("Reboot...\n");

	vTaskDelay(100);
	NVIC_SystemReset();
	while (1) {
		;   //wait for reset
	}
}

