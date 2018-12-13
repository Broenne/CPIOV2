/*
 * usart.c
 *
 *  Created on: 08.12.2018
 *      Author: MB
 */


#include "usart.h"
//#include "main.h"
//#include "delay.h"


extern UART_HandleTypeDef huart1;
#define TBUFSIZE     (256)
#define RBUFSIZE     (256)
#define TMASK        (TBUFSIZE-1)
#define RMASK        (RBUFSIZE-1)
#define RBUFLEN      (uint8_t)(r_in - r_out)
#define TBUFLEN      (uint8_t)(t_in - t_out)


static volatile uint8_t tbuf[TBUFSIZE];
static volatile uint8_t rbuf[RBUFSIZE];
static volatile uint8_t rmsg =0;
static volatile uint8_t t_in =0;
static volatile uint8_t t_out=0;
static volatile uint8_t r_in =0;
static volatile uint8_t r_out=0;
static volatile uint8_t txien=0;



int __io_putchar(int ch) {
//  HAL_UART_Transmit(&huart1, (uint8_t *)&ch, 1, 0xFFFF);
//  return ch;


//	int timeout = 0xFFFF;//UART_TIMEOUT;
//	  while ((TBUFLEN > (TBUFSIZE-8)) && timeout--) // wait a moment, if buffer full
//	  {
//	    //RTOS_WAIT_TICK;
//		  vTaskDelay(1);
//	  }


	  HAL_UART_Transmit(&huart1, (uint8_t *)&ch, 1, 0xFFFF);
	  //UART_PushChar(ch);
	  return 0;

}


//static void UART_PushChar(char ch)
//{
//  if (ch == '\n')
//  {
//    tbuf[t_in++ & TMASK] = '\r';    // add [CR]
//    tbuf[t_in++ & TMASK] = '\n';    // add [NL]
//    newline = 1;
//  }
//  else
//  {
//    tbuf[t_in++ & TMASK] = ch;   // next char
//    newline = 0;
//  }
//  if (txien == DISABLE)
//  {
//    txien  = ENABLE;
//    USART_ITConfig(Open_USART, USART_IT_TXE, ENABLE);  // enable TX interrupt
//  }
//}
//


//int UART_PutChar(char ch)
//{
//  int timeout = UART_TIMEOUT;
//  while ((TBUFLEN > (TBUFSIZE-8)) && timeout--) // wait a moment, if buffer full
//  {
//    RTOS_WAIT_TICK;
//  }
//  UART_PushChar(ch);
//  return 0;
//}







////-------------------------------------------
//// Definition of local constants
////-------------------------------------------
//
//// Buffersizes must be 2^n
//#define TBUFSIZE     (256)
//#define RBUFSIZE     (256)
//#define TMASK        (TBUFSIZE-1)
//#define RMASK        (RBUFSIZE-1)
//#define RBUFLEN      (uint8_t)(r_in - r_out)
//#define TBUFLEN      (uint8_t)(t_in - t_out)
//
//
////--------------------------------------------------------------------------
//// When you are using a RTOS, define RTOS_WAIT_TICK according to your RTOS,
//// if you want schedule other tasks, while waiting for free space in the
//// UART-Buffer. A good value for the TaskDelay(tick-count) is about 10ms.
////--------------------------------------------------------------------------
//// After (UART_TIMEOUT * RTOS_WAIT_TICK) the char will the pushed to the
//// buffer anyway, and a buffer overflow might occur!
////--------------------------------------------------------------------------
////#ifdef RTOS_COOS
////  #define UART_TIMEOUT     (5)
////  #define RTOS_WAIT_TICK   CoTickDelay(1)  /* 1 CoOS-Tick = 10ms */
////#else
//  #define UART_TIMEOUT     (1000)
//  #define RTOS_WAIT_TICK   delay_us(50)    /* use blocking wait  */
////#endif
//
////-------------------------------------------
//// Declaration of local variables
////-------------------------------------------
//static volatile uint8_t tbuf[TBUFSIZE];
//static volatile uint8_t rbuf[RBUFSIZE];
//static volatile uint8_t rmsg =0;
//static volatile uint8_t t_in =0;
//static volatile uint8_t t_out=0;
//static volatile uint8_t r_in =0;
//static volatile uint8_t r_out=0;
//static volatile uint8_t txien=0;
//
//static uint8_t echo    = USART_ECHO;
//static uint8_t newline = 0;

