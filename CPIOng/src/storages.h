/*
 * storages.h
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#ifndef STORAGES_H_
#define STORAGES_H_

#include "include.h"

void InitVirtualEeprom(void);
void SafeGlobalCanId(uint8_t id);
uint8_t GetGloablCanIdFromEeeprom(void);

void SafeUsedActiveSensorType(uint8_t channelModiType);
uint8_t GetUsedActiveSensorType(void);

#endif /* STORAGES_H_ */
