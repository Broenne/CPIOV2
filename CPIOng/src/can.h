
#ifndef _CAN_H
#define _CAN_H

#include "main.h"

void CAN2_init_GPIO(void);
void init_CAN2(void);
void InitCanFilter(void);
void SendCan(uint32_t id, uint8_t data[], uint8_t len);

#endif /*_CAN_H*/