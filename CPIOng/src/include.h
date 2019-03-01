/*
 * include.h
 *
 *  Created on: 06.12.2018
 *      Author: MB
 */

#ifndef INCLUDE_H_
#define INCLUDE_H_



#define CHANNEL_COUNT 					(( unsigned short ) 16 )
#define CAN_DATA_LENGTH_MAX					(( unsigned short ) 8 )

#define QUEUE_SIZE_FOR_CAN_RECEIVE		( ( unsigned short ) 32 )
#define QUEUE_SIZE_FOR_CAN_SEND 		( ( unsigned short ) 32 )

#define Major ((uint8_t)0x00)
#define Minor ((uint8_t)0x00)
#define Bugfix ((uint8_t)0x01)

#define FLIPFLOP_OPENCAN_OFFSET 		(( unsigned short ) 0x00 )
#define PULSE_OPENCAN_OFFSET 			(( unsigned short ) 0x1 ) // 1, 2, 3
#define FLIPFLOP_OPENCAN_OFFSET_RESET 	(( unsigned short ) 0x4 )
#define ANALOG_REQUEST 					(( unsigned short ) 0x175 )
#define SET_ACTIVE_SENSOR 				(( unsigned short ) 0x176 )
//#define REQUEST_INPUT_CONFIG			(( unsigned short ) 0x178 )
#define SEND_INPUT_CONFIG				(( unsigned short ) 0x179 )


// Extended-offset-ids
#define REQUEST_TEXT 					(( unsigned int ) 0xFFFFFF )
#define ALIVE_OFFSET 					(( unsigned int ) 0xFFFFFE )
#define INPUT_STATE 					(( unsigned int ) 0xFFFFFD )
#define REQUEST_INPUT_CONFIG			(( unsigned int ) 0xFFFFFB )

#include "main.h"
#include "stm32f1xx_hal.h"
#include "cmsis_os.h"
#include <stdlib.h>
#include <string.h>
#include <stdio.h>


#include "eeprom/eeprom.h"
#include "eeprom/eepromConfig.h"

#include "helper.h"
#include "channelConfig.h"
#include "pulse.h"
#include "storages.h"
#include "error.h"
#include "IO.h"
#include "alive.h"
#include "can.h"
#include "myCanFunctions.h"
#include "usart/usart.h"
#include "analog.h"
#include "FlipFlop/FlipFlop.h"
#include "Can/CanFilter.h"



#endif /* INCLUDE_H_ */
