/*
 * rtosActions.c
 *
 *  Created on: 04.12.2018
 *      Author: MB
 */

#include "rtosActions.h"


/* The semaphore (in this case binary) that is used by the FreeRTOS tick hook
 * function and the event semaphore task.
 */
static xSemaphoreHandle xEventSemaphore = NULL;

void vApplicationIdleHook(void) {
	volatile size_t xFreeStackSpace;

	/* The idle task hook is enabled by setting configUSE_IDLE_HOOK to 1 in
	 FreeRTOSConfig.h.

	 This function is called on each cycle of the idle task.  In this case it
	 does nothing useful, other than report the amount of FreeRTOS heap that
	 remains unallocated. */
	xFreeStackSpace = xPortGetFreeHeapSize();

	if (xFreeStackSpace > 100) {
		/* By now, the kernel has allocated everything it is going to, so
		 if there is a lot of heap remaining unallocated then
		 the value of configTOTAL_HEAP_SIZE in FreeRTOSConfig.h can be
		 reduced accordingly. */
	}
}

void vApplicationMallocFailedHook(void) {
	/* The malloc failed hook is enabled by setting
	 configUSE_MALLOC_FAILED_HOOK to 1 in FreeRTOSConfig.h.

	 Called if a call to pvPortMalloc() fails because there is insufficient
	 free memory available in the FreeRTOS heap.  pvPortMalloc() is called
	 internally by FreeRTOS API functions that create tasks, queues, software
	 timers, and semaphores.  The size of the FreeRTOS heap is set by the
	 configTOTAL_HEAP_SIZE configuration constant in FreeRTOSConfig.h. */
	for (;;)
		;
}
/*-----------------------------------------------------------*/

void vApplicationStackOverflowHook(xTaskHandle pxTask, signed char *pcTaskName) {
	(void) pcTaskName;
	(void) pxTask;

	/* Run time stack overflow checking is performed if
	 configconfigCHECK_FOR_STACK_OVERFLOW is defined to 1 or 2.  This hook
	 function is called if a stack overflow is detected.  pxCurrentTCB can be
	 inspected in the debugger if the task name passed into this function is
	 corrupt. */
	for (;;)
		;
}
/*-----------------------------------------------------------*/

void vApplicationTickHook(void) {
	portBASE_TYPE xHigherPriorityTaskWoken = pdFALSE;
	static uint32_t ulCount = 0;

	/* The RTOS tick hook function is enabled by setting configUSE_TICK_HOOK to
	 1 in FreeRTOSConfig.h.

	 "Give" the semaphore on every 500th tick interrupt. */
	ulCount++;
	if (ulCount >= 500UL) {
		/* This function is called from an interrupt context (the RTOS tick
		 interrupt),	so only ISR safe API functions can be used (those that end
		 in "FromISR()".

		 xHigherPriorityTaskWoken was initialised to pdFALSE, and will be set to
		 pdTRUE by xSemaphoreGiveFromISR() if giving the semaphore unblocked a
		 task that has equal or higher priority than the interrupted task.
		 http://www.freertos.org/a00124.html */
		xSemaphoreGiveFromISR(xEventSemaphore, &xHigherPriorityTaskWoken);
		ulCount = 0UL;
	}

	/* If xHigherPriorityTaskWoken is pdTRUE then a context switch should
	 normally be performed before leaving the interrupt (because during the
	 execution of the interrupt a task of equal or higher priority than the
	 running task was unblocked).  The syntax required to context switch from
	 an interrupt is port dependent, so check the documentation of the port you
	 are using.  http://www.freertos.org/a00090.html

	 In this case, the function is running in the context of the tick interrupt,
	 which will automatically check for the higher priority task to run anyway,
	 so no further action is required. */
}
/*-----------------------------------------------------------*/

