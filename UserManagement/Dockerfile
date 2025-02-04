FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["UserManagement/src/UserManagement.Api/UserManagement.Api.csproj", "UserManagement.Api/"]
COPY ["UserManagement/src/UserManagement.Application/UserManagement.Application.csproj", "UserManagement.Application/"]
COPY ["UserManagement/src/UserManagement.Domain/UserManagement.Domain.csproj", "UserManagement.Domain/"]
COPY ["UserManagement/src/UserManagement.Contracts/UserManagement.Contracts.csproj", "UserManagement.Contracts/"]
COPY ["UserManagement/src/UserManagement.Infrastructure/UserManagement.Infrastructure.csproj", "UserManagement.Infrastructure/"]
COPY ["SharedKernel/SharedKernel.csproj", "/SharedKernel/"]
RUN dotnet restore "UserManagement.Api/UserManagement.Api.csproj"
COPY ["UserManagement/src", "/src/"]
COPY ["SharedKernel", "/SharedKernel/"]
WORKDIR "/src/UserManagement.Api"
RUN dotnet build "UserManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["UserManagement/scripts/*.sh", "."]
COPY ["scripts/*.sh", "."]
RUN chmod +x *.sh

ENTRYPOINT ["/app/entrypoint.sh"]

ENV ASPNETCORE_URLS=http://*:80
CMD ["dotnet", "UserManagement.Api.dll"]