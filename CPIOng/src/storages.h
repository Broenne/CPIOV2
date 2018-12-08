/*
 * storages.h
 *
 *  Created on: 29.11.2018
 *      Author: tbe241
 */

#ifndef STORAGES_H_
#define STORAGES_H_

#include "include.h"

void InitVirtualEeprom(void);
void SafeGlobalCanId(uint8_t id);
uint8_t GetGloablCanIdFromEeeprom(void);

#endif /* STORAGES_H_ */
