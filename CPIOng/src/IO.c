/*
 * IO.c
 *
 *  Created on: 05.12.2018
 *      Author: MB
 */

#include "IO.h"


//uint16_t ADCBuffer[16];// Der Speicherort f�r die ADC-Wandlung. Wird vom DMA Controller beschrieben
//
//void ADC_DMA_Init(void){
//	GPIO_InitTypeDef GPIO_InitStructure;
//	ADC_InitTypeDef ADC_InitStructure;
//	DMA_InitTypeDef DMA_InitStructure;
//	//NVIC_InitTypeDef NVIC_InitStructure;
//
//
//	RCC_AHBPeriphClockCmd(RCC_AHBPeriph_DMA1, ENABLE);
//
//	DMA_InitStructure.DMA_BufferSize = 16;
//	DMA_InitStructure.DMA_DIR = DMA_DIR_PeripheralSRC;
//	DMA_InitStructure.DMA_M2M = DMA_M2M_Disable;
//	DMA_InitStructure.DMA_MemoryBaseAddr = (uint32_t)ADCBuffer;
//	DMA_InitStructure.DMA_MemoryDataSize = DMA_MemoryDataSize_HalfWord;
//	DMA_InitStructure.DMA_MemoryInc = DMA_MemoryInc_Enable;
//	DMA_InitStructure.DMA_Mode = DMA_Mode_Circular;
//	DMA_InitStructure.DMA_PeripheralBaseAddr = (uint32_t)&ADC1->DR;
//	DMA_InitStructure.DMA_PeripheralDataSize = DMA_PeripheralDataSize_HalfWord;
//	DMA_InitStructure.DMA_PeripheralInc = DMA_PeripheralInc_Disable;
//	DMA_InitStructure.DMA_Priority = DMA_Priority_High;
//	DMA_Init(DMA1_Channel1, &DMA_InitStructure);
//
///* Enable DMA Stream Transfer Complete interrupt */
////DMA_ITConfig(DMA1_Channel1, DMA_IT_TC, ENABLE);
//	DMA_Cmd(DMA1_Channel1, ENABLE);
//
////NVIC_InitStructure.NVIC_IRQChannel = DMA1_Channel1_IRQn;
////NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0;
////NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0;
////NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
////NVIC_Init(&NVIC_InitStructure);
//	RCC_ADCCLKConfig(RCC_PCLK2_Div6);
//	//RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA | RCC_APB2Periph_AFIO | RCC_APB2Periph_ADC1, ENABLE);
//	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA |RCC_APB2Periph_GPIOB |RCC_APB2Periph_GPIOC | RCC_APB2Periph_AFIO | RCC_APB2Periph_ADC1, ENABLE);
//	//PORTA
//	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2
//			| GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5 | GPIO_Pin_6 | GPIO_Pin_7);
//	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AIN;
//	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
//	GPIO_Init(GPIOA, &GPIO_InitStructure);
//	//PORTB
//	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1);
//	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AIN;
//	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
//	GPIO_Init(GPIOB, &GPIO_InitStructure);
//	//PORTC
//	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2
//			| GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5);
//	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AIN;
//	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
//	GPIO_Init(GPIOC, &GPIO_InitStructure);
//
//
//	ADC_InitStructure.ADC_ContinuousConvMode = ENABLE;
//	ADC_InitStructure.ADC_DataAlign = ADC_DataAlign_Right;
//	ADC_InitStructure.ADC_ExternalTrigConv = ADC_ExternalTrigConv_None;
//	ADC_InitStructure.ADC_Mode = ADC_Mode_Independent;
//	ADC_InitStructure.ADC_NbrOfChannel = 16;
//	ADC_InitStructure.ADC_ScanConvMode = ENABLE;
//	ADC_Init(ADC1, &ADC_InitStructure);
//
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_0, 1, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_1, 2, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_2, 3, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_3, 4, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_4, 5, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_5, 6, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_6, 7, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_7, 8, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_8, 9, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_9, 10, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_10, 11, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_11, 12, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_12, 13, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_13, 14, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_14, 15, ADC_SampleTime_1Cycles5);
//	ADC_RegularChannelConfig(ADC1, ADC_Channel_15, 16, ADC_SampleTime_1Cycles5);
//
//
//	ADC_Cmd(ADC1, ENABLE);
//	ADC_DMACmd(ADC1, ENABLE);
//
//	ADC_ResetCalibration(ADC1);
//	while(ADC_GetResetCalibrationStatus(ADC1));
//	ADC_StartCalibration(ADC1);
//	while(ADC_GetCalibrationStatus(ADC1));
//
//	ADC_SoftwareStartConvCmd(ADC1, ENABLE);
//}


