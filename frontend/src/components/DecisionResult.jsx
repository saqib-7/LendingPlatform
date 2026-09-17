// Shows the lending decision returned by the backend after an application is submitted.
export default function DecisionResult({ decision }) {
  if (!decision) return null;

  // The current .NET API serializes Accepted as 0 and Declined as 1.
  const approved = decision.decision === 0 || decision.decision === 'Accepted';
  const label = approved ? 'Approved' : 'Declined';

  return (
    <section className={`decision-result ${approved ? 'approved' : 'declined'}`} aria-live="polite">
      <p className="eyebrow">Application decision</p>
      <h2>{label}</h2>
      <p>{decision.decisionReason}</p>
      <p className="ltv">Loan-to-value: {Number(decision.loanToValuePercent).toFixed(2)}%</p>
    </section>
  );
}
