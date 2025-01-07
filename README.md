# GameProifle
GameProfile is a web service designed for managing your game collection, forum and providing mini analytics to enhance your gaming experience.
## Techical Stack
* Asp.net 7
* Angular
* CI/CD Github Actions
* MS SQL
* Redis
* Docker
* Nginx
## Architecture 
The entry point for the application is a request to the server via **Nginx**, which serves the **Angular** application. Through reverse proxy, the request is routed to the Docker container environment that hosts the following services:
- **Redis**
- **MSSQL**
- **SteamCMD**
- **ASP.NET**
All requests are handled by the **ASP.NET server**, which in turn communicates with the other applications.
![](/Screenshots/Scheme.png)
* Infrastructure Layer: Handles external APIs 
* Persistence Layer: Manages database interactions, migrations, and caching
* Business Logic Layer: Contains core application rules and entities (e.g., Forum, Game, Profile).
* Application Layer: Facilitates data transfer and DTO creation
* Wep API Layer: Exposes API endpoints for client interaction
![](/Screenshots/Layers.png)
## ERD
![](/Screenshots/ERD.png)
## Tests
Tests were conducted on the API using Insomnia and Swagger. 
![](/Screenshots/Insomnia.png)
## Screenshots
![](/Screenshots/Catalog.png)
![](/Screenshots/Game.png)
![](/Screenshots/Stats.png)