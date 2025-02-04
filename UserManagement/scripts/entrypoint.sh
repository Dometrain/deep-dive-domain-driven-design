#!/bin/bash
set -e

# Wait for RabbitMQ
./wait-for-it.sh rabbitmq:5672 --timeout=30
echo "RabbitMQ is up and running!. Starting User Management service"

exec "$@"