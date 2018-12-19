/*
 * error.c
 *
 *  Created on: 30.11.2018
 *  Author: MB
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

// max 7, dann nächstes byte
#define COUNTER_ERROR_POSITION (1)
#define APPLICATION_END_ERROR (2)
#define Could_NOT_SAFE_GLOBAL_CAN_ID_ERROR (3)
#define Could_Not_Read_Global_Can_Id_Error (2)
#define Possible_Pulse_Send_Queue_Full_Error (4)
#define Pulse_Sender_Create_Task_Error (5)
#define Can_Send_Error (6)

void SetErrorFlag(int pos){
	errorFlgas[pos / 8] = errorFlgas[pos / 8] ^ ( 1 << pos);
}

/*
 * Created on: 30.11.2018
 * Author: MB
 * Setzen des errors aus dem counter befehl.
 * */
void SetCounterError(void){
	// todo mb: Fehler merken KEIN!!!!!! printf nutzen. Eigene Funktion myprinft (noch zu schreiben)
	myPrintf("ERROR die Infos können nicht mehr weggeschickt werden \r\n");
	SetErrorFlag(COUNTER_ERROR_POSITION);
	// Reset();
}

void SetApplicationEndError(void){
	myPrintf("This should never reached! \r\n");
	SetErrorFlag(APPLICATION_END_ERROR);
}

void SetCouldNotSafeGlobalCanIdError(){
	myPrintf("Could not write eeprom \r\n");
	SetErrorFlag(Could_NOT_SAFE_GLOBAL_CAN_ID_ERROR);
}

void SetCouldNotReadGlobalCanIdError(void){
	myPrintf("Variable can id in eeprom not found \n");
}

void SetPossiblePulseSendQueueFullError(void){
	myPrintf("Error in pulse queue send. Possible overflow. \r\n");
}

void SetPulseSenderCreateTaskError(void){
	myPrintf("Error in create task for send can pulse information. \r\n");
}

void SetCanSendError(void){
	myPrintf("Error in can send \r\n");
}

void SetSendAliveError(void){
	myPrintf("Set send alive error. \r\n");
}
