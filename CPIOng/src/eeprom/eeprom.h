/*
 * eeprom.h
 *
 *  Created on: 08.12.2018
 *      Author: MB
 */

#ifndef EEPROM_EEPROM_H_
#define EEPROM_EEPROM_H_

#include <stdbool.h>
#include "stm32f1xx_hal.h"



// https://github.com/nimaltd/EEPROM.git

//################################################################################################################
bool	EE_Format(void);
bool 	EE_Read(uint16_t VirtualAddress, uint32_t* Data);
bool 	EE_Write(uint16_t VirtualAddress, uint32_t Data);
bool	EE_Reads(uint16_t StartVirtualAddress,uint16_t HowMuchToRead,uint32_t* Data);
bool 	EE_Writes(uint16_t StartVirtualAddress,uint16_t HowMuchToWrite,uint32_t* Data);
//################################################################################################################

#endif /* EEPROM_EEPROM_H_ */
