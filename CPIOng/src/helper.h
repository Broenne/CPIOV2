/*
 * helper.h
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#ifndef HELPER_H_
#define HELPER_H_

#include "include.h"

void ActivateDebug(uint activate);
void Reset(void);
void myPrintf(char* resString);
void myPrintf_ToArg2(char* resString, int arg1, int arg2);


void GetIfNewTextAvailable(uint8_t* data);
void GetTextDataForRow(uint8_t pos, uint8_t posInRow, uint8_t* data);

#endif /* HELPER_H_ */
