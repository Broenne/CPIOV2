/*
 * IO.c
 *
 *  Created on: 05.12.2018
 *      Author: MB
 */

#include "IO.h"

extern ADC_HandleTypeDef hadc1;
static uint32_t adcbuffer[16];

/*
 * Created on: 30.11.18
 * Author: MB
 Initialisierung der Eing�nge auf dem borad.
 Siehe Schaltplan*/
void InitReadIO(void) {
	HAL_ADC_Start_DMA(&hadc1, adcbuffer, 16);
}

uint8_t CalculateAnalogToHighOrLow(uint32_t value) {
	if (value > 3000) {
		return 1;
	} else {
		return 0;
	}
}


// todo mb: eing�nge richtig sortieren
uint8_t ReadInputsFromRegisterA(void) {
	uint8_t res = 0;
	int anaDigits;
	for (int i = 0; i < 8; ++i) {
		anaDigits = CalculateAnalogToHighOrLow(adcbuffer[i]);
		res = res | (anaDigits << i);
	}

	return res;

}

uint8_t ReadInputsFromRegisterB(void) {

	uint8_t b0 = CalculateAnalogToHighOrLow(adcbuffer[8]);
	uint8_t b1 = CalculateAnalogToHighOrLow(adcbuffer[9]);
	//printf("value analog b2 %d \r\n", b1);
	return ((b1 << 1) & b0);
	//return 0;
	//return (uint8_t)GPIO_ReadInputData(GPIOB) & 0x03; // Pb0 = 10, pb1 = 11
}

uint8_t ReadInputsFromRegisterC(void) {
	uint8_t res = 0;
	int anaDigits;
	for (int i = 0; i < 6; ++i) {
		anaDigits = CalculateAnalogToHighOrLow(adcbuffer[i + 10]);
		res = res | (anaDigits << i);
	}

	return res;
	//return (uint8_t)GPIO_ReadInputData(GPIOC) & 0x3F;
}