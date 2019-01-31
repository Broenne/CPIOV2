/*
 * include.h
 *
 *  Created on: 06.12.2018
 *      Author: MB
 */

#ifndef INCLUDE_H_
#define INCLUDE_H_


#define CHANNEL_COUNT 					( ( unsigned short ) 16 )



#include "main.h"
//#include "stm32f1xx_hal_gpio.h"
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


#define Major ((uint8_t)0x00)
#define Minor ((uint8_t)0x00)
#define Bugfix ((uint8_t)0x01)




#define FLIPFLOP_OPENCAN_OFFSET 		(( unsigned short ) 0x170 )
#define FLIPFLOP_OPENCAN_OFFSET_RESET 	(( unsigned short ) 0x172 )
#define REQUEST_INPUT_CONFIG			(( unsigned short ) 0x178)
#define PULSE_OPENCAN_OFFSET 			( ( unsigned short ) 0x180 )


#endif /* INCLUDE_H_ */
