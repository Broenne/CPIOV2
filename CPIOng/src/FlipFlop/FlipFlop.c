/*
 * FlipFlop.c
 *
 *  Created on: 24.01.2019
 *      Author: MB
 */

#include "FlipFlop.h"

static volatile uint16_t StorageFlipFlopState = 0;

/*
 *  Created on: 24.01.2019
 *      Author: MB
 *
 *      Die Funktion setzt den FlipFlop Zustand des Kanals, wenn es eine �nderung gab.
 *      Es wird eine Nachricht auf den CAN-BUS mit der Info gelegt.
 */
void SetFlipFlop(uint8_t channel) {

	int mask = 0;
	mask = 1 << channel;
	StorageFlipFlopState |= mask;

	uint16_t data = StorageFlipFlopState;
	SendFlipFlopStateViaCan(data);
}

/*
 *  Created on: 24.01.2019
 *      Author: MB *
 *      Es wird der Kanal, oder die Kan�le zur�ck gesetzt
 */
void ResetFlipFlop(uint8_t* resetChannelsBitmask) {

	uint16_t resetMask = 0;
	resetMask |= (resetChannelsBitmask[0]);
	resetMask |= (resetChannelsBitmask[1] << 8);

	for(int i = 0; i < CHANNEL_COUNT; ++i){
		if((resetMask >> i) & 0x01){
			// wenn das entsprechende bit in der maske gesetz ist, dann zur�ck setzen
			uint16_t singleMaskForThisChannel = 1 << i;
			singleMaskForThisChannel = ~singleMaskForThisChannel; // invertiert
			StorageFlipFlopState = StorageFlipFlopState & singleMaskForThisChannel;
		}
	}

	// zur Best�tigung wieder wegsenden
	uint16_t data = StorageFlipFlopState;
	SendFlipFlopStateViaCan(data);
}

