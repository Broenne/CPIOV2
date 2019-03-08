/*
 * analog.c
 *
 *  Created on: 12.12.2018
 *      Author: MB
 */

#include "analog.h"

#define MAX_UART_STRING_LENGTH (( unsigned short ) 50 )
#define ANA_CHANNEL_COMMAND "AnaCh"

extern UART_HandleTypeDef huart1;

static osThreadId uartTaskHandle;

uint8_t text;

/*
 * Created on: 12.02.19
 * Author: MB
 * Service for send analog value on standardt console.
 * */
void SendAnalogValue(char* data, int len) {
	for (int i = (CHANNEL_COUNT - 1); 0 != i; --i) {
		char cmp[sizeof(ANA_CHANNEL_COMMAND) + 2];
		strcpy(cmp, ANA_CHANNEL_COMMAND);
		char str[2];
		sprintf(str, "%d", i);
		strcat(cmp, str);

		char resString[20];
		if (0 == strncmp(data, cmp, len)) {
			int res = ReadChannelAnalog((uint) i);
			sprintf(resString, "Ana%i:%d\r\n", i, res);

			myPrintf(resString);
			break;
		}
	}
}

/*
 * Created on: 12.02.19
 * Author: MB
 * 	Service for endless read UART task.
 * 	if string terminated on \n function call analog value is called
 * */
void ReadUartTask(void) {
	char inputData[MAX_UART_STRING_LENGTH];
	int pos = 0;

	while (1) {
		if (HAL_UART_Receive(&huart1, &text, sizeof(text), 10) == HAL_OK) {
			if (text == '\n') {
				SendAnalogValue(inputData, pos);
				pos = 0;
				continue;

			} else {
				inputData[pos] = text;
			}

			++pos;
		}

		vTaskDelay(100);
	}
}

void InitAnalog(void) {
	osThreadDef(UartTask, ReadUartTask, osPriorityNormal, 0, 128);
	uartTaskHandle = osThreadCreate(osThread(UartTask), NULL);
}
