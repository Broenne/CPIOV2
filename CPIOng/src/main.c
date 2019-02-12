/**
 ******************************************************************************
 * @file           : main.c
 * @brief          : Main program body
 ******************************************************************************
 * This notice applies to any and all portions of this file
 * that are not between comment pairs USER CODE BEGIN and
 * USER CODE END. Other portions of this file, whether
 * inserted by the user or by software development tools
 * are owned by their respective copyright owners.
 *
 * Copyright (c) 2018 STMicroelectronics International N.V.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted, provided that the following conditions are met:
 *
 * 1. Redistribution of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 * 3. Neither the name of STMicroelectronics nor the names of other
 *    contributors to this software may be used to endorse or promote products
 *    derived from this software without specific written permission.
 * 4. This software, including modifications and/or derivative works of this
 *    software, must execute solely and exclusively on microcontroller or
 *    microprocessor devices manufactured by or for STMicroelectronics.
 * 5. Redistribution and use of this software other than as permitted under
 *    this license is void and will automatically terminate your rights under
 *    this license.
 *
 * THIS SOFTWARE IS PROVIDED BY STMICROELECTRONICS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS, IMPLIED OR STATUTORY WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 * PARTICULAR PURPOSE AND NON-INFRINGEMENT OF THIRD PARTY INTELLECTUAL PROPERTY
 * RIGHTS ARE DISCLAIMED TO THE FULLEST EXTENT PERMITTED BY LAW. IN NO EVENT
 * SHALL STMICROELECTRONICS OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA,
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
 * EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 ******************************************************************************
 */
/* Includes ------------------------------------------------------------------*/

#include "include.h"

/* Private variables ---------------------------------------------------------*/
ADC_HandleTypeDef hadc1; // todo mb: funktion zum übergeben schreiben
DMA_HandleTypeDef hdma_adc1;

volatile CAN_HandleTypeDef hcan2;

UART_HandleTypeDef huart1;

WWDG_HandleTypeDef hwwdg;

//osThreadId defaultTaskHandle;
//osThreadId Idle_TaskHandle;
//osThreadId myTask03Handle;
//osMessageQId myQueue01Handle;
//osTimerId myTimer01Handle;

/* USER CODE BEGIN PV */
/* Private variables ---------------------------------------------------------*/

/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_DMA_Init(void);
static void MX_ADC1_Init(void);
static void MX_CAN2_Init(void);
static void MX_USART1_UART_Init(void);
static void MX_WWDG_Init(void);

/**
 * @brief  The application entry point.
 *
 * @retval None
 */
int main(void) {

	/* Reset of all peripherals, Initializes the Flash interface and the Systick. */
	HAL_Init();

	/* Configure the system clock */
	SystemClock_Config();

	/* Initialize all configured peripherals */
	MX_GPIO_Init();
	MX_DMA_Init();
	MX_ADC1_Init();
	MX_CAN2_Init();
	MX_USART1_UART_Init();

	PrepareCan();
	InitCanFilter();

	InitReadIO();
	InitPulse();
	InitAlive();
	InitAnalog();

	osKernelStart();

	while (1) {
	}

}

/**
 * @brief System Clock Configuration
 * @retval None
 */
