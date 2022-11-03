# Library

## Setup

Prerequisite:

- [.Net 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [NodeJS](https://nodejs.org/en/) or [nvm](https://github.com/nvm-sh/nvm)
- Install `dotnet-ef`:

```
dotnet tool install dotnet-ef
```

- Set connection string (it has to be an absolute path):

```
dotnet user-secrets -p src/Library.Api set "Database:ConnectionString" "Data Source=/Users/<Path to repository>/Library/data/Library.db"
```

## Run step

### Generate database

```
dotnet ef database update -p src/Library.Api
```

### Run Api

```
dotnet run --project src/Library.Api
```

Swagger will be available [here](http://localhost:5289/swagger/index.html).

### Run Web

```
dotnet run --project src/Library.Web
```

SPA proxy will be available here [here](http://localhost:5202/).
