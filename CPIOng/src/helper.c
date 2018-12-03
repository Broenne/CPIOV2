/*
 * helper.c
 *
 *  Created on: 29.11.2018
 *      Author: tbe241
 */

#include "helper.h"

void delay_us(unsigned int d) {
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

static int printOnUart;
/*
 * 30.11.2017
 * MB
 * 0 = deactivate debug
 * 1 = debug on uart
 *
 */
void ActivateUartDebug(uint activate){
	printOnUart = activate;
}

int GetDebugStatusInfo(){
	return printOnUart;
}


int _write(int file, char *ptr, int len) {
	if(GetDebugStatusInfo()){
		int i = 0;
			for (i = 0; i < len; i++)
				ITM_SendChar((*ptr++));
				//UART_PutChar(*ptr++);
			return len;
	}

}