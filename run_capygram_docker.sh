#!/bin/bash
echo "-------------------------Remove container--------------------------------"
echo $SUDO_PASSWORD | sudo -S docker compose -f ./docker-compose.Dev.Infrastructure.yaml down
echo "-------------------------Run container and rebuild image--------------------------------"
echo $SUDO_PASSWORD | sudo -S docker compose -f ./docker-compose.Dev.Infrastructure.yaml up -d --build