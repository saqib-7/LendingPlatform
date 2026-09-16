# Lending Platform API

This ASP.NET Core API receives a secured loan application, makes the lending decision from the candidate-test rules, saves it locally, and exposes portfolio metrics.

## Run it

1. Open a terminal in this `backend` folder.
2. Run `dotnet restore` once to download the SQLite provider.
3. Run `dotnet run`.

The application creates `lending-platform.db` automatically beside the project. It stores submitted applications so the summary survives a restart.

## Endpoints

| Method | Path | Purpose |
| --- | --- | --- |
| POST | `/api/loan-applications` | Submit and assess a loan application. |
| GET | `/api/loan-applications` | List all submitted applications. |
| GET | `/api/loan-applications/{id}` | Get one application. |
| GET | `/api/loan-applications/summary` | Get applicant counts, loans written, and mean LTV. |

### Example request

```json
POST /api/loan-applications
{
  "loanAmount": 500000,
  "assetValue": 800000,
  "creditScore": 850
}
```

This is accepted: its LTV is 62.5%, which is below 80%, and its credit score is at least 800.

## Business-rule interpretation

- GBP 100,000 and GBP 1,500,000 are allowed; values outside that inclusive range are declined.
- GBP 1,000,000 is the high-value bracket: LTV `<= 60%` and credit score `>= 950`.
- Below GBP 1,000,000: LTV `< 60%` needs `>= 750`; LTV from `60%` to `< 80%` needs `>= 800`; LTV from `80%` to `< 90%` needs `>= 900`; LTV `>= 90%` is declined.
- “Loans written” is the sum of accepted loan amounts. Mean LTV includes every submitted application.

## Project guide

- `Contracts/` has the JSON shapes accepted and returned by the API.
- `Controllers/` translates HTTP requests into application actions.
- `Services/LoanDecisionService.cs` contains only lending rules, so it is easy to test independently.
- `Domain/` has business entities and the decision enum.
- `Data/` has database configuration.
- `Program.cs` wires everything together on startup.
