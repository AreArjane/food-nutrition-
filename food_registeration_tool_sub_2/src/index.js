import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router } from 'react-router-dom';
import App from './App';
import reportWebVitals from './reportWebVitals';
import NutrientManager from './NutrientManager'; // Juster stien hvis nødvendig

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Router>
      <App />
    </Router>
  </React.StrictMode>
);

ReactDOM.render(
  <React.StrictMode>
    <NutrientManager />
  </React.StrictMode>,
  document.getElementById('root')
);

// Define the URL for the analytics endpoint
const analyticsUrl = 'http://localhost:5072/analytics'; // Update to your backend analytics endpoint

// Function to send performance metrics to the backend
function sendToAnalytics(metric) {
  const body = JSON.stringify(metric);

  if (navigator.sendBeacon) {
    // Use navigator.sendBeacon for performance-friendly requests
    navigator.sendBeacon(analyticsUrl, body);
  } else {
    // Fallback to fetch for older browsers
    fetch(analyticsUrl, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body,
    }).catch((err) => console.error('Failed to send analytics:', err));
  }
}

// Pass the function to reportWebVitals for monitoring
reportWebVitals(sendToAnalytics);