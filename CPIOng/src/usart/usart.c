/*
 * usart.c
 *
 *  Created on: 08.12.2018
 *      Author: MB
 */

#include "usart.h"

extern UART_HandleTypeDef huart1;

int __io_putchar(int ch) {

	while (HAL_UART_STATE_READY != HAL_UART_GetState(&huart1)) {
		// todo mb: timeout
	}

	HAL_UART_Transmit(&huart1, (uint8_t*) &ch, 1, 100);

	return ch;
}

