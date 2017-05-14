#include <stdio.h>
#include <unistd.h>
#include <pthread.h>
#include <stdlib.h>
#define size 10

int arr[size];
int incr = size/2;
pthread_t thread[size];



void arr_initiate() {
    int i;
    srand(time(NULL));
    for(i = 0; i < size; i++)
        arr[i] = rand() % size;
    
    printf("Original array\n");
    for(i = 0; i < size; i++)
        printf("%d ", arr[i]);
    printf("\n");
}

/*void ShellSort(int incr)
{
        for(int i=incr+1; i<size; i++)
        {
            j = i-incr;
            while(j>0){
                if(arr[j]>arr[j+incr]){
                    int tmp;
                    tmp = arr[i];
                    arr[i] = arr[i+1];
                    arr[i+1] = tmp;
                    j=j-incr;
                }
                else
                    j=0;
            }
            
        }
}*/


void shellsort()
{
    int j, k, tmp;
    //for (i = num / 2; i > 0; i = i / 2)
    //{
        for (j = incr; j < size; j++)
        {
            for(k = j - incr; k >= 0; k = k - incr)
            {
                if (arr[k+incr] >= arr[k])
                    break;
                else
                {
                    tmp = arr[k];
                    arr[k] = arr[k+incr];
                    arr[k+incr] = tmp;
                }
            }
        }
}

int main() {
    int i;
    arr_initiate();
    int threadCount=3;

    while(incr>0)
    {
     
        for(i = 0; i < threadCount; i++){
            if(incr>0){
            pthread_create(&thread[i], NULL, &shellsort, NULL);
            incr=incr/2;
            }
            else
                break;
            
            for(i = 0; i < threadCount; i++)
                pthread_join(thread[i], NULL);
        }
        
     
     }
    
    
    
    
    printf("Sorted array\n");
    for(i = 0; i < size; i++)
        printf("%d ", arr[i]);
    printf("\n");
    
    exit(0);
}
