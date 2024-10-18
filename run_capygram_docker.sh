#!/bin/bash
echo "-------------------------Remove container--------------------------------"
echo $SUDO_PASSWORD | sudo -S docker compose -f ./docker-compose.Dev.Infrastructure.yaml down
echo "-------------------------Rebuild image--------------------------------"
echo $SUDO_PASSWORD | sudo -S docker-compose -f ./docker-compose.Dev.Infrastructure.yaml build --no-cache
echo "-------------------------Run container--------------------------------"
echo $SUDO_PASSWORD | sudo -S docker compose -f ./docker-compose.Dev.Infrastructure.yaml up -d