/*
 * analog.c
 *
 *  Created on: 12.12.2018
 *      Author: MB
 */

#include "analog.h"


extern UART_HandleTypeDef huart1;

static osThreadId uartTaskHandle;;
uint8_t text;



void ReadUartTask(void){
	char inputData[50];
	int pos = 0;
	//std::string cmd;

	while(1){
			if(HAL_UART_Receive(&huart1, &text, sizeof(text), 0) == HAL_OK){


				if(text == 0x00 ){
					// clear all, NULL-terminierter String


					if(strncmp(resCutted, "AnaCh1", 6)){
						printf("AnaCh1 result \r\n");
					}

					pos = 0;

				}else{
					inputData[pos] = text;
				}

				++pos;

			}
			else{
				// printf("no data \r\n");
			}


		}


}


void InitAnalog(void){
	// gh�rt das uart hier rein? am besten nochmal abstrahieren
	osThreadDef(UartTask, ReadUartTask, osPriorityNormal, 0, 128);
	uartTaskHandle = osThreadCreate(osThread(UartTask), NULL);
}