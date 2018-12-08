/*
 * error.h
 *
 *  Created on: 30.11.2018
 *      Author: MB
 */

#ifndef ERROR_H_
#define ERROR_H_

#include "main.h"

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
void SetPossiblePulseSendQueueFullError(void);
void SetPulseSenderCreateTaskError(void);
void SetCanSendError(void);


#endif /* ERROR_H_ */