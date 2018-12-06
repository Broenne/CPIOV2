/*
 * IO.c
 *
 *  Created on: 05.12.2018
 *      Author: MB
 */

#include "IO.h"

uint8_t ReadInputsFromRegisterA(void){
	return (uint8_t)(GPIO_ReadInputData(GPIOA) & 0xFF); // Inetressant sind nur die untesten 8 bits, siehe Schaltplan
}

uint8_t ReadInputsFromRegisterB(void){
	return (uint8_t)GPIO_ReadInputData(GPIOB) & 0x03; // Pb0 = 10, pb1 = 11
}

uint8_t ReadInputsFromRegisterC(void){
	return (uint8_t)GPIO_ReadInputData(GPIOC) & 0x3F;
}
