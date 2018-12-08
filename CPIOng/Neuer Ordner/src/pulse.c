/*
 * pulse.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "pulse.h"

#define MessageSize 100

typedef struct {
	uint8_t channel;
	uint32_t res;
} MessageForSend;



/*
 * Created on: 30.11.18
 * Author: MB
 * Initialisierung des Timers f�r die internen Zeitmessung.
 * Bei jedem Aufruf wird der Zeitstempel inkrementiert
 * und die Eing�nge werden ausgelesen.
 * */
void Init_TimerForPulsTime(void) {

	// calculate:
	// updateFrequenz = Clock/((PSC-1)*(Period-1)

	TIM_TimeBaseInitTypeDef TIM_TimeBase_InitStructure;
	NVIC_InitTypeDef NVIC_InitStructure;
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_TIM3, ENABLE); // Timer 3 Interrupt enable
	TIM_TimeBase_InitStructure.TIM_ClockDivision = TIM_CKD_DIV1;
	TIM_TimeBase_InitStructure.TIM_CounterMode = TIM_CounterMode_Up;
	TIM_TimeBase_InitStructure.TIM_Period = 1999; //1999;
	TIM_TimeBase_InitStructure.TIM_Prescaler = 36; // todo mb: langsamer zum testen prescal auf 72 MHz bezogen -> 72Mhz/36 = 2 Mhz  -> 2Mhz = 0,5 us
	TIM_TimeBaseInit(TIM3, &TIM_TimeBase_InitStructure);

	TIM_ITConfig(TIM3, TIM_IT_Update, ENABLE);

	NVIC_InitStructure.NVIC_IRQChannel = TIM3_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x00;//0x0F;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x0F;

	NVIC_Init(&NVIC_InitStructure);
	 NVIC_SetPriority(TIM3_IRQn, 0); // https://electronics.stackexchange.com/questions/76299/stm32-interrupt-priority-preemption-problems

	TIM_Cmd(TIM3, ENABLE);
}

void InitPulse(void) {
	InitInputs(); // todo mb: das in main
	//Init_TimerForPulsTime();
}

volatile static xQueueHandle PulsQueue = NULL;

/*
 * Created on: 30.11.18
 * Author: MB
* Diese Funktion arbeitet die queue ab.
* Es besteht keine Priorit�t und es ist darauf zu achten, das de Prozessor ausreichend Zeit hat, die Information los zu werden.
  * */
void SendPulsePerCanTask(void * pvParameters) {
	/* The parameter value is expected to be 1 as 1 is passed in the  pvParameters value in the call to xTaskCreate() below.*/
	configASSERT(((uint32_t ) pvParameters) == 1);

	int i=0;
	while (1) {
		printf("run %d \r\n", ++i);
		MessageForSend currentMessage;
		 /* Wait for the maximum period for data to become available on the queue.
		        The period will be indefinite if INCLUDE_vTaskSuspend is set to 1 in
		        FreeRTOSConfig.h. */
		//if (xQueueReceive(PulsQueue, &currentMessage, 0) == pdTRUE) {
		//if (xQueueReceiveFromISR(PulsQueue, &currentMessage, 0) == pdTRUE) {
			//printf("Received %d %d \r\n", currentMessage.channel, currentMessage.res);
	//		SendCanTimeDif(currentMessage.channel, currentMessage.res);
	//	}
		//vTaskDelay(10);
	}

	printf("Sender task error \r\n");
}


#define QUEUE_SIZE_FOR_PULSE_INFO		( ( unsigned short ) 10 )

/*
 * Created on: 30.11.18
 * Author: MB
 * Funktion zum initailiserne der Queue zum senden der Impulsinformation.
  * */
void InitQueueForPulse(void){
	//PulsQueue = xQueueCreate(QUEUE_SIZE_FOR_PULSE_INFO, sizeof(MessageForSend));
}

/*
 * Created on: 30.11.18
 * Author: MB
 * Diese Funktion initialisiert die Queue
 * Diese und startet den task um diese abzuarbeiten.
  * */
void InitPulseSender(void) {

	InitQueueForPulse();

	portBASE_TYPE xReturned;
	xTaskHandle xHandle = NULL;

	/* Create the task, storing the handle. */
	xReturned = xTaskCreate(SendPulsePerCanTask, "CanS", configMINIMAL_STACK_SIZE, (void * ) 1, /*tskIDLE_PRIORITY*/5, &xHandle);

	if (xReturned != pdPASS) {
		SetPulseSenderCreateTaskError();
	}

	// todo mb: wann und wie den task deleten?
	/* The task was created.  Use the task's handle to delete the task. */
			//vTaskDelete( xHandle );
}

