FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base  
WORKDIR /app  
EXPOSE 80  
EXPOSE 443  

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build  
ARG BUILD_CONFIGURATION=Development  
WORKDIR /src  

# Copying all project files  
COPY . .  

# Restoring dependencies  
RUN pwd  
RUN dotnet restore "Bootstrapper/Bootstrapper.csproj"  

# Copying all source code  
COPY . .  

# Building the project  
WORKDIR "/src/Bootstrapper"  
RUN dotnet build "Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build  

# Publish stage  
FROM build AS publish  
ARG BUILD_CONFIGURATION=Release  
RUN dotnet publish "Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false  

# Final image  
FROM base AS final  
WORKDIR /app  
COPY --from=publish /app/publish .  
ENTRYPOINT ["dotnet", "Bootstrapper.dll"]
