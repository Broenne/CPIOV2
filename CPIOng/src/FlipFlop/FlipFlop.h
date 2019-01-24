/*
 * FlipFlop.h
 *
 *  Created on: 24.01.2019
 *      Author: MB
 */

#ifndef FLIPFLOP_FLIPFLOP_H_
#define FLIPFLOP_FLIPFLOP_H_

#include "../include.h"

/*
 *  Created on: 24.01.2019
 *      Author: MB
 *
 *      Die Funktion setzt den FlipFlop Zustand des Kanals, wenn es eine Änderung gab.
 *      Es wird eine Nachricht auf den CAN-BUS mit der Info gelegt.
 */
void SetFlipFlop(uint8_t channel);


void ResetFlipFlop(uint8_t* resetChannelsBitmask);


#endif /* FLIPFLOP_FLIPFLOP_H_ */
