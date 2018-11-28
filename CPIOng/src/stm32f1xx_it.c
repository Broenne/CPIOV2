/**
 ******************************************************************************
 * @file    stm32f1xx_it.c
 * @author  MCD Application Team
 * @version V1.0.0
 * @date    11-February-2014
 * @brief   Main Interrupt Service Routines.
 *          This file provides template for all exceptions handler and
 *          peripherals interrupt service routine.
 ******************************************************************************
 * @attention
 *
 * <h2><center>&copy; COPYRIGHT 2014 STMicroelectronics</center></h2>
 *
 * Licensed under MCD-ST Liberty SW License Agreement V2, (the "License");
 * You may not use this file except in compliance with the License.
 * You may obtain a copy of the License at:
 *
 *        http://www.st.com/software_license_agreement_liberty_v2
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 ******************************************************************************
 */

/* Includes ------------------------------------------------------------------*/
#include "stm32f1xx_it.h"

/** @addtogroup IO_Toggle
 * @{
 */

/* Private typedef -----------------------------------------------------------*/
/* Private define ------------------------------------------------------------*/
/* Private macro -------------------------------------------------------------*/
/* Private variables ---------------------------------------------------------*/
/* Private function prototypes -----------------------------------------------*/
/* Private functions ---------------------------------------------------------*/

void SwitchMainLed(void) {
	TIM_ClearITPendingBit(TIM2, TIM_IT_Update); // setz timer zur�ck, achtung dann kann man ihn auch anders nicht mehr benutzen
	if (GPIO_ReadOutputDataBit(GPIOD, GPIO_Pin_2)) {
		GPIO_WriteBit(GPIOD, GPIO_Pin_2, RESET);
	} else {
		GPIO_WriteBit(GPIOD, GPIO_Pin_2, SET);
	}
}

// todo mb: Funktion drum herum
volatile static uint32_t tickMs;
volatile static uint32_t lastTimeValue[8];

void SanCanAlive() {
	if (!(tickMs % 500)) {
		uint8_t p[] = { 0, 1, 2, 3, 4, 5, 6, 7 };
		SendCan(0x123, p, 8);
	}
}

void SendCan(uint32_t id, uint8_t data[], uint8_t len) {
	static int i;
	++i;
	CanTxMsg canMessage;
	canMessage.StdId = id;
	canMessage.ExtId = 0;
	canMessage.RTR = CAN_RTR_DATA;
	canMessage.IDE = CAN_ID_STD;
	canMessage.DLC = len;

	memcpy(canMessage.Data, data, len * sizeof(uint8_t));

	while (!(CAN1->TSR & CAN_TSR_TME0 || CAN1->TSR & CAN_TSR_TME1
			|| CAN1->TSR & CAN_TSR_TME2)) {
	} // todo mb: das ding austimen lassen

	CAN_Transmit(CAN2, &canMessage);
}

void TIM2_IRQHandler(void) {
	SwitchMainLed();
	SanCanAlive();
	//++tickMs; // wenn der in ms l�uft, ist alles ok?!
//	if(TIM_GetCounter(TIM2)==1946){
//		++tickMs;//1946
//	}
}

void SendCanTimeDif(uint8_t channel, uint32_t res) {

	uint8_t p[] = { 0, 0, 0, 0, 0 };
	// channel
	p[0] = channel;
	// timestamp
	p[1] = (res >> 24) & 0xFF;
	p[2] = (res >> 16) & 0xFF;
	p[3] = (res >> 8) & 0xFF;
	p[4] = res & 0xFF;

	SendCan(0x01, p, 5);
}

void SendTimeInfo(uint8_t channel) {
	uint32_t actualTimeValue = tickMs;
	uint32_t res;
	if (actualTimeValue > lastTimeValue[channel]) {
		res = actualTimeValue - lastTimeValue[channel];
	} else {
		res = lastTimeValue[channel] - actualTimeValue;
	}

	SendCanTimeDif(channel, res);
	printf("Rising edge on .... In %d %d \n", channel, res);
	lastTimeValue[channel] = actualTimeValue;
}