void SystemClock_Config(void) {

	RCC_OscInitTypeDef RCC_OscInitStruct;
	RCC_ClkInitTypeDef RCC_ClkInitStruct;
	RCC_PeriphCLKInitTypeDef PeriphClkInit;

	/**Initializes the CPU, AHB and APB busses clocks
	 */
	RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
	RCC_OscInitStruct.HSEState = RCC_HSE_ON;
	RCC_OscInitStruct.HSEPredivValue = RCC_HSE_PREDIV_DIV1;
	RCC_OscInitStruct.HSIState = RCC_HSI_ON;
	RCC_OscInitStruct.Prediv1Source = RCC_PREDIV1_SOURCE_HSE;
	RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
	RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
	RCC_OscInitStruct.PLL.PLLMUL = RCC_PLL_MUL9;
	RCC_OscInitStruct.PLL2.PLL2State = RCC_PLL_NONE;
	if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Initializes the CPU, AHB and APB busses clocks
	 */
	RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK | RCC_CLOCKTYPE_SYSCLK | RCC_CLOCKTYPE_PCLK1 | RCC_CLOCKTYPE_PCLK2;
	RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
	RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
	RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
	RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV2;

	if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_2) != HAL_OK) {
		SetPeriperialInitError();
	}

	PeriphClkInit.PeriphClockSelection = RCC_PERIPHCLK_ADC;
	PeriphClkInit.AdcClockSelection = RCC_ADCPCLK2_DIV4;
	if (HAL_RCCEx_PeriphCLKConfig(&PeriphClkInit) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure the Systick interrupt time	 */
	HAL_SYSTICK_Config(HAL_RCC_GetHCLKFreq() / 1000);

	/**Configure the Systick
	 */
	HAL_SYSTICK_CLKSourceConfig(SYSTICK_CLKSOURCE_HCLK);

	/**Configure the Systick interrupt time
	 */
	__HAL_RCC_PLLI2S_ENABLE();

	/* SysTick_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(SysTick_IRQn, 14, 0);
}

/* ADC1 init function */
static void MX_ADC1_Init(void) {

	ADC_ChannelConfTypeDef sConfig;

	/**Common config
	 */
	hadc1.Instance = ADC1;
	hadc1.Init.ScanConvMode = ADC_SCAN_ENABLE;
	hadc1.Init.ContinuousConvMode = ENABLE;
	hadc1.Init.DiscontinuousConvMode = DISABLE;
	hadc1.Init.ExternalTrigConv = ADC_SOFTWARE_START;
	hadc1.Init.DataAlign = ADC_DATAALIGN_RIGHT;
	hadc1.Init.NbrOfConversion = 16;

	if (HAL_ADC_Init(&hadc1) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_0;
	sConfig.Rank = ADC_REGULAR_RANK_1;
	sConfig.SamplingTime = ADC_SAMPLETIME_1CYCLE_5;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_1;
	sConfig.Rank = ADC_REGULAR_RANK_2;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		//_Error_Handler(__FILE__, __LINE__);
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_2;
	sConfig.Rank = ADC_REGULAR_RANK_3;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_3;
	sConfig.Rank = ADC_REGULAR_RANK_4;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_4;
	sConfig.Rank = ADC_REGULAR_RANK_5;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_5;
	sConfig.Rank = ADC_REGULAR_RANK_6;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_6;
	sConfig.Rank = ADC_REGULAR_RANK_7;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_7;
	sConfig.Rank = ADC_REGULAR_RANK_8;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_8;
	sConfig.Rank = ADC_REGULAR_RANK_9;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_9;
	sConfig.Rank = ADC_REGULAR_RANK_10;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_10;
	sConfig.Rank = ADC_REGULAR_RANK_11;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_11;
	sConfig.Rank = ADC_REGULAR_RANK_12;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_12;
	sConfig.Rank = ADC_REGULAR_RANK_13;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_13;
	sConfig.Rank = ADC_REGULAR_RANK_14;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_14;
	sConfig.Rank = ADC_REGULAR_RANK_15;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}

	/**Configure Regular Channel
	 */
	sConfig.Channel = ADC_CHANNEL_15;
	sConfig.Rank = ADC_REGULAR_RANK_16;
	if (HAL_ADC_ConfigChannel(&hadc1, &sConfig) != HAL_OK) {
		SetPeriperialInitError();
	}
}

/* CAN2 init function */
static void MX_CAN2_Init(void) {
	// info m: die static zuweisung des Speicherbereichs für Tx und Rx wurden nicht durch cubemx zugewiesen, daher hier die Ergänzung
	static CanTxMsgTypeDef TxMessage;
	static CanRxMsgTypeDef RxMessage;

	hcan2.pTxMsg = &TxMessage;
	hcan2.pRxMsg = &RxMessage;

	hcan2.Instance = CAN2;

	hcan2.Init.Prescaler = 18;
	hcan2.Init.Mode = CAN_MODE_NORMAL;
	hcan2.Init.SJW = CAN_SJW_1TQ;
	hcan2.Init.BS1 = CAN_BS1_11TQ;
	hcan2.Init.BS2 = CAN_BS2_4TQ;
	hcan2.Init.TTCM = DISABLE;
	hcan2.Init.ABOM = ENABLE;
	hcan2.Init.AWUM = DISABLE;
	hcan2.Init.NART = DISABLE;
	hcan2.Init.RFLM = DISABLE;
	hcan2.Init.TXFP = DISABLE;

	if (HAL_CAN_Init(&hcan2) != HAL_OK) {
		SetPeriperialInitError();
	}

	//FilterIdNull();

	// todo mb: hier muss es noch
	__HAL_CAN_ENABLE_IT(&hcan2, CAN_IT_FMP0);

}

/* USART1 init function */
static void MX_USART1_UART_Init(void) {

	static uint8_t rxBuff[100];
	huart1.pRxBuffPtr = &rxBuff[0];

	huart1.Instance = USART1;
	huart1.Init.BaudRate = 57600;
	huart1.Init.WordLength = UART_WORDLENGTH_8B;
	huart1.Init.StopBits = UART_STOPBITS_1;
	huart1.Init.Parity = UART_PARITY_NONE;
	huart1.Init.Mode = UART_MODE_TX_RX;
	huart1.Init.HwFlowCtl = UART_HWCONTROL_NONE;
	huart1.Init.OverSampling = UART_OVERSAMPLING_16;

	HAL_UART_DeInit(&huart1);

	if (HAL_UART_Init(&huart1) != HAL_OK) {
		SetPeriperialInitError();
	}
}

/* WWDG init function */
static void MX_WWDG_Init(void) {
	hwwdg.Instance = WWDG;
	hwwdg.Init.Prescaler = WWDG_PRESCALER_4;
	hwwdg.Init.Window = 64;
	hwwdg.Init.Counter = 64;
	hwwdg.Init.EWIMode = WWDG_EWI_DISABLE;
	if (HAL_WWDG_Init(&hwwdg) != HAL_OK) {
		SetPeriperialInitError();
	}
}

/** 
 * Enable DMA controller clock
 */
static void MX_DMA_Init(void) {
	/* DMA controller clock enable */
	__HAL_RCC_DMA1_CLK_ENABLE()
	;

	/* DMA interrupt init */
	/* DMA1_Channel1_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(DMA1_Channel1_IRQn, 5, 0);
	HAL_NVIC_EnableIRQ(DMA1_Channel1_IRQn);

}

/** Configure pins as 
 * Analog
 * Input
 * Output
 * EVENT_OUT
 * EXTI
 */
static void MX_GPIO_Init(void) {

	GPIO_InitTypeDef GPIO_InitStruct;

	/* GPIO Ports Clock Enable */
	__HAL_RCC_GPIOC_CLK_ENABLE()
	;
	__HAL_RCC_GPIOD_CLK_ENABLE()
	;
	__HAL_RCC_GPIOA_CLK_ENABLE()
	;
	__HAL_RCC_GPIOB_CLK_ENABLE()
	;

	/*Configure GPIO pin Output Level */
	HAL_GPIO_WritePin(GPIOB,
	OUT10_Pin | OUT11_Pin | OUT12_Pin | OUT13_Pin | OUT14_Pin | OUT15_Pin | OUT7_Pin | OUT8_Pin | OUT9_Pin, GPIO_PIN_RESET);

	/*Configure GPIO pin Output Level */
	HAL_GPIO_WritePin(GPIOC,
	OUT0_Pin | OUT1_Pin | OUT2_Pin | OUT3_Pin | OUT4_Pin | OUT5_Pin | OUT6_Pin, GPIO_PIN_RESET);

	/*Configure GPIO pin Output Level */
	HAL_GPIO_WritePin(LED_S_GPIO_Port, LED_S_Pin, GPIO_PIN_RESET);

	/*Configure GPIO pins : PRINT_1_Pin PRINT_2_Pin PRINT_4_Pin */
	GPIO_InitStruct.Pin = PRINT_1_Pin | PRINT_2_Pin | PRINT_4_Pin;
	GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	HAL_GPIO_Init(GPIOC, &GPIO_InitStruct);

	/*Configure GPIO pins : OUT10_Pin OUT11_Pin OUT12_Pin OUT13_Pin
	 OUT14_Pin OUT15_Pin OUT7_Pin OUT8_Pin
	 OUT9_Pin */
	GPIO_InitStruct.Pin = OUT10_Pin | OUT11_Pin | OUT12_Pin | OUT13_Pin | OUT14_Pin | OUT15_Pin | OUT7_Pin | OUT8_Pin | OUT9_Pin;
	GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
	HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

	/*Configure GPIO pins : OUT0_Pin OUT1_Pin OUT2_Pin OUT3_Pin
	 OUT4_Pin OUT5_Pin OUT6_Pin */
	GPIO_InitStruct.Pin = OUT0_Pin | OUT1_Pin | OUT2_Pin | OUT3_Pin | OUT4_Pin | OUT5_Pin | OUT6_Pin;
	GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
	HAL_GPIO_Init(GPIOC, &GPIO_InitStruct);

	/*Configure GPIO pin : PRINT_3_Pin */
	GPIO_InitStruct.Pin = PRINT_3_Pin;
	GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	HAL_GPIO_Init(PRINT_3_GPIO_Port, &GPIO_InitStruct);

	/*Configure GPIO pin : LED_S_Pin */
	GPIO_InitStruct.Pin = LED_S_Pin;
	GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
	GPIO_InitStruct.Pull = GPIO_NOPULL;
	GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
	HAL_GPIO_Init(LED_S_GPIO_Port, &GPIO_InitStruct);

}

#ifdef  USE_FULL_ASSERT
/**
 * @brief  Reports the name of the source file and the source line number
 *         where the assert_param error has occurred.
 * @param  file: pointer to the source file name
 * @param  line: assert_param error line source number
 * @retval None
 */
void assert_failed(uint8_t* file, uint32_t line) {
	/* USER CODE BEGIN 6 */
	/* User can add his own implementation to report the file name and line number,
	 tex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
	/* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

/**
 * @}
 */

/**
 * @}
 */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