/*
 * Created on: 30.11.18
 * Author: MB
 * Diese Funktion kalkuliert den time stamp und schiebt in eine FIFO queue.
 * Das wegsenden z.B. per CAN-BUS geh�rt nicht zur Aufgabe.
 * Diese Funktion l�uft im interrupt handling.
 * */
void SendTimeInfo(uint8_t channel) {
	uint32_t actualTimeValue = tickMs;
	uint32_t res;
	if (actualTimeValue > lastTimeValue[channel]) {
		res = actualTimeValue - lastTimeValue[channel];
	} else {
		res = lastTimeValue[channel] - actualTimeValue;
	}

	printf("SendTimeInfo %d \r\n", channel);
	MessageForSend messageForSend;
	messageForSend.channel = channel;
	messageForSend.res = res;

	if (xQueueSendFromISR(PulsQueue, &messageForSend, 0) != pdTRUE) {
		SetPossiblePulseSendQueueFullError();
	}


//	static  portBASE_TYPE xTaskWokenByPost;
//
//	// We have not woken a task at the start of the ISR.
//	xTaskWokenByPost = pdFALSE;
//
//	 xTaskWokenByPost = xQueueSendFromISR( PulsQueue, &messageForSend, xTaskWokenByPost );
//	 if( xTaskWokenByPost )
//	  {
//	    // We should switch context so the ISR returns to a different task.
//	    // NOTE: How this is done depends on the port you are using. Check
//	    // the documentation and examples for your port.
//	    portYIELD_FROM_ISR();
//	  }



	lastTimeValue[channel] = actualTimeValue;
}

void CheckInputsRegisterA(void) {
	static uint8_t oldGpioA = 0;
	uint8_t gpioA = ReadInputsFromRegisterA();

	if (gpioA != oldGpioA) {
		uint8_t dif = gpioA ^ oldGpioA; // PinA0 -> I0 	// PinA1 -> I1	// PinA2 -> I2	// PinA3 -> I3	// PinA4 -> I4	// PinA5 -> I5	// PinA6 -> I6	// PinA7 -> I7
		for (int i = 0; i < 8; ++i) {	// �nderung bit und steigende Flanke
			if ((dif >> i) & 0x01 && (gpioA >> i & 0x01)) {
				SendTimeInfo(i); // Achtung, nur bei a
			}
		}
	}

	oldGpioA = gpioA; // Wert merken
}

void CheckInputsRegisterB(void) {
	static uint8_t oldGpioB = 0;
	uint8_t gpioB = ReadInputsFromRegisterB();

	if (gpioB != oldGpioB) {
		uint8_t dif = gpioB ^ oldGpioB;
		for (int i = 0; i < 2; ++i) {
			if ((dif >> i) & 0x01 && (gpioB >> i & 0x01)) {
				SendTimeInfo(8 + i);
			}
		}
	}

	oldGpioB = gpioB;
}

void CheckInputsRegisterC(void) {
	static uint8_t oldGpioC = 0;
	uint8_t gpioC = ReadInputsFromRegisterC();

	if (gpioC != oldGpioC) {
		uint8_t dif = gpioC ^ oldGpioC;
		for (int i = 0; i < 6; ++i) {
			if ((dif >> i) & 0x01 && (gpioC >> i & 0x01)) {
				SendTimeInfo(10 + i);
			}
		}
	}

	oldGpioC = gpioC;
}

/*
 * Created on: 30.11.18
 * Author: MB
 * Interrupt handler von timer 3
 * Es wird der counter inkrementiert, welcher den timestamp wiederspiegelt.
 * Danach lesen der Eing�nge und ggf Information an queue �bergeben zum wegsenden.
 * */
void TIM3_IRQHandler(void) {

	portDISABLE_INTERRUPTS();
	//__disable_irq();
//	UBaseType_t uxSavedInterruptStatus;
//
//	    /* Call taskENTER_CRITICAL_FROM_ISR() to create a critical section, saving the
//	    returned value into a local stack variable. */
//	    uxSavedInterruptStatus = taskENTER_CRITICAL_FROM_ISR();

	++tickMs;
	CheckInputsRegisterA();
	CheckInputsRegisterB();
	CheckInputsRegisterC();

	TIM_ClearITPendingBit(TIM3, TIM_IT_Update); // setz timer zur�ck, achtung dann kann man ihn auch anders nicht mehr benutzen



	portENABLE_INTERRUPTS();
	//__enable_irq();
	//taskEXIT_CRITICAL_FROM_ISR( uxSavedInterruptStatus );

}