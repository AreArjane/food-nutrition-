import React from 'react';
import ReactDOM from 'react-dom/client'; // Import React DOM for rendering
import { BrowserRouter as Router } from 'react-router-dom'; // Import React Router for routing
import App from './App'; // Import the main App component
import reportWebVitals from './reportWebVitals'; // Import for performance monitoring (optional)
import NutrientManager from './NutrientManager'; // Import the NutrientManager component (adjust the path if necessary)

// Create the root element for rendering the application
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Router>
      <App />
    </Router>
  </React.StrictMode>
);

// Render the NutrientManager component separately
ReactDOM.render(
  <React.StrictMode>
    <NutrientManager />
  </React.StrictMode>,
  document.getElementById('root') // Attach to the same root element
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
