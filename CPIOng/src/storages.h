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

/*
 * Created on: 11.02.19
 * Author: MB
 * Function for save channel modi to eeprom
 * */
void SafeChannelConfig(uint channel, uint8_t channelModiType);

#endif /* STORAGES_H_ */
