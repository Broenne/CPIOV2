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

/*
 * 30.11.18
 * MB
* Funktion zum senden der Puls-Zeitinformation per CAN.
  * */
void SendCanTimeDif(uint8_t channel, uint32_t res);


#endif /* MYCANFUNCTIONS_H_ */