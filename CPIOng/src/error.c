/*
 * error.c
 *
 *  Created on: 30.11.2018
 *      Author: tbe241
 */


#include "error.h"

/*
 * 30.11.2018
 * MB
 * Setzen des errors aus dem counter befehl.
 * */
void SetCounterError(void){
	// todo mb: Fehler merken
	printf("ERROR die Infos k�nnen nicht mehr weggeschickt werden");
	// Reset();
}

void SetApplicationEndError(void){
	printf("This should never reached!");
}
