# CandidateBoard
The project contain 2 part: API and UI
[Database](#Database)
[API](#API)
[UI](#UI)

Database
========
In this project, I use MySQL. 
To setup My SQL, you can follow two ways to setup:
1. Install My SQL to your PC. You can download from this URL: 
    https://dev.mysql.com/downloads/installer/
2. Setup Docker Desktop and install My SQL
    - You can download Docker Descktop form this URL:
        https://www.docker.com/products/docker-desktop/
    - Then you can follow this URL to setup My SQL to docker: 
        https://www.datacamp.com/tutorial/set-up-and-configure-mysql-in-docker

API
========
This guide will show you how to set up API
There are two ways to run the API:
1. Run directly when your PC already have Visual Studio
    - First, you have to Restore Nuget Package (all of Nuget can be found in nuget.org page)
    - Then, please edit the ConnectionString in appsettings.json file to the server that you install the My Sql Server (if you install it in your local PC, it should be localhost)
    - Then you can fress F5 to run the project
    - If the API show the Swagger UI, it meen your setup is for API success
2. Deploy it to IIS
    - Setup IIS in your PC: https://community.lansweeper.com/t5/installation/install-iis-internet-information-services/ta-p/64422
    - You need to download .net core 6 bundle. You can download and install in this link https://learn.microsoft.com/vi-vn/aspnet/core/host-and-deploy/iis/hosting-bundle?view=aspnetcore-6.0
    - Go to src/CandidateBoard/src/API project, run dotnet build


