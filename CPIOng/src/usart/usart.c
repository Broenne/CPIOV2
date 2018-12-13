/*
 * usart.c
 *
 *  Created on: 08.12.2018
 *      Author: MB
 */


#include "usart.h"


extern UART_HandleTypeDef huart1;

int __io_putchar(int ch) {
	  HAL_StatusTypeDef status = HAL_UART_Transmit(&huart1, (uint8_t*)&ch, 1, 0xFFFF);
	  return ch;
}

