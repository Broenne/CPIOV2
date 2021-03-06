/*
 * myCanFunctions.h
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#ifndef MYCANFUNCTIONS_H_
#define MYCANFUNCTIONS_H_

#include "include.h"

void PrepareCan(void);
void SetGlobalCanNodeId(uint16_t canId);
uint16_t GetGlobalCanNodeId();

/*
 * 30.11.18
 * MB
* Funktion zum senden der Puls-Zeitinformation per CAN.
  * */
void SendCanTimeDif(uint8_t channel, uint32_t res, uint8_t checkSum);

/*
 * 24.01.19
 * MB
* Funktion zum senden der flipflopstates per can. Ein mapping findet in der Funktion statt.
  * */
void SendFlipFlopStateViaCan(uint16_t flipFlopState);

#endif /* MYCANFUNCTIONS_H_ */
