/*
 * uart.h
 *
 *  Created on: 30.11.2018
 *      Author: tbe241
 */

#ifndef USART_H_
#define USART_H_

#define	USART1_OPEN
//#define	USART2_OPEN
//#define	USART3_OPEN
//#define USART6_OPEN

#define USART_BAUD     (57600)
#define USART_ECHO     1//ON
#define USART_DATA     (USART_WordLength_8b)
#define USART_PARITY   (USART_Parity_No)
#define USART_STOP     (USART_StopBits_1)
#define USART_FLOWCTRL (USART_HardwareFlowControl_None)

/**
 * @brief Definition for COM port1, connected to USART3
 */
#if defined	USART2_OPEN
#define Open_USART                        USART2
#define Open_USART_CLK                    RCC_APB1Periph_USART2

#define Open_USART_TX_PIN                 GPIO_Pin_2
#define Open_USART_TX_GPIO_PORT           GPIOA
#define Open_USART_TX_GPIO_CLK            RCC_AHB1Periph_GPIOA
#define Open_USART_TX_SOURCE              GPIO_PinSource2
#define Open_USART_TX_AF                  GPIO_AF_USART2

#define Open_USART_RX_PIN                 GPIO_Pin_3
#define Open_USART_RX_GPIO_PORT           GPIOA
#define Open_USART_RX_GPIO_CLK            RCC_AHB1Periph_GPIOA
#define Open_USART_RX_SOURCE              GPIO_PinSource3
#define Open_USART_RX_AF                  GPIO_AF_USART2

#define Open_USART_IRQn                   USART2_IRQn
#define USARTx_IRQHANDLER                 USART2_IRQHandler

#elif	defined	USART1_OPEN
#define Open_USART                        USART1
#define Open_USART_CLK                    RCC_APB2Periph_USART1

#define Open_USART_TX_PIN                 GPIO_Pin_9
#define Open_USART_TX_GPIO_PORT           GPIOA
#define Open_USART_TX_GPIO_CLK            RCC_APB2Periph_GPIOA
#define Open_USART_TX_SOURCE              GPIO_PinSource9
#define Open_USART_TX_AF                  GPIO_AF_USART1

#define Open_USART_RX_PIN                 GPIO_Pin_10
#define Open_USART_RX_GPIO_PORT           GPIOA
#define Open_USART_RX_GPIO_CLK            RCC_APB2Periph_GPIOA
#define Open_USART_RX_SOURCE              GPIO_PinSource10
#define Open_USART_RX_AF                  GPIO_AF_USART1

#define Open_USART_IRQn                   USART1_IRQn
#define USARTx_IRQHANDLER                 USART1_IRQHandler

#elif	defined	USART3_OPEN
#define Open_USART                        USART3
#define Open_USART_CLK                    RCC_APB1Periph_USART3

#define Open_USART_TX_PIN                 GPIO_Pin_10
#define Open_USART_TX_GPIO_PORT           GPIOB
#define Open_USART_TX_GPIO_CLK            RCC_APB2Periph_GPIOB
#define Open_USART_TX_SOURCE              GPIO_PinSource10
#define Open_USART_TX_AF                  GPIO_AF_USART3

#define Open_USART_RX_PIN                 GPIO_Pin_11
#define Open_USART_RX_GPIO_PORT           GPIOB
#define Open_USART_RX_GPIO_CLK            RCC_APB2Periph_GPIOB
#define Open_USART_RX_SOURCE              GPIO_PinSource11
#define Open_USART_RX_AF                  GPIO_AF_USART3

#define Open_USART_IRQn                   USART3_IRQn
#define USARTx_IRQHANDLER                 USART3_IRQHandler
#elif defined USART6_OPEN
#define Open_USART                        USART6
#define Open_USART_CLK                    RCC_APB2Periph_USART6

#define Open_USART_TX_PIN                 GPIO_Pin_6
#define Open_USART_TX_GPIO_PORT           GPIOC
#define Open_USART_TX_GPIO_CLK            RCC_AHB1Periph_GPIOC
#define Open_USART_TX_SOURCE              GPIO_PinSource6
#define Open_USART_TX_AF                  GPIO_AF_USART6

#define Open_USART_RX_PIN                 GPIO_Pin_7
#define Open_USART_RX_GPIO_PORT           GPIOC
#define Open_USART_RX_GPIO_CLK            RCC_AHB1Periph_GPIOC
#define Open_USART_RX_SOURCE              GPIO_PinSource7
#define Open_USART_RX_AF                  GPIO_AF_USART6

#define Open_USART_IRQn                   USART6_IRQn
#define USARTx_IRQHANDLER                 USART6_IRQHandler
#else
#error "Please select The COM to be used (in usart.h)"
#endif

int UART_PutChar(char ch);
int UART_GetLine(char *str);
void UART_GoToNewLine(void);
void UART_Init(void);

#endif /*_USART_H*/
