import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router } from 'react-router-dom'; // Import BrowserRouter
import App from './App';
import reportWebVitals from './reportWebVitals';
import AuthForm from './components/AuthForm/AuthForm'; // Adjust the path if necessary

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Router> {/* Wrap your App with Router */}
    <App />
    </Router>
  </React.StrictMode>
);

// Define sendToAnalytics function
function sendToAnalytics(metric) {
  console.log(metric); // Log to console or send to an endpoint
  const body = JSON.stringify(metric);
  const url = 'https://example.com/analytics'; // Replace with your URL
  if (navigator.sendBeacon) {
    navigator.sendBeacon(url, body);
  } else {
    fetch(url, { body, method: 'POST', keepalive: true });
  }
}

// Measure performance
reportWebVitals(sendToAnalytics);