/*
 * Created on: 30.11.18
 * Author: MB
Initialisierung der Eing�nge auf dem borad.
Siehe Schaltplan*/
void InitInputs(void) {
	GPIO_InitTypeDef GPIO_InitStructure;
//
//	// http://stefanfrings.de/stm32/index.html
	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOA, ENABLE);

	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2
			| GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5 | GPIO_Pin_6 | GPIO_Pin_7);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;
	//GPIO_InitStructure.GPIO_Speed = GPIO_Speed_2MHz;
	GPIO_Init(GPIOA, &GPIO_InitStructure);

	RCC_APB2PeriphClockCmd(RCC_APB2Periph_GPIOB, ENABLE);
	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;

	//GPIO_InitStructure.GPIO_Speed = GPIO_Speed_2MHz;
	GPIO_Init(GPIOB, &GPIO_InitStructure);

	RCC_APB2PeriphClockCmd( RCC_APB2Periph_GPIOC, ENABLE);
	GPIO_InitStructure.GPIO_Pin = (GPIO_Pin_0 | GPIO_Pin_1 | GPIO_Pin_2
			| GPIO_Pin_3 | GPIO_Pin_4 | GPIO_Pin_5);
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_IPU;
	//GPIO_InitStructure.GPIO_Speed = GPIO_Speed_2MHz;
	GPIO_Init(GPIOC, &GPIO_InitStructure);


	//ADC_DMA_Init();
}




uint8_t CalculateAnalogToHighOrLow(uint16_t value){
	if(value > 3000){
		return 1;
	}
	else{
		return 0;
	}
}



uint8_t ReadInputsFromRegisterA(void){
	return 0;
	//return (uint8_t)(GPIO_ReadInputData(GPIOA) & 0xFF); // Inetressant sind nur die untesten 8 bits, siehe Schaltplan
}

uint8_t ReadInputsFromRegisterB(void){

//	uint16_t b1 = ADCBuffer[8];
//	uint16_t b2 = ADCBuffer[9];
//	printf("value analog b2 %d \r\n", b2);
//	uint8_t b0 = CalculateAnalogToHighOrLow(ADCBuffer[8]);
//	uint8_t b1 = CalculateAnalogToHighOrLow(ADCBuffer[9]);
//	return ( (b1 << 1) & b0 );
	//return 0;
	return (uint8_t)GPIO_ReadInputData(GPIOB) & 0x03; // Pb0 = 10, pb1 = 11
}

uint8_t ReadInputsFromRegisterC(void){
//	uint8_t c0 = CalculateAnalogToHighOrLow(ADCBuffer[10]);
//	uint8_t c1 = CalculateAnalogToHighOrLow(ADCBuffer[11]);
//	uint8_t c2 = CalculateAnalogToHighOrLow(ADCBuffer[12]);
//	uint8_t c3 = CalculateAnalogToHighOrLow(ADCBuffer[13]);
//	uint8_t c4 = CalculateAnalogToHighOrLow(ADCBuffer[14]);
//	uint8_t c5 = CalculateAnalogToHighOrLow(ADCBuffer[15]);
	uint8_t res;
//	int anaDigits;
//	for(int i = 0; i < 6; ++i){
//		anaDigits = CalculateAnalogToHighOrLow(ADCBuffer[i + 10]);
//		res = res | (anaDigits << i  );
//	}

	//return res;
	return (uint8_t)GPIO_ReadInputData(GPIOC) & 0x3F;
}
