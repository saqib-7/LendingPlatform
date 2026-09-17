const baseUrl = import.meta.env.VITE_API_BASE_URL;

// Turns an unsuccessful API response into a helpful error for the page to display.
async function throwIfNotOk(response) {
  if (response.ok) return;

  let message = 'The request could not be completed.';
  try {
    const errorBody = await response.json();
    message = errorBody.title || errorBody.detail || message;
  } catch {
    // Keep the friendly fallback message when the server did not return JSON.
  }
  throw new Error(message);
}

// Sends a new loan application to the real backend and returns its decision.
export async function createLoanApplication(application) {
  const response = await fetch(`${baseUrl}/api/loan-applications`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(application),
  });

  await throwIfNotOk(response);
  return response.json();
}

// Gets the current portfolio totals from the real backend.
export async function getPortfolioSummary() {
  const response = await fetch(`${baseUrl}/api/loan-applications/summary`);
  await throwIfNotOk(response);
  return response.json();
}
