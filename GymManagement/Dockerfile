FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["GymManagement/src/GymManagement.Api/GymManagement.Api.csproj", "GymManagement.Api/"]
COPY ["GymManagement/src/GymManagement.Application/GymManagement.Application.csproj", "GymManagement.Application/"]
COPY ["GymManagement/src/GymManagement.Domain/GymManagement.Domain.csproj", "GymManagement.Domain/"]
COPY ["GymManagement/src/GymManagement.Contracts/GymManagement.Contracts.csproj", "GymManagement.Contracts/"]
COPY ["GymManagement/src/GymManagement.Infrastructure/GymManagement.Infrastructure.csproj", "GymManagement.Infrastructure/"]
COPY ["SharedKernel/SharedKernel.csproj", "/SharedKernel/"]
RUN dotnet restore "GymManagement.Api/GymManagement.Api.csproj"
COPY ["GymManagement/src", "/src/"]
COPY ["SharedKernel", "/SharedKernel"]
WORKDIR /src/GymManagement.Api
RUN dotnet build "GymManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GymManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["GymManagement/scripts/*.sh", "."]
COPY ["scripts/*.sh", "."]
RUN chmod +x *.sh

ENTRYPOINT ["/app/entrypoint.sh"]

ENV ASPNETCORE_URLS=http://*:80
CMD ["dotnet", "GymManagement.Api.dll"]