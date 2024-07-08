#include <stdio.h>
#include <stdlib.h>
#include <sys/poll.h>
#include <string.h>
#include <strings.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <time.h>
#include <unistd.h>

#include "network_common.h"

#define MAX_CLIENTS 100

int main(int argc, char *argv[]) {
    struct sockaddr_in stCAddr;
    socklen_t nCAddr = sizeof(struct sockaddr_in);

    struct sockaddr_in stAddrs[MAX_CLIENTS + 1];
    struct pollfd rfds[MAX_CLIENTS + 1];
    int nTimeout = 0;
    int nRetval;

    char strBuffer[BUFSIZ];
    int nBufferLen = 0;

    for (int i = 0; i < MAX_CLIENTS + 1; i++) {
        rfds[i].fd = -1;
        rfds[i].events = 0;
        rfds[i].revents = 0;

        bzero(&stAddrs[i], sizeof(struct sockaddr_in));
    }

    if (argc != 2) {
        printf("usage: %s <port>\n", argv[0]);
        return -1;
    }

    printf("Port %s\n", argv[1]);

    rfds[0].fd = start_tcp_server(atoi(argv[1]), MAX_CLIENTS);
    if (rfds[0].fd < 0) {
        printf("Starting Server is failed.\n");
        return -1;
    }
    rfds[0].events = POLLIN;
    rfds[0].revents = 0;

    nTimeout = 1000;
    do {
        nRetval = poll(rfds, MAX_CLIENTS + 1, nTimeout);
        if (nRetval > 0) {
            for (int i = 0; i < MAX_CLIENTS + 1; i++) {
                if (rfds[i].fd < 0) continue;
                if (rfds[i].revents & POLLIN) {
                    if (i == 0) {
                        printf("Connecting...\n");
                        int nFd = accept(rfds[i].fd, (struct sockaddr *)&stCAddr, &nCAddr);  // 접속
                        if (nFd > 0) {
                            if (nCAddr > 0) {
                                char strBuffer[BUFSIZ];
                                bzero(strBuffer, BUFSIZ);
                                inet_ntop(AF_INET, &stCAddr.sin_addr, strBuffer, INET_ADDRSTRLEN);
                                printf("New connection with %s\n", strBuffer);
                            }

                            // 빈자리를 찾아서 그곳에 추가
                            for (int i = 1; i < MAX_CLIENTS + 1; i++) {
                                if (rfds[i].fd < 0) {
                                    rfds[i].fd = nFd;
                                    rfds[i].events = POLLIN;
                                    rfds[i].revents = 0;

                                    bcopy(&stCAddr, &stAddrs[i], sizeof(struct sockaddr));

                                    bzero(strBuffer, BUFSIZ);
                                    // JSON 형태로 클라이언트에게 전달
                                    nBufferLen = sprintf(strBuffer, "{\"fd\": %d, \"body\": \"Welcome!\r\n\"}", rfds[i].fd);
                                    send(rfds[i].fd, strBuffer, nBufferLen, 0);

                                    bzero(strBuffer, BUFSIZ);
                                    /*nBufferLen = sprintf(strBuffer, "Your Id is (%d)[%s:%d]\r\n",
                                                         rfds[i].fd,
                                                         inet_ntoa(stAddrs[i].sin_addr),
                                                         ntohs(stAddrs[i].sin_port));

                                    send(rfds[i].fd, strBuffer, nBufferLen, 0);
                                    */
                                    break;
                                }
                            }
                        }
                    }
                    else {
                        bzero(strBuffer, BUFSIZ);
                        nBufferLen = recv(rfds[i].fd, strBuffer, BUFSIZ, 0);

                        char *nickname = strtok(strBuffer, "|");  // |를 기준으로 닉네임을 가져옴
                        char *content = strtok(NULL, "|");  // |를 기준으로 대화 내용을 가져옴

                        if (nBufferLen <= 0) {
                            printf("A Client(%d, %s:%d) is disconneted.\n",
                                rfds[i].fd,
                                inet_ntoa(stAddrs[i].sin_addr),
                                ntohs(stAddrs[i].sin_port));
                            close(rfds[i].fd);
                            rfds[i].fd = -1;
                            rfds[i].events = 0;
                            rfds[i].revents = 0;
                        }
                        else {
                            char strBuffer2[BUFSIZ];
                            int nBufferLen2 = 0;
                            bzero(strBuffer2, BUFSIZ);
                            // JSON 형태로 클라이언트에게 여러 정보를 전달
                            nBufferLen2 = sprintf(strBuffer2
                                , "{\"fd\": %d, \"nickname\": \"%s\",  \"ipAddr\": \"%s\", \"port:\": %d, \"body\": \"%s\"}",
                                                    rfds[i].fd,
                                                    nickname,  // 닉네임 추가
                                                    inet_ntoa(stAddrs[i].sin_addr),
                                                    ntohs(stAddrs[i].sin_port),
                                                    content);
                            printf("%s\n", strBuffer2);
                            for (int j = 1; j < MAX_CLIENTS + 1; j++) {
                                if (rfds[j].fd < 0) continue;
                                send(rfds[j].fd, strBuffer2, nBufferLen2, 0);
                            }
                        }
                    }
                }
            }
        }
        else if (nRetval < 0) {
            break;
        }
    } while(1);

    for (int i = 0; i < MAX_CLIENTS + 2; i++) {
        if (rfds[i].fd > 0) close(rfds[i].fd);
    }

    printf("Bye~\n");

    return 0;
}