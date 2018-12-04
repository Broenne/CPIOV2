/*
 * error.c
 *
 *  Created on: 30.11.2018
 *      Author: tbe241
 */


#include "error.h"

/*
 * 30.11.2018
 * Author: MB
 * Setzen des errors aus dem counter befehl.
 * */
void SetCounterError(void){
	// todo mb: Fehler merken
	printf("ERROR die Infos können nicht mehr weggeschickt werden \r\n");
	// Reset();
}

void SetApplicationEndError(void){
	printf("This should never reached! \r\n");
}

void SetCouldNotSafeGlobalCanIdError(){
	printf("Could not write eeprom \r\n");
}

void SetCouldNotReadGlobalCanIdError(void){
	printf("Variable can id in eeprom not found \n");
}

void SetPossiblePulseSendQueueFullError(void){
	printf("Error in pulse queue send. Possible overflow. \r\n");
}

void SetPulseSenderCreateTaskError(void){
	printf("Error in create task for send can pulse information. \r\n");
}
