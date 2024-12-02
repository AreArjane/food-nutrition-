import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router } from 'react-router-dom';
import App from './App';
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Router>
      <App />
    </Router>
  </React.StrictMode>
);

// Define the URL for the analytics endpoint
const analyticsUrl = 'http://localhost:5072/analytics'; // Oppdater til din backend analytics endpoint

// Funksjon for å sende ytelsesmålinger til backend
function sendToAnalytics(metric) {
  console.log('Sending metric:', metric); // Debugging
  const body = JSON.stringify(metric);

  if (navigator.sendBeacon) {
    navigator.sendBeacon(analyticsUrl, body);
  } else {
    fetch(analyticsUrl, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body,
    }).catch((err) => console.error('Failed to send analytics:', err));
  }
}

// Passer funksjonen til reportWebVitals for overvåking
reportWebVitals(sendToAnalytics);
