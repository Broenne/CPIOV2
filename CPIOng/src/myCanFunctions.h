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
void SetGlobalCanNodeId(uint8_t canId);
uint8_t GetGlobalCanNodeId();

/*
 * 8.12.18
 * MB
* Funktion um den Filter auf die aktuelle ID zu setzen
  * */
//void FilterOnlyMyId(CAN_HandleTypeDef* hcan);


/*
 * 30.11.18
 * MB
* Funktion zum senden der Puls-Zeitinformation per CAN.
  * */
void SendCanTimeDif(uint8_t channel, uint32_t res);


#endif /* MYCANFUNCTIONS_H_ */
