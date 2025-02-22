FROM mcr.microsoft.com/dotnet/sdk:8.0:alpine
WORKDIR /app

RUN apk add --no-cache nano

COPY backend.csproj .
RUN dotnet restore backend.csproj

COPY . .
RUN dotnet build backend.csproj

EXPOSE 5000

CMD ["dotnet", "run"]