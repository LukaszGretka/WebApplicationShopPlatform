# Web Application Shop Platform

Backend template for efficient creating online shopping platforms.
Powered by .NET Core 3.1

# Installation

What you need?

1. Docker on local or remote machine (I used Docker on WSL Ubuntu 20.06 subsystem on Windows 10) <br/>
How to install Linux subsystem on Win 10: https://docs.microsoft.com/en-us/windows/wsl/install-win10 <br>
How to install and configurate docker on Linux machine: https://docs.docker.com/engine/install/ubuntu/

2. Pull Postgres Docker's image 9.6.21 <br/>
``` sudo docker pull postgres:9.6.21-alpine ```

3. Start Postgres database <br/> 
``` sudo docker run -p 5432:5432 -e POSTGRES_PASSWORD=db-password postgres:9.6.21-alpine ```

4. Check if database accept connection. <br/> 
Type in Windows shell (for example PowerShell) <br/>
``` wsl hostname -I ``` <br/>
to get WSL IP address. By using database client (for example pgAdmin), try to connect to database. If db accept the connection, replace IP address in connection string, which is located in appseting.json<br/>