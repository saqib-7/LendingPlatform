import { useState } from "react";

// Collects loan details, performs simple browser-side checks, and submits them to the parent.
export default function LoanApplicationForm({ onSubmit, isSubmitting }) {
  const [values, setValues] = useState({
    loanAmount: "",
    assetValue: "",
    creditScore: "",
  });
  const [validationError, setValidationError] = useState("");

  // Updates one field as the person types into the form.
  function handleChange(event) {
    setValues({ ...values, [event.target.name]: event.target.value });
  }

  // Checks the required numeric fields before calling the backend.
  function handleSubmit(event) {
    event.preventDefault();
    const loanAmount = Number(values.loanAmount);
    const assetValue = Number(values.assetValue);
    const creditScore = Number(values.creditScore);

    if (!values.loanAmount || !values.assetValue || !values.creditScore) {
      setValidationError("Please complete all three fields.");
      return;
    }
    if (
      !Number.isFinite(loanAmount) ||
      !Number.isFinite(assetValue) ||
      !Number.isFinite(creditScore) ||
      loanAmount <= 0 ||
      assetValue <= 0
    ) {
      setValidationError("Please enter positive numeric values.");
      return;
    }
    if (
      !Number.isInteger(creditScore) ||
      creditScore < 1 ||
      creditScore > 999
    ) {
      setValidationError("Credit score must be a whole number from 1 to 999.");
      return;
    }

    setValidationError("");
    onSubmit({ loanAmount, assetValue, creditScore });
  }

  return (
    <section className="panel form-panel">
      <div>
        <p className="eyebrow">New application</p>
        <h2>Check a loan decision</h2>
        <p className="muted">
          Enter the applicant’s details and let the lending API make the
          decision.
        </p>
      </div>
      <form onSubmit={handleSubmit} noValidate>
        <label htmlFor="loanAmount">Loan amount (£)</label>
        <input
          id="loanAmount"
          name="loanAmount"
          type="number"
          min="0.01"
          step="0.01"
          value={values.loanAmount}
          onChange={handleChange}
          placeholder="100000-1500000"
          required
        />

        <label htmlFor="assetValue">Asset value (£)</label>
        <input
          id="assetValue"
          name="assetValue"
          type="number"
          min="0.01"
          step="0.01"
          value={values.assetValue}
          onChange={handleChange}
          required
        />

        <label htmlFor="creditScore">Credit score (1–999)</label>
        <input
          id="creditScore"
          name="creditScore"
          type="number"
          min="1"
          max="999"
          step="1"
          value={values.creditScore}
          onChange={handleChange}
          placeholder="1-999"
          required
        />

        {validationError && (
          <p className="form-error" role="alert">
            {validationError}
          </p>
        )}
        <button type="submit" disabled={isSubmitting}>
          {isSubmitting ? "Checking…" : "Submit application"}
        </button>
      </form>
    </section>
  );
}
