import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);

// Function to create the login form
function createLoginForm() {
  const container = document.createElement('div');
  container.className = 'container';
  container.innerHTML = `
      <h1>Login</h1>
      <form id="loginForm">
          <input type="text" placeholder="Username" id="username" required />
          <input type="password" placeholder="Password" id="password" required />
          <button type="submit">Login</button>
      </form>
      <p>Don't have an account? <a href="#" onclick="showSignup()">Sign Up</a></p>
  `;
  return container;
}

// Function to create the signup form
function createSignupForm() {
  const container = document.createElement('div');
  container.className = 'container';
  container.innerHTML = `
      <h1>Sign Up</h1>
      <form id="signupForm">
          <input type="text" placeholder="Username" id="signupUsername" required />
          <input type="email" placeholder="Email" id="email" required />
          <input type="password" placeholder="Password" id="signupPassword" required />
          <button type="submit">Sign Up</button>
      </form>
      <p>Already have an account? <a href="#" onclick="showLogin()">Login</a></p>
  `;
  return container;
}

// Function to handle login form submission
function handleLogin(event) {
  event.preventDefault();
  const username = document.getElementById('username').value;
  const password = document.getElementById('password').value;
  console.log('Login attempted with:', { username, password });
}

// Function to handle signup form submission
function handleSignup(event) {
  event.preventDefault();
  const username = document.getElementById('signupUsername').value;
  const email = document.getElementById('email').value;
  const password = document.getElementById('signupPassword').value;
  console.log('Signup attempted with:', { username, email, password });
}

// Function to show the login form
function showLogin() {
  const root = document.getElementById('root');
  root.innerHTML = '';
  const loginForm = createLoginForm();
  root.appendChild(loginForm);
  document.getElementById('loginForm').addEventListener('submit', handleLogin);
}

// Function to show the signup form
function showSignup() {
  const root = document.getElementById('root');
  root.innerHTML = '';
  const signupForm = createSignupForm();
  root.appendChild(signupForm);
  document.getElementById('signupForm').addEventListener('submit', handleSignup);
}

// Initial rendering of the login form
showLogin();

// Define the sendToAnalytics function
function sendToAnalytics(metric) {
  console.log(metric); // Log to console or send to an endpoint
  // Example for sending to an endpoint:
  const body = JSON.stringify(metric);
  const url = 'https://example.com/analytics'; // Replace with your URL
  if (navigator.sendBeacon) {
    navigator.sendBeacon(url, body);
  } else {
    fetch(url, { body, method: 'POST', keepalive: true });
  }
}


// Measure performance and log results or send to analytics

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals(sendToAnalytics);
