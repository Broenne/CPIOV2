/*
 * myCanFunctions.h
 *
 *  Created on: 29.11.2018
 *      Author: tbe241
 */

#ifndef MYCANFUNCTIONS_H_
#define MYCANFUNCTIONS_H_

#include "main.h"

void PrepareCan(void);
void SetGlobalCanNodeId(uint8_t canId);
uint8_t GetGlobalCanNodeId();

#endif /* MYCANFUNCTIONS_H_ */
