# Game Tracker

Game Tracker is a fullstack .NET/Blazor/PostgreSQL webapp to keep a running list of video games to play or games you have played.


## Build Instructions


#### Setup the DB

* Install [PostgreSQL](https://www.postgresql.org/download/) on your system (or download [PgAdmin](https://www.postgresql.org/download/) for a database GUI tool).
* Create a new database. The `appsettings.json` file will default to look for a `game_tracker` database.
* Run the SQL script `/Scripts/create_schema.sql`

#### Run the Application in Visual Studio

* Add the database credentials to the `appsettings.json` file
```
"gameTrackerDb": "Server=localhost;Database=game_tracker;User Id=postgres;Password=;"
```
* Run as Server or in IIS

This will default to running the app on the configured port in HTTPS.
