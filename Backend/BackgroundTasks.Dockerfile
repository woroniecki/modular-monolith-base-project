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
RUN dotnet restore "BackgroundTasks/BackgroundTasks.csproj"  

# Copying the entire source code  
COPY . .  

# Building the project  
WORKDIR "/src/BackgroundTasks"  
RUN dotnet build "BackgroundTasks.csproj" -c $BUILD_CONFIGURATION -o /app/build  

# Publish stage  
FROM build AS publish  
ARG BUILD_CONFIGURATION=Release  
RUN dotnet publish "BackgroundTasks.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false  

# Final image  
FROM base AS final  
WORKDIR /app  
COPY --from=publish /app/publish .  
ENTRYPOINT ["dotnet", "BackgroundTasks.dll"]
