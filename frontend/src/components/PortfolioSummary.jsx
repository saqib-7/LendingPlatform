// Formats a number as a British pound amount for the dashboard.
function formatCurrency(value) {
  return new Intl.NumberFormat('en-GB', { style: 'currency', currency: 'GBP', maximumFractionDigits: 0 }).format(value || 0);
}

// Displays the portfolio metrics fetched from the backend, including its loading and error states.
export default function PortfolioSummary({ summary, isLoading, error, onRefresh }) {
  return (
    <section className="panel summary-panel">
      <div className="section-heading">
        <div>
          <p className="eyebrow">Portfolio dashboard</p>
          <h2>Portfolio summary</h2>
        </div>
        <button className="secondary-button" type="button" onClick={onRefresh} disabled={isLoading}>
          {isLoading ? 'Loading…' : 'Refresh'}
        </button>
      </div>

      {error && <p className="form-error" role="alert">{error}</p>}
      {isLoading && !summary && <p className="muted">Loading the latest portfolio data…</p>}
      {summary && (
        <div className="metrics" aria-live="polite">
          <article><span>Total applicants</span><strong>{summary.totalApplications}</strong></article>
          <article><span>Approved</span><strong className="success-number">{summary.acceptedApplications}</strong></article>
          <article><span>Declined</span><strong className="danger-number">{summary.declinedApplications}</strong></article>
          <article><span>Loans written</span><strong>{formatCurrency(summary.totalValueOfLoansWritten)}</strong></article>
          <article><span>Average LTV</span><strong>{summary.meanLoanToValuePercent == null ? '—' : `${Number(summary.meanLoanToValuePercent).toFixed(2)}%`}</strong></article>
        </div>
      )}
    </section>
  );
}
