/**
 ******************************************************************************
 * File Name          : stm32f1xx_hal_msp.c
 * Description        : This file provides code for the MSP Initialization
 *                      and de-Initialization codes.
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
#include "stm32f1xx_hal.h"
extern DMA_HandleTypeDef hdma_adc1;

extern void _Error_Handler(char *, int);
/* USER CODE BEGIN 0 */

/* USER CODE END 0 */
/**
 * Initializes the Global MSP.
 */
void HAL_MspInit(void) {
	/* USER CODE BEGIN MspInit 0 */

	/* USER CODE END MspInit 0 */

	__HAL_RCC_AFIO_CLK_ENABLE()	;
	__HAL_RCC_PWR_CLK_ENABLE()	;

	HAL_NVIC_SetPriorityGrouping(NVIC_PRIORITYGROUP_4);

	/* System interrupt init*/
	/* MemoryManagement_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(MemoryManagement_IRQn, 0, 0);
	/* BusFault_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(BusFault_IRQn, 0, 0);
	/* UsageFault_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(UsageFault_IRQn, 0, 0);
	/* SVCall_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(SVCall_IRQn, 0, 0);
	/* DebugMonitor_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(DebugMonitor_IRQn, 0, 0);
	/* PendSV_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(PendSV_IRQn, 15, 0);
	/* SysTick_IRQn interrupt configuration */
	HAL_NVIC_SetPriority(SysTick_IRQn, 15, 0);

	/**ENABLE: Full SWJ (JTAG-DP + SW-DP): Reset State
	 */
	__HAL_AFIO_REMAP_SWJ_ENABLE();

	/* USER CODE BEGIN MspInit 1 */

	/* USER CODE END MspInit 1 */
}

void HAL_ADC_MspInit(ADC_HandleTypeDef* hadc) {

	GPIO_InitTypeDef GPIO_InitStruct;
	if (hadc->Instance == ADC1) {
		/* USER CODE BEGIN ADC1_MspInit 0 */

		/* USER CODE END ADC1_MspInit 0 */
		/* Peripheral clock enable */
		__HAL_RCC_ADC1_CLK_ENABLE()
		;

		/**ADC1 GPIO Configuration
		 PC0     ------> ADC1_IN10
		 PC1     ------> ADC1_IN11
		 PC2     ------> ADC1_IN12
		 PC3     ------> ADC1_IN13
		 PA0-WKUP     ------> ADC1_IN0
		 PA1     ------> ADC1_IN1
		 PA2     ------> ADC1_IN2
		 PA3     ------> ADC1_IN3
		 PA4     ------> ADC1_IN4
		 PA5     ------> ADC1_IN5
		 PA6     ------> ADC1_IN6
		 PA7     ------> ADC1_IN7
		 PC4     ------> ADC1_IN14
		 PC5     ------> ADC1_IN15
		 PB0     ------> ADC1_IN8
		 PB1     ------> ADC1_IN9
		 */
		GPIO_InitStruct.Pin = IN10_Pin | IN11_Pin | IN12_Pin | IN13_Pin | IN14_Pin | IN15_Pin;
		GPIO_InitStruct.Mode = GPIO_MODE_ANALOG;
		HAL_GPIO_Init(GPIOC, &GPIO_InitStruct);

		GPIO_InitStruct.Pin = IN0_Pin | IN1_Pin | IN2_Pin | IN3_Pin | IN4_Pin | IN5_Pin | IN6_Pin | IN7_Pin;
		GPIO_InitStruct.Mode = GPIO_MODE_ANALOG;
		HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

		GPIO_InitStruct.Pin = IN8_Pin | IN9_Pin;
		GPIO_InitStruct.Mode = GPIO_MODE_ANALOG;
		HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

		/* ADC1 DMA Init */
		/* ADC1 Init */
		hdma_adc1.Instance = DMA1_Channel1;
		hdma_adc1.Init.Direction = DMA_PERIPH_TO_MEMORY;
		hdma_adc1.Init.PeriphInc = DMA_PINC_DISABLE;
		hdma_adc1.Init.MemInc = DMA_MINC_ENABLE;
		hdma_adc1.Init.PeriphDataAlignment = DMA_PDATAALIGN_HALFWORD;
		hdma_adc1.Init.MemDataAlignment = DMA_MDATAALIGN_HALFWORD;
		hdma_adc1.Init.Mode = DMA_CIRCULAR;
		hdma_adc1.Init.Priority = DMA_PRIORITY_HIGH;
		if (HAL_DMA_Init(&hdma_adc1) != HAL_OK) {
			//_Error_Handler(__FILE__, __LINE__);
		}

		__HAL_LINKDMA(hadc, DMA_Handle, hdma_adc1);

		/* ADC1 interrupt Init */
		HAL_NVIC_SetPriority(ADC1_2_IRQn, 5, 0);
		HAL_NVIC_EnableIRQ(ADC1_2_IRQn);
		/* USER CODE BEGIN ADC1_MspInit 1 */

		/* USER CODE END ADC1_MspInit 1 */
	}

}

