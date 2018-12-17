/*
 * pulse.c
 *
 *  Created on: 29.11.2018
 *      Author: MB
 */

#include "pulse.h"

#define QUEUE_SIZE_FOR_PULSE_INFO		( ( unsigned short ) 80 ) // 16 * 5
#define CHANNEL_COUNT 					( ( unsigned short ) 16 )

typedef struct {
	uint8_t channel;
	uint32_t res;
} MessageForSend;



static TIM_HandleTypeDef pulseTimerInstance; // = { .Instance = TIM2 };

static xQueueHandle PulsQueue = NULL;
uint32_t PreemptPriorityPulseTimer = 5;
uint32_t SubPriorityPulseTimer = 0;

osThreadId myTask03Handle;

static uint32_t TimerCorrectureFactor = 0;

void InitTimerPulseCorrecturFactor() {
	TimerCorrectureFactor = 0; // todo mb: load from eeprom
}

void SetTimerPulseCorrecturFactor(uint16_t value) {
	// Info, hier nur int 16 erlaubt, korrektur sonst viel zu gro�. Sollen wir den Bereich noch einschr�nken?
	TimerCorrectureFactor = (uint16_t) value;
	Reset();
}

uint32_t GetTimerPulseCorrecturFactor() {
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
	// f / fclock / Perios*(Pre+1))
	InitTimerPulseCorrecturFactor();

	__HAL_RCC_TIM3_CLK_ENABLE()
	;
	pulseTimerInstance.Instance = TIM3;
	pulseTimerInstance.Init.CounterMode = TIM_COUNTERMODE_UP;

	pulseTimerInstance.Init.Prescaler = 8;	//9;
	pulseTimerInstance.Init.Period = 800;	//801;// + GetTimerPulseCorrecturFactor();

	pulseTimerInstance.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
	pulseTimerInstance.Init.RepetitionCounter = 0;
	pulseTimerInstance.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
	//s_TimerInstance.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;//TIM_AUTORELOAD_PRELOAD_ENABLE;//
	if (HAL_TIM_Base_Init(&pulseTimerInstance) != HAL_OK) {
		//_Error_Handler(__FILE__, __LINE__);
	}

	HAL_TIM_Base_Start_IT(&pulseTimerInstance);

	HAL_NVIC_SetPriority(TIM3_IRQn, PreemptPriorityPulseTimer, SubPriorityPulseTimer); // extrem hohe priorit�t sollte es sein!!!
	HAL_NVIC_EnableIRQ(TIM3_IRQn);
}

//osMessageQId myQueue01Handle;

void InitQueueForPulse(void) {
	PulsQueue = xQueueCreate(QUEUE_SIZE_FOR_PULSE_INFO, sizeof(MessageForSend));
}


static ChannelModi ChannelModiStorage[CHANNEL_COUNT];


static ChannelModiType ActivatedChannelModi = Licht;
void SetActiveChannelModiType(ChannelModiType val){
	ActivatedChannelModi = val;
	// ins eeprom speichern, wenn eg�ndert
}

ChannelModiType GetActiveChannelModiType(void){
	// todo mb: aus eeprom laden bei initialisierung?
	return ActivatedChannelModi;
}

void ChangeChannelModi(uint8_t channel, ChannelModiType channelModiType){
	ChannelModiStorage[channel].channelModiType = channelModiType; // todo mb: das ist schei�e. hier kommt die Gefahr das man was durcheinader wirft
}

void InitChannelModi(void){
	// todo mb: von au�en initialisieren und in eeprom abspeichern
		// der channel wird extra abgespeichert und nicht �ber die Position im Array behandelt. �berscihtlicher!
		ChannelModiStorage[0].channelModiType = None;
		ChannelModiStorage[1].channelModiType = None;
		ChannelModiStorage[2].channelModiType = None;
		ChannelModiStorage[3].channelModiType = None;
		ChannelModiStorage[4].channelModiType = None;
		ChannelModiStorage[5].channelModiType = None;
		ChannelModiStorage[6].channelModiType = None;
		ChannelModiStorage[7].channelModiType = None;
		ChannelModiStorage[8].channelModiType = None;
		ChannelModiStorage[9].channelModiType = None;
		ChannelModiStorage[10].channelModiType = None;
		ChannelModiStorage[11].channelModiType = None;
		ChannelModiStorage[12].channelModiType = None;
		ChannelModiStorage[13].channelModiType = Licht;
		ChannelModiStorage[14].channelModiType = None;
		ChannelModiStorage[15].channelModiType = Licht;
}

void InitPulse(void) {
	InitQueueForPulse();
	InitPulseSender();
	Init_TimerForPulsTime();
	InitChannelModi();
}

/*
 * Created on: 30.11.18
 * Author: MB
 * Diese Funktion arbeitet die queue ab.
 * Es besteht keine Priorit�t und es ist darauf zu achten, das de Prozessor ausreichend Zeit hat, die Information los zu werden.
 * Es werden nur die eingesellten und passend konfigurierten Infos weg gesendet.
 * */
void SendPulsePerCanTask(void * pvParameters) {
	while (1) {
		static MessageForSend currentMessage;
		currentMessage.channel = 0;
		currentMessage.res = 1;

		if (xQueueReceive(PulsQueue, &currentMessage, 100) == pdTRUE) {

			for(int i=0; i < CHANNEL_COUNT; ++i){

				// voraussetzung, channel muss dem eingang entsprechen!!
				static ChannelModiType channelModi;
				channelModi = ChannelModiStorage[i].channelModiType;

				if(i == currentMessage.channel && channelModi == GetActiveChannelModiType()){
					SendCanTimeDif(currentMessage.channel, currentMessage.res);
					break; // schleife kan dann beendet werden
				}
			}

			// todo mb: flip flop einzeln behandeln
		}
	}

	printf("Sender task error \r\n");
}

/*
 * Created on: 30.11.18
 * Author: MB
 * Diese Funktion initialisiert die Queue
 * Diese und startet den task um diese abzuarbeiten.
 * */
void InitPulseSender(void) {

	//  /* Create the thread(s) */
	//  /* definition and creation of defaultTask */
	osThreadDef(PulseTask, SendPulsePerCanTask, osPriorityNormal, 0, 128);
	myTask03Handle = osThreadCreate(osThread(PulseTask), NULL);
	// todo mb: wann und wie den task deleten?
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
	static MessageForSend messageForSend;
	messageForSend.channel = channel;
	messageForSend.res = res;

	if (xQueueSendFromISR(PulsQueue, &messageForSend, 0) != pdTRUE) {
		SetPossiblePulseSendQueueFullError();
	}

	lastTimeValue[channel] = actualTimeValue;
}

void CheckPulseInputs(void) {
	static uint16_t oldValue = 0;
	static uint8_t data[2];
	static uint16_t value = 0;
	ReadInputs(&data[0]);
	value = data[0];
	value = value | (data[1] << 8);
	if (value != oldValue) {
		uint16_t dif = value ^ oldValue;
		for (int i = 0; i < 16; ++i) {
			if ((dif >> i) & 0x01 && (value >> i & 0x01)) {
				SendTimeInfo(i);
			}
		}
	}

	oldValue = value;
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

	++tickMs;
	CheckPulseInputs();
	__HAL_TIM_CLEAR_FLAG(&pulseTimerInstance, TIM_FLAG_UPDATE);

	portENABLE_INTERRUPTS();
}