//
//
//int UART_GetLine(char *str)
////-------------------------------------------------------
//// Check if a CR-terminated line is received on UART
//// if 'YES' then copy line to *str and return(StringLength)
//// else return(StringLength=0)
////-------------------------------------------------------
//{
//  int i=0;
//  if(rmsg)                               // if message in buffer available
//  {
//    rmsg--;                              // decrease message buffer
//    str[i]=rbuf [r_out++ & RMASK];       // get first char
//    while(str[i])                        // while not stringend
//    {
//      str[++i]=rbuf [r_out++ & RMASK];   // get next char
//    }
//    if (i==0)
//    {
//      str[0]='\r';                        // insert [CR]
//      str[1]='\0';                        // insert NULL
//      i=1;                                // adjust string length
//    }
//  }
//  return(i);                              // return(StringLength)
//}
//
////===============================================================================
//// Check if cursor is on a new and empty line, else insert CR+LF
////-------------------------------------------------------------------------------
//void UART_GoToNewLine(void)
//{
//  int timeout = UART_TIMEOUT;
//  if (newline == 0)
//  {
//    while ((TBUFLEN > (TBUFSIZE-8)) && timeout--);  // wait a moment, if buffer full
//    UART_PushChar('\n');
//  }
//}
//
////===============================================================================
//// used for printf()
//// - Put a char to the transmit buffer. Wait some time if
////   buffer is full, else push the char to the transmit buffer.
//// - Keeping some free bytes on the buffer, needed by the RX-ISR for its echo.
////-------------------------------------------------------------------------------
//int UART_PutChar(char ch)
//{
//  int timeout = UART_TIMEOUT;
//  while ((TBUFLEN > (TBUFSIZE-8)) && timeout--) // wait a moment, if buffer full
//  {
//    RTOS_WAIT_TICK;
//  }
//  UART_PushChar(ch);
//  return 0;
//}
//
////===============================================================================
//// Called by UART_PutChar() or by RX-ISR echo
//// - Push a char to the transmit buffer, no-wait and no check if buffer full
//// - Inserts automatically a '\r' prior to any '\n'
//// - Enable the UART TX Interrupt if necessary
////-------------------------------------------------------------------------------
//static void UART_PushChar(char ch)
//{
//  if (ch == '\n')
//  {
//    tbuf[t_in++ & TMASK] = '\r';    // add [CR]
//    tbuf[t_in++ & TMASK] = '\n';    // add [NL]
//    newline = 1;
//  }
//  else
//  {
//    tbuf[t_in++ & TMASK] = ch;   // next char
//    newline = 0;
//  }
//  if (txien == DISABLE)
//  {
//    txien  = ENABLE;
//    USART_ITConfig(Open_USART, USART_IT_TXE, ENABLE);  // enable TX interrupt
//  }
//}
//
//
//
////=================================================================================
//void USARTx_IRQHANDLER(void)
//{
//  char ch;
//  //===============================================================================
//  // USART RX Interrupt Service Routine
//  //-------------------------------------------------------------------------------
//  if (USART_GetFlagStatus(Open_USART, USART_FLAG_RXNE) == SET)
//  {
//    ch = USART_ReceiveData(Open_USART); // get received character
//    if (ch > 0x1F)                      // if printable char
//    {
//      rbuf[r_in++ & RMASK] = ch;        // add char to buffer
//      if (echo)
//      {
//        UART_PushChar(ch);              // echo
//      }
//    }
//    else if (ch=='\r')                  // if char = [CR]
//    {
//      rbuf[r_in++ & RMASK] = '\0';      // add stringend to buffer
//      rmsg++;                           // increment message counter
//      if (echo)
//      {
//        UART_PushChar('\n');            // echo '\n' for [CR]+[LF]
//      }
//    }
//    else if (ch=='\b')                  // if backspace
//    {
//      if (rbuf[(r_in-1) & RMASK])       // if there is a char, left from cursor
//      {
//        rbuf[(--r_in) & RMASK]=0;       // then delete it and go one char back.
//        if (echo)
//        {
//          UART_PushChar('\b');          // Back-SPACE
//          UART_PushChar(' ');           // echo a SPACE to delete char on Terminal
//          UART_PushChar('\b');          // Back-SPACE again
//        }
//      }
//    }
//  }
//  //===============================================================================
//  // USART TX Interrupt Service Routine
//  //-------------------------------------------------------------------------------
//  if (USART_GetFlagStatus(Open_USART, USART_FLAG_TXE) == SET)
//  {
//    // If TX-Buffer contains Data
//    if (t_in != t_out)
//    {
//      // then send next char
//      USART_SendData(Open_USART, tbuf[t_out & TMASK]);
//      t_out++;   // pointer to next char
//    }
//    else
//    {
//      // else disable the DataRegisterEmpty Interrupt
//      USART_ITConfig(Open_USART, USART_IT_TXE, DISABLE);
//      txien  = DISABLE;
//    }
//  }
//}
////=================================================================================
//
///* Use no semihosting */
//#if 0
//#pragma import(__use_no_semihosting)
//struct __FILE
//{
//	int handle;
//};
//FILE __stdout;
//
//int _sys_exit(int x)
//{
//	x = x;
//	return(0);
//}
//#endif
//
//
//
///**
//  * @brief  Retargets the C library printf function to the USART.
//  * @param  None
//  * @retval None
//  */
//PUTCHAR_PROTOTYPE
//{
//  /* Place your implementation of fputc here */
//  /* e.g. write a character to the USART */
//  UART_PutChar((char)ch);
//  return ch;
//}
//
//
