/*
 * include.h
 *
 *  Created on: 06.12.2018
 *      Author: MB
 */

#ifndef INCLUDE_H_
#define INCLUDE_H_


#define CHANNEL_COUNT 					(( unsigned short ) 16 )

#define Major ((uint8_t)0x00)
#define Minor ((uint8_t)0x00)
#define Bugfix ((uint8_t)0x01)

#define FLIPFLOP_OPENCAN_OFFSET 		(( unsigned short ) 0x170 )
#define FLIPFLOP_OPENCAN_OFFSET_RESET 	(( unsigned short ) 0x172 )
#define ANALOG_REQUEST 					(( unsigned short ) 0x175 )
#define SET_ACTIVE_SENSOR 				(( unsigned short ) 0x176 )
#define REQUEST_TEXT 					(( unsigned short ) 0x177 )
#define REQUEST_INPUT_CONFIG			(( unsigned short ) 0x178 )
#define SEND_INPUT_CONFIG				(( unsigned short ) 0x179 )
#define PULSE_OPENCAN_OFFSET 			(( unsigned short ) 0x180 )
#define AliveOffset 					(( unsigned short )	0x200 )


#include "main.h"
#include "stm32f1xx_hal.h"
#include "cmsis_os.h"
#include <stdlib.h>
#include <string.h>
#include <stdio.h>


#include "eeprom/eeprom.h"
#include "eeprom/eepromConfig.h"

#include "helper.h"
#include "pulseConfig.h"
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
