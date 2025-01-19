# Team1 - Recipes

The app is available at https://uthrecipesweb2025team1.azurewebsites.net/

### Prerequisites

1. Clone the Uth.Recipes repository: https://github.com/GeorgeVovos/UTH-SE/tree/main/Exercises/04-Recipes
2. Build and Run         
2.1 [Install & start Docker Desktop](https://docs.docker.com/engine/install/)   
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;or   
2.2.1. [Install the .NET 9 SDK (Windows/Mac/Linux)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)   
2.2.2. Have an instance of "MySql" Or "SQL Server" running

### Build and Run

**1.Using Docker**    

On the top level directory, run `docker compose up`   
Navigate to `http://localhost:8085/`   
When finished, run `docker compose down`    
*Troubleshooting*:   
- Docker compose will start new MySql container on port 3309.   
  If this port is used on your machine,
replace all references to "3309" in the repo to another port (in *docker-compose.yml* and *App.config*)
- The recipes website will run on port 8085.    
  If this port is used on your machine, replace all references to "8085" in the repo to another port (in *docker-compose.yml* )

**2.Using the .NET 9 SDK**   
- <span style="color:red">Update the "DatabaseType" and "ConnectionString" keys in Uth.Recipes.Web\App.Config (The file contains instructions)</span>
- On the top level directory, run `dotnet build Uth.Recipes.sln`   
- On the top level directory, run `dotnet run --project ./Uth.Recipes.Web/Uth.Recipes.Web.csproj`   
- Navigate to `http://localhost:5230`   
*Troubleshooting*: If port 5230 is used, update *Uth.Recipes.Web\Properties\launchSettings.json** and try the above steps again
