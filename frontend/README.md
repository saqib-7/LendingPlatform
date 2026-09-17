# Lending Platform frontend

This is a small React website for submitting loan applications and viewing the portfolio summary. It talks to the real ASP.NET Core API; it does not contain any lending decision rules or mock data.

## Run the frontend

1. Open a terminal in the `frontend` folder.
2. Install the React and Vite packages once:

   ```bash
   npm install
   ```

3. Start the website:

   ```bash
   npm run dev
   ```

4. Open the local address Vite prints in the terminal (normally `http://localhost:5173`).

## Run both parts together

1. In one terminal, open `backend` and run `dotnet run`. The API runs at `http://localhost:5193`.
2. In a second terminal, open `frontend` and run `npm run dev`.
3. Open `http://localhost:5173` in your browser.

The backend must be running before the form and dashboard can work.

## How the two parts communicate

The backend address is in `.env` as `VITE_API_BASE_URL`. Change that value if the API uses a different address. `src/api/loanApi.js` is the only file that makes HTTP requests:

- `POST /api/loan-applications` submits the form.
- `GET /api/loan-applications/summary` gets the dashboard numbers.

`backend/Program.cs` now includes a CORS policy allowing `http://localhost:5173`. This is necessary because the React site and API run on different local ports during development, and browsers otherwise block the requests.