//
//void EXTI0_IRQHandler(void) {
//
//	/* Make sure that interrupt flag is set */
//	    if (EXTI_GetITStatus(EXTI_Line0) != RESET) {
//
//
//	    	// hier kommt intzerrupt von 0 (PA0) oder 8 (PB0)
//
//	    		/* Disable interrupts */
//	    		__disable_irq();
//
//	    		// alten un neuen wert merken, um genau zu detektieren
//	    		if(GPIO_ReadInputDataBit(GPIOA, GPIO_Pin_0))
//	    		{
//	    			printf("Rising edge on A \n");
//	    		}
//	    		if(GPIO_ReadInputDataBit(GPIOB, GPIO_Pin_0))
//	    		{
//	    			printf("Rising edge on B \n");
//	    		}
//
//	    		SendTimeInfo(0);
//
//	    		EXTI_ClearITPendingBit(EXTI_Line0);
//	    		__enable_irq();
//	    	//	if (GPIO_ReadOutputDataBit(GPIOC, GPIO_Pin_9)) {
//	    	//		GPIO_WriteBit(GPIOC, GPIO_Pin_9, RESET);
//	    	//	} else {
//	    	//		GPIO_WriteBit(GPIOC, GPIO_Pin_9, SET);
//	    	//	}
//
//	    }
//
//
//
//}
//
//void EXTI1_IRQHandler(void) {
//	/* Disable interrupts */
//	__disable_irq();
//
//	SendTimeInfo(1);
//	EXTI_ClearITPendingBit(EXTI_Line1);
//	__enable_irq();
////	if (GPIO_ReadOutputDataBit(GPIOC, GPIO_Pin_9)) {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, RESET);
////	} else {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, SET);
////	}
//}
//
//void EXTI2_IRQHandler(void) {
//	/* Disable interrupts */
//	__disable_irq();
//	SendTimeInfo(2);
//	EXTI_ClearITPendingBit(EXTI_Line2);
//	__enable_irq();
////	if (GPIO_ReadOutputDataBit(GPIOC, GPIO_Pin_9)) {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, RESET);
////	} else {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, SET);
////	}
//}
//
//void EXTI3_IRQHandler(void) {
//	/* Disable interrupts */
//	__disable_irq();
//	SendTimeInfo(3);
//	EXTI_ClearITPendingBit(EXTI_Line3);
//	__enable_irq();
////	if (GPIO_ReadOutputDataBit(GPIOC, GPIO_Pin_9)) {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, RESET);
////	} else {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, SET);
////	}
//}
//
//void EXTI4_IRQHandler(void) {
//	__disable_irq();
//	SendTimeInfo(4);
//	EXTI_ClearITPendingBit(EXTI_Line4);
//	__enable_irq();
////	if (GPIO_ReadOutputDataBit(GPIOC, GPIO_Pin_9)) {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, RESET);
////	} else {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, SET);
////	}
//}
//
//void EXTI9_5_IRQHandler(void) {
//	__disable_irq();
//	printf("Rising edge on .... In 5-9\n");
//
//	//SendTimeInfo(5);
//	EXTI_ClearITPendingBit(EXTI_Line5);
//	__enable_irq();
////	if (GPIO_ReadOutputDataBit(GPIOC, GPIO_Pin_9)) {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, RESET);
////	} else {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, SET);
////	}
//}
//
//void EXTI15_10_IRQHandler(void) {
//	__disable_irq();
//	printf("Rising edge on .... In 15-10\n");
//
//	//SendTimeInfo(5);
//	EXTI_ClearITPendingBit(EXTI_Line5);
//	__enable_irq();
////	if (GPIO_ReadOutputDataBit(GPIOC, GPIO_Pin_9)) {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, RESET);
////	} else {
////		GPIO_WriteBit(GPIOC, GPIO_Pin_9, SET);
////	}
//}

void CAN2_RX0_IRQHandler(void) {
	printf("receive can 2 interrupt\n");
	// todo mb: interrupts sperren
	CanRxMsg RxMessage;
	CAN_Receive(CAN2, CAN_FIFO0, &RxMessage);
	if (RxMessage.Data[0] == 1) {
		GPIO_WriteBit(GPIOA, GPIO_Pin_5, Bit_SET);
	} else {
		GPIO_WriteBit(GPIOA, GPIO_Pin_5, Bit_RESET);
	}
}

