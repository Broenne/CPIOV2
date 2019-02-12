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
void ReadInputs(uint8_t* data);
void GetInputs(uint8_t* data);
int ReadChannelAnalog(uint pos);

#endif /* IO_H_ */