void HAL_ADC_MspDeInit(ADC_HandleTypeDef* hadc) {

	if (hadc->Instance == ADC1) {
		/* USER CODE BEGIN ADC1_MspDeInit 0 */

		/* USER CODE END ADC1_MspDeInit 0 */
		/* Peripheral clock disable */
		__HAL_RCC_ADC1_CLK_DISABLE();

		/**ADC1 GPIO Configuration
		 PC0     ------> ADC1_IN10
		 PC1     ------> ADC1_IN11
		 PC2     ------> ADC1_IN12
		 PC3     ------> ADC1_IN13
		 PA0-WKUP     ------> ADC1_IN0
		 PA1     ------> ADC1_IN1
		 PA2     ------> ADC1_IN2
		 PA3     ------> ADC1_IN3
		 PA4     ------> ADC1_IN4
		 PA5     ------> ADC1_IN5
		 PA6     ------> ADC1_IN6
		 PA7     ------> ADC1_IN7
		 PC4     ------> ADC1_IN14
		 PC5     ------> ADC1_IN15
		 PB0     ------> ADC1_IN8
		 PB1     ------> ADC1_IN9
		 */
		HAL_GPIO_DeInit(GPIOC, IN10_Pin | IN11_Pin | IN12_Pin | IN13_Pin | IN14_Pin | IN15_Pin);

		HAL_GPIO_DeInit(GPIOA, IN0_Pin | IN1_Pin | IN2_Pin | IN3_Pin | IN4_Pin | IN5_Pin | IN6_Pin | IN7_Pin);

		HAL_GPIO_DeInit(GPIOB, IN8_Pin | IN9_Pin);

		/* ADC1 DMA DeInit */
		HAL_DMA_DeInit(hadc->DMA_Handle);

		/* ADC1 interrupt DeInit */
		HAL_NVIC_DisableIRQ(ADC1_2_IRQn);
		/* USER CODE BEGIN ADC1_MspDeInit 1 */

		/* USER CODE END ADC1_MspDeInit 1 */
	}

}

void HAL_CAN_MspInit(CAN_HandleTypeDef* hcan) {

	GPIO_InitTypeDef GPIO_InitStruct;
	if (hcan->Instance == CAN2) {
		/* USER CODE BEGIN CAN2_MspInit 0 */

		/* USER CODE END CAN2_MspInit 0 */
		/* Peripheral clock enable */
		__HAL_RCC_CAN2_CLK_ENABLE()
		;
		__HAL_RCC_CAN1_CLK_ENABLE()
		;

		/**CAN2 GPIO Configuration
		 PB5     ------> CAN2_RX
		 PB6     ------> CAN2_TX
		 */
		GPIO_InitStruct.Pin = GPIO_PIN_5;
		GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
		GPIO_InitStruct.Pull = GPIO_NOPULL;

		HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

		GPIO_InitStruct.Pin = GPIO_PIN_6;
		GPIO_InitStruct.Mode = GPIO_MODE_AF_PP;
		GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_HIGH;
		HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

		__HAL_AFIO_REMAP_CAN2_ENABLE()
		;

		/* CAN2 interrupt Init */
		HAL_NVIC_SetPriority(CAN2_RX0_IRQn, 5, 0);
		HAL_NVIC_EnableIRQ(CAN2_RX0_IRQn);
		/* USER CODE BEGIN CAN2_MspInit 1 */

		/* USER CODE END CAN2_MspInit 1 */
	}

}

