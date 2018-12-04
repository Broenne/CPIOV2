/*
 * error.c
 *
 *  Created on: 30.11.2018
 *      Author: MB
 */


#include "error.h"

static uint8_t errorFlgas[4];

/*
 * Created on: 30.11.2018
 * Author: MB
 * Set the application status to paramter.
 * */
void GetApplicationStatus(uint8_t* data){
	memcpy(data,errorFlgas,2);
}

/*
 * Created on: 30.11.2018
 * Author: MB
 * Setzen des errors aus dem counter befehl.
 * */
void SetCounterError(void){
	// todo mb: Fehler merken
	printf("ERROR die Infos k�nnen nicht mehr weggeschickt werden \r\n");
	errorFlgas[0] = errorFlgas[0] ^ ( 1 << 1); // todo mb: die in alive einbauen
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

void SetCanSendError(void){
	printf("Error in can send");
}