/******************************************************************************/
/*            Cortex-M Processor Exceptions Handlers                          */
/******************************************************************************/

/**
 * @brief  This function handles NMI exception.
 * @param  None
 * @retval None
 */
void NMI_Handler(void) {
}

/**
 * @brief  This function handles Hard Fault exception.
 * @param  None
 * @retval None
 */
void HardFault_Handler(void) {
	/* Go to infinite loop when Hard Fault exception occurs */
	while (1) {
	}
}

/**
 * @brief  This function handles Memory Manage exception.
 * @param  None
 * @retval None
 */
void MemManage_Handler(void) {
	/* Go to infinite loop when Memory Manage exception occurs */
	while (1) {
	}
}

/**
 * @brief  This function handles Bus Fault exception.
 * @param  None
 * @retval None
 */
void BusFault_Handler(void) {
	/* Go to infinite loop when Bus Fault exception occurs */
	while (1) {
	}
}

/**
 * @brief  This function handles Usage Fault exception.
 * @param  None
 * @retval None
 */
void UsageFault_Handler(void) {
	/* Go to infinite loop when Usage Fault exception occurs */
	while (1) {
	}
}

/**
 * @brief  This function handles SVCall exception.
 * @param  None
 * @retval None
 */
void SVC_Handler(void) {
}

/**
 * @brief  This function handles Debug Monitor exception.
 * @param  None
 * @retval None
 */
void DebugMon_Handler(void) {
}

/**
 * @brief  This function handles PendSVC exception.
 * @param  None
 * @retval None
 */
void PendSV_Handler(void) {
}

static uint16_t oldGpioA;
static uint16_t oldGpioB;
static uint16_t oldGpioC;

/**
 * @brief  This function handles SysTick Handler.
 * @param  None
 * @retval None
 */
void SysTick_Handler(void) {
	++tickMs;

	uint16_t gpioA = (GPIO_ReadInputData(GPIOA) & 0xFF); // Inetressant sind nur die untesten 8 bits, siehe Schaltplan
	uint16_t gpioB = GPIO_ReadInputData(GPIOB) & 0x03; // Pb0 = 10, pb1 = 11
	uint16_t gpioC = GPIO_ReadInputData(GPIOC) & 0x3F;

	if (gpioA != oldGpioA) {
		uint16_t dif = gpioA ^ oldGpioA; // PinA0 -> I0 	// PinA1 -> I1	// PinA2 -> I2	// PinA3 -> I3	// PinA4 -> I4	// PinA5 -> I5	// PinA6 -> I6	// PinA7 -> I7
		for (int i = 0; i < 8; ++i) {
			// �nderung bit und steigende Flanke
			if ((dif >> i) & 0x01 && (gpioA >> i & 0x01)) {
				SendTimeInfo(i); // Achtung, nur bei a
			}
		}
	}

	if (gpioB != oldGpioB) {
		uint16_t dif = gpioB ^ oldGpioB;
		for (int i = 0; i < 2; ++i) {
			if ((dif >> i) & 0x01 && (gpioB >> i & 0x01)) {
				SendTimeInfo(8 + i);
			}
		}
	}

	if (gpioC != oldGpioC) {
		uint16_t dif = gpioC ^ oldGpioC;
		for (int i = 0; i < 6; ++i) {
			if ((dif >> i) & 0x01 && (gpioC >> i & 0x01)) {
				SendTimeInfo(10 + i);
			}
		}
	}

	// Wert merken
	oldGpioA = gpioA;
	oldGpioB = gpioB;
	oldGpioC = gpioC;
}

/******************************************************************************/
/*                 STM32F1xx Peripherals Interrupt Handlers                   */
/*  Add here the Interrupt Handler for the used peripheral(s) (PPP), for the  */
/*  available peripheral interrupt handler's name please refer to the startup */
/*  file (startup_stm32f10x_md.s).                                            */
/******************************************************************************/

/**
 * @brief  This function handles PPP interrupt request.
 * @param  None
 * @retval None
 */
/*void PPP_IRQHandler(void)
 {
 }*/

/**
 * @}
 */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
