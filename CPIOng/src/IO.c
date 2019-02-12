/*
 * IO.c
 *
 *  Created on: 05.12.2018
 *      Author: MB
 */

#include "IO.h"

extern ADC_HandleTypeDef hadc1;
static uint16_t adcbuffer[16];

#define DIGIT_LIMIT_FOR_HIGH_SIGNAL ( ( unsigned short ) 3000 )

/*
 * Created on: 30.11.18
 * Author: MB
 Initialisierung der Eing�nge auf dem borad.
 Siehe Schaltplan*/
void InitReadIO(void) {
	HAL_ADC_Start_DMA(&hadc1, &adcbuffer[0], 16);
}

unsigned short GetAnalogBarrier(void) {
	return DIGIT_LIMIT_FOR_HIGH_SIGNAL;
}

/*
 * Created on: 12.02.19
 * Author: MB
 * Service um zwichen high und low Signal der analogen Eing�nge zu unterscheiden.
 */
uint8_t CalculateAnalogToHighOrLow(uint16_t value) {
	if (value > GetAnalogBarrier()) {
		return 1;
	} else {
		return 0;
	}
}

int ReadChannelAnalog(uint pos) {
	return adcbuffer[pos];
}


static uint8_t dataHelper[CHANNEL_COUNT / 8] = { 0 }; // static um es nur einmal anzulegen?

void ReadInputs(uint8_t* data) {
	uint16_t inputs[CHANNEL_COUNT];
	dataHelper[0] = 0;
	dataHelper[1] = 0;

	memcpy(&inputs, &adcbuffer[0], CHANNEL_COUNT * sizeof(uint16_t));

	int anaDigits;
	for (int i = 0; i < CHANNEL_COUNT; ++i) {
		anaDigits = CalculateAnalogToHighOrLow(inputs[i]);
		dataHelper[i / 8] = dataHelper[i / 8] | (anaDigits << (i % 8));
	}

	memcpy(data, dataHelper, sizeof(dataHelper));
}
