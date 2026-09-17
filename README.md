# Lending Platform

A small full-stack lending decision application. It lets a user submit a secured loan application, receives a decision from the API, and shows a live portfolio summary.

The project is intentionally simple: the backend owns the lending rules and data, while the React frontend focuses on a clear, responsive interface.

## What it does

- Submit a loan amount, asset value, and credit score.
- Get an approved or declined decision with an explanation.
- Save applications locally in SQLite.
- View portfolio totals, including approved/declined counts, loans written, and average LTV.

## Tech stack

- **Backend:** ASP.NET Core Web API, Entity Framework Core, SQLite
- **Frontend:** React, Vite, plain CSS
- **Tests:** xUnit

## Requirements

- .NET 10 SDK
- Node.js and npm

## Running the project

Start the API first:

```bash
cd backend
dotnet run
```

The API starts at `http://localhost:5193`. On its first run, it creates a local SQLite database automatically.

In a second terminal, start the frontend:

```bash
cd frontend
npm install
npm run dev
```

Open the address printed by Vite, normally `http://localhost:5173`.

The frontend calls the API through the base URL in `frontend/.env`. CORS is configured in the backend for the local Vite development server.

## API endpoints

| Method | Endpoint | Purpose |
| --- | --- | --- |
| `POST` | `/api/loan-applications` | Submit and assess an application |
| `GET` | `/api/loan-applications` | List submitted applications |
| `GET` | `/api/loan-applications/{id}` | Get one application |
| `GET` | `/api/loan-applications/summary` | Get portfolio metrics |

## Run the tests

```bash
dotnet test backend.Tests/backend.Tests.csproj
```

## What I would improve for production

Before treating this as an industry-ready lending system, I would add authentication and role-based access, stronger input validation and error handling, database migrations, audit logging, monitoring, rate limiting, and a proper CI/CD pipeline. I would also move secrets and environment settings out of local files, add broader API and frontend tests, and review the lending rules with the relevant compliance and risk teams.

## AI assistance

AI was used as a development companion while building this project. I used it for ideas, troubleshooting, and help writing some functions; the implementation was reviewed and integrated into the project as part of the development process.
