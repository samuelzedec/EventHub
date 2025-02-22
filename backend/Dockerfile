FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine
WORKDIR /app
RUN apk add --no-cache nano
RUN apk add --no-cache bash

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY backend.csproj .
RUN dotnet restore backend.csproj

COPY start.sh .
RUN chmod 755 /app/start.sh

COPY . .
RUN dotnet build backend.csproj

EXPOSE 5000

CMD ["/bin/bash", "./start.sh"]