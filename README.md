# ToDoApp

Cross-platform ToDo list application with a C# .NET backend and React frontend.

## Components

- **backend**: ASP.NET Core Web API storing user-specific todos in memory.
- **frontend**: React client using Keycloak for authentication.
- **Keycloak**: OAuth2 provider configured via Docker.

## Running

1. Start services with Docker Compose:

   ```bash
   docker compose up --build
   ```

   This launches Keycloak on [http://localhost:8080](http://localhost:8080), the API on [http://localhost:5000](http://localhost:5000) and the React app on [http://localhost:3000](http://localhost:3000).

2. In Keycloak, create a realm named `todo` with clients `todo-api` (confidential) and `todo-frontend` (public). Users log in via Keycloak and each maintains a separate list.

## Development

The backend requires .NET 8 SDK, and the frontend uses Node 20 with Vite. Both run on Mac or Linux.
