# Modular monolith base project

This is base project with basic configuration to start work on developing without any extra configuration with DDD approach and easily portable to microservices.

#### Features
- Angular frontend
- ASP.NET Core backend API
- Database integration
- Docker Compose setup for dev and prod
- SSL support in production
- CI/CD ready architecture

#### Tech Stack

- **Frontend:** Angular 19
- **Backend:** .NET 9, ASP.NET Core
- **Database:** PostgreSQL
- **Containerization:** Docker & Docker Compose
- **Hosting:** Caddy reverse proxy

## Getting Started

Go to ./Env and run project locally

```powershell
docker compose -f docker-compose-local.yml build
docker compose -f docker-compose-local.yml up -d
```
## Run on production

Use ./Env/docker-compose-prod.yml to run your project on production
Keep in mind, that in production docker compose background task project is cutted off,
if it's needed in project you should add it manually, base on docker-compose-local.yml

Prepare CaddyFile ./Frontend/frontend-spa-app/Caddyfile:
replace localhost with proper url for reverse proxy

Needed enviroment variables:
- POSTGRES_USER
- POSTGRES_PASSWORD 
- Npqsql__ConnectionString (Database connection string)
- Cors__Origin
- APP_BASE_URL (Base url for frontend to call)
- ASPNETCORE_ENVIRONMENT: Production
- JwtSettings__SecretKey (keep it safe)
- JwtSettings__ExpirationInMinutes (how long token is valid)

## Project description

#### Structur

Here is a short summary of the project.

Backend  
&emsp;BackgroundTasks - coravel console app to run background tasks    
&emsp;Bootstrapper - api project entry point
&emsp;Modules - separated modules by domains  
&emsp;&emsp;Core - main project module to start with
&emsp;&emsp;UserManagement - resposnsible for user, account, user account actions  
&emsp;SharedUtils  
&emsp;&emsp;SharedUtils  
&emsp;&emsp;SharedUtils.Domain  
&emsp;Tests  
&emsp;&emsp;Core.Tests  
&emsp;&emsp;UserManagement.Tests  

Frontend  
&emsp;todo

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
