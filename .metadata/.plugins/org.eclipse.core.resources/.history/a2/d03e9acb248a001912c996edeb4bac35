/*
 * IO.c
 *
 *  Created on: 05.12.2018
 *      Author: MB
 */

#include "IO.h"

extern ADC_HandleTypeDef hadc1;
static uint16_t adcbuffer[CHANNEL_COUNT];

#define DIGIT_LIMIT_FOR_HIGH_SIGNAL ( ( unsigned short ) 4000 )

/*
 * Created on: 30.11.18
 * Author: MB
 Initialisierung der Eingänge auf dem borad.
 Siehe Schaltplan*/
void InitReadIO(void) {
	HAL_ADC_Start_DMA(&hadc1, &adcbuffer[0], CHANNEL_COUNT);
}

unsigned short GetAnalogBarrier(void) {
	return DIGIT_LIMIT_FOR_HIGH_SIGNAL;
}

/*
 * Created on: 12.02.19
 * Author: MB
 * Service um zwichen high und low Signal der analogen Eingänge zu unterscheiden.
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

#define MEAN_VALUE_DEPTH ( ( unsigned short ) 10 )
static int MeanValue[CHANNEL_COUNT][MEAN_VALUE_DEPTH]; // array[y][x]
static int MeanValuePointer[CHANNEL_COUNT];

void ReadInputs(uint8_t* data) {
	uint16_t inputs[CHANNEL_COUNT];
	dataHelper[0] = 0;
	dataHelper[1] = 0;

	memcpy(&inputs, &adcbuffer[0], CHANNEL_COUNT * sizeof(uint16_t));

	int anaDigits;
	for (int i = 0; i < CHANNEL_COUNT; ++i) {

		// Mittelwert berechnen, zumm entprellen (todo mb: ausllagern und wie lange dauert das? wir sind im interrupt)
		//////////////////////////////////////////////////////////////////////////////////
		// pointer erhöhen um zu wissen welcher wert ersetzt werden muuss
		MeanValuePointer[i]++;
		if(MeanValuePointer[i] >= MEAN_VALUE_DEPTH){
			MeanValuePointer[i] = 0;
		}

		// wert an passende Stelle schreiben
		MeanValue[i][MeanValuePointer[i]] = anaDigits;

		// Mittelwert berechne, achtung zahlentyp überlauf?!!!!!!??????? (Werte sin 2 byte)
		uint32_t mittelwertSumme = 0;
		for(int j = 0;j < MEAN_VALUE_DEPTH; ++j){
			mittelwertSumme	+= MeanValue[i][j];
		}

		int meanValue = mittelwertSumme / 10; // evtl besser 8 oder 16 nehmen und dann shiften >>1(:2)   >>2(:4)
		//////////////////////////////////////////////////////////////////////////


		anaDigits = CalculateAnalogToHighOrLow(inputs[i]);
		dataHelper[i / 8] = dataHelper[i / 8] | (anaDigits << (i % 8));
	}

	memcpy(data, dataHelper, sizeof(dataHelper));
}

void GetInputs(uint8_t* data) {
	uint8_t val[2] = { 0, 0};
	ReadInputs(&val[0]);
	memcpy(data, &val, sizeof(val));
}


