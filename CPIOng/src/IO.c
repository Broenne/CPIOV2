/*
 * IO.c
 *
 *  Created on: 05.12.2018
 *      Author: MB
 */

#include "IO.h"

extern ADC_HandleTypeDef hadc1;
static uint16_t adcbuffer[16];

/*
 * Created on: 30.11.18
 * Author: MB
 Initialisierung der Eing�nge auf dem borad.
 Siehe Schaltplan*/
void InitReadIO(void) {
	HAL_ADC_Start_DMA(&hadc1, &adcbuffer[0], 16);
}

uint8_t CalculateAnalogToHighOrLow(uint16_t value) {
	if (value > 3000) {
		return 1;
	} else {
		return 0;
	}
}

int ReadChannelAnalog(uint pos){
	return adcbuffer[pos];
}

void ReadInputs(uint8_t* data) {
	uint16_t inputs[16];
	uint8_t dataHelper[2] = {0};

	memcpy(&inputs, &adcbuffer[0], 16 * sizeof(uint16_t));

	// erstes byte
	int anaDigits;
	for (int i = 0; i < 8; ++i) {
		anaDigits = CalculateAnalogToHighOrLow(inputs[i]);
		dataHelper[0] = dataHelper[0] | (anaDigits << i);
	}

	for (int i = 8; i < 16; ++i) {
		anaDigits = CalculateAnalogToHighOrLow(inputs[i]);
		dataHelper[1] = dataHelper[1] | (anaDigits << (i - 8));
	}

	memcpy(data, dataHelper, sizeof(dataHelper));
}