void HAL_CAN_MspDeInit(CAN_HandleTypeDef* hcan) {

	if (hcan->Instance == CAN2) {
		/* USER CODE BEGIN CAN2_MspDeInit 0 */

		/* USER CODE END CAN2_MspDeInit 0 */
		/* Peripheral clock disable */
		__HAL_RCC_CAN2_CLK_DISABLE();
		__HAL_RCC_CAN1_CLK_DISABLE();

		/**CAN2 GPIO Configuration
		 PB5     ------> CAN2_RX
		 PB6     ------> CAN2_TX
		 */
		HAL_GPIO_DeInit(GPIOB, GPIO_PIN_5 | GPIO_PIN_6);

		/* CAN2 interrupt DeInit */
		HAL_NVIC_DisableIRQ(CAN2_RX0_IRQn);
		/* USER CODE BEGIN CAN2_MspDeInit 1 */

		/* USER CODE END CAN2_MspDeInit 1 */
	}

}

void HAL_UART_MspInit(UART_HandleTypeDef* huart) {

	GPIO_InitTypeDef GPIO_InitStruct;
	if (huart->Instance == USART1) {
		/* USER CODE BEGIN USART1_MspInit 0 */

		/* USER CODE END USART1_MspInit 0 */
		/* Peripheral clock enable */
		__HAL_RCC_USART1_CLK_ENABLE()
		;

		/**USART1 GPIO Configuration
		 PA9     ------> USART1_TX
		 */
		GPIO_InitStruct.Pin = GPIO_PIN_9;
		GPIO_InitStruct.Mode = GPIO_MODE_AF_PP; //GPIO_MODE_AF_OD; // todo mb: von hand ge�ndert
		GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_HIGH;
		HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

		//  rx
		GPIO_InitStruct.Pin = GPIO_PIN_10;
		GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
		GPIO_InitStruct.Pull = GPIO_NOPULL;
		HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

		//__HAL_AFIO_REMAP_USART1_ENABLE(); // dann sendet er auch nicht mehr richtig

		/* USER CODE BEGIN USART1_MspInit 1 */

		/* USER CODE END USART1_MspInit 1 */
	}

}

void HAL_UART_MspDeInit(UART_HandleTypeDef* huart) {

	if (huart->Instance == USART1) {
		/* USER CODE BEGIN USART1_MspDeInit 0 */

		/* USER CODE END USART1_MspDeInit 0 */
		/* Peripheral clock disable */
		__HAL_RCC_USART1_CLK_DISABLE();

		/**USART1 GPIO Configuration
		 PA9     ------> USART1_TX
		 */
		HAL_GPIO_DeInit(GPIOA, GPIO_PIN_9);

		/* USER CODE BEGIN USART1_MspDeInit 1 */

		/* USER CODE END USART1_MspDeInit 1 */
	}

}

void HAL_WWDG_MspInit(WWDG_HandleTypeDef* hwwdg) {

	if (hwwdg->Instance == WWDG) {
		/* USER CODE BEGIN WWDG_MspInit 0 */

		/* USER CODE END WWDG_MspInit 0 */
		/* Peripheral clock enable */
		__HAL_RCC_WWDG_CLK_ENABLE()
		;
		/* USER CODE BEGIN WWDG_MspInit 1 */

		/* USER CODE END WWDG_MspInit 1 */
	}

}

/* USER CODE BEGIN 1 */

/* USER CODE END 1 */

/**
 * @}
 */

/**
 * @}
 */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
