FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["SessionReservation/src/SessionReservation.Api/SessionReservation.Api.csproj", "SessionReservation.Api/"]
COPY ["SessionReservation/src/SessionReservation.Application/SessionReservation.Application.csproj", "SessionReservation.Application/"]
COPY ["SessionReservation/src/SessionReservation.Domain/SessionReservation.Domain.csproj", "SessionReservation.Domain/"]
COPY ["SessionReservation/src/SessionReservation.Contracts/SessionReservation.Contracts.csproj", "SessionReservation.Contracts/"]
COPY ["SessionReservation/src/SessionReservation.Infrastructure/SessionReservation.Infrastructure.csproj", "SessionReservation.Infrastructure/"]
COPY ["SharedKernel/SharedKernel.csproj", "/SharedKernel/"]
RUN dotnet restore "SessionReservation.Api/SessionReservation.Api.csproj"
COPY ["SessionReservation/src", "/src/"]
COPY ["SharedKernel", "/SharedKernel/"]
WORKDIR "/src/SessionReservation.Api"
RUN dotnet build "SessionReservation.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SessionReservation.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["SessionReservation/scripts/*.sh", "."]
COPY ["scripts/*.sh", "."]
RUN chmod +x *.sh

ENTRYPOINT ["/app/entrypoint.sh"]

ENV ASPNETCORE_URLS=http://*:80
CMD ["dotnet", "SessionReservation.Api.dll"]