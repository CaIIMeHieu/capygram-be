#!/bin/bash
echo "-------------------------Remove container--------------------------------"
sudo docker compose -f .\docker-compose.Dev.Infrastructure.yaml down
echo "-------------------------Rebuild image--------------------------------"
sudo docker-compose -f .\docker-compose.Dev.Infrastructure.yaml build --no-cache
echo "-------------------------Run container--------------------------------"
sudo docker compose -f .\docker-compose.Dev.Infrastructure.yaml up -d