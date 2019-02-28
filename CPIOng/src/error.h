/*
 * error.h
 *
 *  Created on: 30.11.2018
 *      Author: MB
 */

#ifndef ERROR_H_
#define ERROR_H_

#include "include.h"

/*
 * Created on: 30.11.2018
 * Author: MB
 * Set the application status to paramter.
 * */
void GetApplicationStatus(uint8_t*);

void SetCounterError(void);
void SetApplicationEndError(void);

void SetCouldNotSafeGlobalCanIdError(void);
void SetCouldNotReadGlobalCanIdError(void);

void SetCouldNotFindSpecificSensorPosition(void);

void SetPossiblePulseSendQueueFullError(void);
void SetPulseSenderCreateTaskError(void);

void SetCanSendError(void);
void SetCanWorkerTaskError(void);
void SetSendAliveError(void);

void SetInitPulseTimerError(void);
void SetPulseSenderTaskError(void);

void SetPeriperialInitError(void);

#endif /* ERROR_H_ */
