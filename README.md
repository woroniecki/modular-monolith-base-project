# Modular monolith base project

This is base project with basic configuration to start work on developing without any extra configuration.

#### Features
- Angular frontend
- ASP.NET Core backend API
- Database integration
- Docker Compose setup for dev and prod
- SSL support in production
- CI/CD ready architecture

#### Tech Stack

- **Frontend:** Angular
- **Backend:** ASP.NET Core
- **Database:** SQL Server / PostgreSQL / MongoDB (choose accordingly)
- **Containerization:** Docker & Docker Compose
- **CI/CD:** [GitHub Actions / GitLab CI / other]
- **Hosting:** VPS with Nginx proxy & SSL

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
Needed enviroment variables:
- POSTGRES_USER
- POSTGRES_PASSWORD
- Npqsql__ConnectionString
- Cors__Origin
- APP_BASE_URL: 
- ASPNETCORE_ENVIRONMENT: Production

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
