# Web Application Shop Platform

Backend template for efficient creating online shopping platforms.
Powered by .NET Core 3.1

# Installation

What you need?

1. Docker on local or remote machine (I used Docker on WSL Ubuntu 20.06 subsystem on Windows 10) 
How to install Linux subsystem on Win 10: https://docs.microsoft.com/en-us/windows/wsl/install-win10
How to install and configurate docker on Linux machine: https://docs.docker.com/engine/install/ubuntu/

2. Pull Postgres Docker's image 9.6.21 
``` sudo docker pull postgres:9.6.21 ```

3. Start Postgres database <br/> 
``` sudo docker run -p 127.0.0.1:5432:5432 -e POSTGRES_PASSWORD=db-password postgres:9.6.21 ```

4. Check if database accept connection.
By using database client (for example pgAdmin https://www.pgadmin.org/download/), try to connect to database by using following credentials:
IP = 127.0.0.1
port = 5432
username: postgres
password: db-password

5. Migrate database
Use
``` update-database ``` 
command to perform database migration from Package Manager Console in Visual Studio or simply open any shell at project folder and then by using command
``` dotnet ef database update```
update your database to the latest migration version (for this case EF Core command-line tool has to be installed https://docs.microsoft.com/pl-pl/ef/core/cli/dotnet)