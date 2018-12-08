/*
 * pulse.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "pulse.h"

#define QUEUE_SIZE_FOR_PULSE_INFO		( ( unsigned short ) 100 )

typedef struct {
	uint8_t channel;
	uint32_t res;
} MessageForSend;

static TIM_HandleTypeDef pulseTimerInstance; // = { .Instance = TIM2 };

static xQueueHandle PulsQueue = NULL;
uint32_t PreemptPriorityPulseTimer=5;
uint32_t SubPriorityPulseTimer=5;


static uint32_t TimerCorrectureFactor = 0;

void InitTimerPulseCorrecturFactor(){
	TimerCorrectureFactor = 0; // todo mb: load from eeprom
}

void SetTimerPulseCorrecturFactor(uint16_t value){
	// Info, hier nur int 16 erlaubt, korrektur sonst viel zu gro�. Sollen wir den Bereich noch einschr�nken?
	TimerCorrectureFactor = (uint16_t)value;
	// todo mb: ssafe to eeprom

	// REset();

}

uint32_t GetTimerPulseCorrecturFactor(){
	return TimerCorrectureFactor;
}

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

	InitTimerPulseCorrecturFactor();

	__HAL_RCC_TIM3_CLK_ENABLE()	;
	pulseTimerInstance.Instance = TIM3;
	pulseTimerInstance.Init.Prescaler = 9;
	pulseTimerInstance.Init.CounterMode = TIM_COUNTERMODE_UP;
	pulseTimerInstance.Init.Period = 9001 + GetTimerPulseCorrecturFactor();
	pulseTimerInstance.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
	pulseTimerInstance.Init.RepetitionCounter = 0;
	//s_TimerInstance.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;//TIM_AUTORELOAD_PRELOAD_ENABLE;//
	if (HAL_TIM_Base_Init(&pulseTimerInstance) != HAL_OK) {
		_Error_Handler(__FILE__, __LINE__);
	}

	HAL_TIM_Base_Start(&pulseTimerInstance);
	HAL_NVIC_SetPriority(TIM3_IRQn, PreemptPriorityPulseTimer, SubPriorityPulseTimer); // extrem hohe priorit�t sollte es sein!!!
	HAL_NVIC_EnableIRQ(TIM3_IRQn);

	__HAL_TIM_ENABLE_IT(&pulseTimerInstance, TIM_IT_UPDATE);
}

//osMessageQId myQueue01Handle;

void InitQueueForPulse(void) {
	PulsQueue = xQueueCreate(QUEUE_SIZE_FOR_PULSE_INFO, sizeof(MessageForSend));
//	  osMessageQDef(myQueue01, 16, uint16_t);
//	  myQueue01Handle = osMessageCreate(osMessageQ(myQueue01), NULL);
}

void InitPulse(void) {
	InitQueueForPulse();
	InitPulseSender();
	//InitInputs(); // todo mb: das in main
	Init_TimerForPulsTime();
}

/*
 * Created on: 30.11.18
 * Author: MB
 * Diese Funktion arbeitet die queue ab.
 * Es besteht keine Priorit�t und es ist darauf zu achten, das de Prozessor ausreichend Zeit hat, die Information los zu werden.
 * */
void SendPulsePerCanTask(void * pvParameters) {
	/* The parameter value is expected to be 1 as 1 is passed in the  pvParameters value in the call to xTaskCreate() below.*/
	//configASSERT(((uint32_t ) pvParameters) == 1);
	//int i = 0;
	while (1) {
		//printf("run %d \r\n", ++i);
		static MessageForSend currentMessage;
		currentMessage.channel = 0;
		currentMessage.res = 1;
		/* Wait for the maximum period for data to become available on the queue.
		 The period will be indefinite if INCLUDE_vTaskSuspend is set to 1 in
		 FreeRTOSConfig.h. */
		if (xQueueReceive(PulsQueue, &currentMessage, 100) == pdTRUE) {
		//if (xQueueReceiveFromISR(PulsQueue, &currentMessage, (BaseType_t *)(SubPriorityPulseTimer+SubPriorityPulseTimer-1)) == pdTRUE) {
			//printf("Received %d %d \r\n", currentMessage.channel, currentMessage.res);
			SendCanTimeDif(currentMessage.channel, currentMessage.res);
		}
		else{
			vTaskDelay(10);
		}

	}

	printf("Sender task error \r\n");
}

/*
 * Created on: 30.11.18
 * Author: MB
 * Funktion zum initailiserne der Queue zum senden der Impulsinformation.

 * */

osThreadId myTask03Handle;
/*
 * Created on: 30.11.18
 * Author: MB
 * Diese Funktion initialisiert die Queue
 * Diese und startet den task um diese abzuarbeiten.
 * */
void InitPulseSender(void) {

	//  /* Create the thread(s) */
	//  /* definition and creation of defaultTask */
	osThreadDef(PulseTask, SendPulsePerCanTask, osPriorityAboveNormal, 0, 128);
	myTask03Handle = osThreadCreate(osThread(PulseTask), NULL);


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

	// printf("SendTimeInfo %d \r\n", channel);
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

	__HAL_TIM_CLEAR_FLAG(&pulseTimerInstance, TIM_FLAG_UPDATE);

	portENABLE_INTERRUPTS();
	//__enable_irq();
	//taskEXIT_CRITICAL_FROM_ISR( uxSavedInterruptStatus );

}
