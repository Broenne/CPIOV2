/*
 * helper.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "helper.h"

extern UART_HandleTypeDef huart1;


#define MAX_STRING_SIZE (( unsigned short ) 200 )

#define CAN_TEXT_TABELL_ROWS (( unsigned short ) 8 )
static int pointerToTextTabelleForCan;
static char TextStorageForCan[CAN_TEXT_TABELL_ROWS][MAX_STRING_SIZE]; // 8*200


void GetIfNewTextAvailable(uint8_t* data){
	static uint8_t oldValue;

	static uint8_t pos = 0;
	for(int i = oldValue; i < pointerToTextTabelleForCan; ++i){
		pos = 1 << i;
		*data ^= pos;
	}

	oldValue = pointerToTextTabelleForCan;

	// welche Position hat der Pointer?
	// unterschied zu vorher?
	// dann die bits setzen und info geben
}


void GetTextDataForRow(uint8_t pos, uint8_t posInRow, uint8_t* data){
	memcpy(data, &TextStorageForCan[pos][posInRow],  5);
}

void StoreForCan(char* resString, uint size){
	memcpy(TextStorageForCan[pointerToTextTabelleForCan], resString, size);
	++pointerToTextTabelleForCan;

	if(pointerToTextTabelleForCan > (CAN_TEXT_TABELL_ROWS - 1)){
		pointerToTextTabelleForCan = 0;
	}
}


/*
 *  Created on: 29.11.2018
 *      Author: MB
 *      Funktion zum schreiben auf die UART console.
 *      Printf überschreiben führt zu Problemne, daher diese Funktion nutzen
 */
void myPrintf(char* resString){

	// länger ermitteln, maximal 200 zeichen. string endet mit /n
	char* remberCharAddress = resString;
	int size=0;
	for(size=0; size < MAX_STRING_SIZE; ++size){
		if((*resString) == '\n'){
			break;
		}

		++(resString);
	}

	resString = remberCharAddress;

	StoreForCan(resString, size);
	HAL_UART_Transmit(&huart1, (uint8_t*) resString, size, 100);
}

volatile static int printMode = 0;
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
		ITM_SendChar((*ptr++));
		//__io_putchar((*ptr++));
		return len;
	}

	return 0;
}

void Reset(void) {
	myPrintf("Reboot...\r\n");

	vTaskDelay(100);
	NVIC_SystemReset();
	while (1) {
		;   //wait for reset
	}
}

