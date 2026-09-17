import { useEffect, useState } from 'react';
import { createLoanApplication, getPortfolioSummary } from './api/loanApi';
import DecisionResult from './components/DecisionResult';
import LoanApplicationForm from './components/LoanApplicationForm';
import PortfolioSummary from './components/PortfolioSummary';

// Provides the page layout and keeps the form decision and portfolio data in one place.
export default function App() {
  const [decision, setDecision] = useState(null);
  const [formError, setFormError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [summary, setSummary] = useState(null);
  const [summaryError, setSummaryError] = useState('');
  const [isSummaryLoading, setIsSummaryLoading] = useState(true);

  // Fetches summary data and shows a friendly message if the backend is unavailable.
  async function loadSummary() {
    setIsSummaryLoading(true);
    setSummaryError('');
    try {
      setSummary(await getPortfolioSummary());
    } catch {
      setSummaryError('Could not load the portfolio. Check that the backend is running.');
    } finally {
      setIsSummaryLoading(false);
    }
  }

  // Sends the form data to the backend and refreshes the dashboard after a successful submission.
  async function handleApplicationSubmit(application) {
    setIsSubmitting(true);
    setFormError('');
    setDecision(null);
    try {
      setDecision(await createLoanApplication(application));
      await loadSummary();
    } catch {
      setFormError('Could not submit the application. Check that the backend is running and try again.');
    } finally {
      setIsSubmitting(false);
    }
  }

  // Loads the existing portfolio when the page first opens.
  useEffect(() => {
    loadSummary();
  }, []);

  return (
    <main>
      <header className="hero">
        <p className="brand">LENDING PLATFORM</p>
        <h1>Simple, informed lending decisions.</h1>
        <p>Submit an application and see a live view of your lending portfolio.</p>
      </header>
      <div className="content-grid">
        <div>
          <LoanApplicationForm onSubmit={handleApplicationSubmit} isSubmitting={isSubmitting} />
          {formError && <p className="page-error" role="alert">{formError}</p>}
          <DecisionResult decision={decision} />
        </div>
        <PortfolioSummary summary={summary} isLoading={isSummaryLoading} error={summaryError} onRefresh={loadSummary} />
      </div>
    </main>
  );
}
