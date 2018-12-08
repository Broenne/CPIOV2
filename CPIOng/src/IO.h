/*
 * IO.h
 *
 *  Created on: 05.12.2018
 *      Author: MB
 */

#ifndef IO_H_
#define IO_H_

#include "include.h"

void InitReadIO(void);
uint8_t ReadInputsFromRegisterA(void);
uint8_t ReadInputsFromRegisterB(void);
uint8_t ReadInputsFromRegisterC(void);
void GetInputs(uint8_t* data);


#endif /* IO_H_ */