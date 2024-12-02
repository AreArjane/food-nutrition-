import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';  // Use axios for API calls
import loginImage from '../../assets/images/login-image.jpg';

const AuthForm = () => {
  const [activeForm, setActiveForm] = useState('login');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userType, setUserType] = useState('normal');
  const [error, setError] = useState('');
  const navigate = useNavigate(); // Using useNavigate for redirection after login

  // Login function that sends data to the API
  const login = async (event) => {
    event.preventDefault();
    console.log('Logging in with:', email, password);
    
    try {
      // API call for logging the user in
      const response = await axios.post('/api/login', { email, password });
      
      // Check if login was successful
      if (response.status === 200) {
        console.log('Login successful');
        localStorage.setItem('user', JSON.stringify(response.data)); // Store user data in localStorage
        navigate('/food-list');  // After successful login, navigate to the food list page
      }
    } catch (error) {
      console.error('Login failed:', error);
      setError('Invalid email or password');
    }
  };

  // Sign-up function that sends data to the API
  const signup = async (event) => {
    event.preventDefault();
    console.log('Signing up with:', email, password, userType);

    try {
      // API call for registering the user
      const response = await axios.post('/api/signup', { email, password, userType });

      // Check if registration was successful
      if (response.status === 201) {
        console.log('Sign-up successful');
        setActiveForm('login'); // After registration, switch to login form
      }
    } catch (error) {
      console.error('Sign up failed:', error);
      setError('There was an error signing up');
    }
  };

  // Password reset function
  const resetPassword = async (event) => {
    event.preventDefault();
    console.log('Resetting password for:', email);

    try {
      const response = await axios.post('/api/reset-password', { email });
      if (response.status === 200) {
        console.log('Password reset email sent');
        setError('Check your email to reset your password');
      }
    } catch (error) {
      console.error('Password reset failed:', error);
      setError('Error resetting password');
    }
  };

  return (
    <div className="auth-form">
      {/* Image Container with inline style */}
      <div className="auth-image-container" style={{ backgroundImage: url(${loginImage}) }}></div>
      <nav>
        <Link to="#" onClick={() => setActiveForm('login')} className={activeForm === 'login' ? 'active' : ''}>Login</Link>
        <Link to="#" onClick={() => setActiveForm('signup')} className={activeForm === 'signup' ? 'active' : ''}>Sign Up</Link>
        <Link to="#" onClick={() => setActiveForm('reset')} className={activeForm === 'reset' ? 'active' : ''}>Forgot Password?</Link>
      </nav>

      {error && <p className="error-message">{error}</p>}

      {activeForm === 'login' && (
        <form onSubmit={login}>
          <h2>Login</h2>
          <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} required />
          <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required />
          <button type="submit">Login</button>
        </form>
      )}

      {activeForm === 'signup' && (
        <form onSubmit={signup}>
          <h2>Sign Up</h2>
          <input type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} required />
          <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required />
          <select value={userType} onChange={(e) => setUserType(e.target.value)}>
            <option value="normal">Normal User</option>
            <option value="super">Super User</option>
            <option value="admin">Admin User</option>
          </select>
          <button type="submit">Sign Up</button>
        </form>
      )}

      {activeForm === 'reset' && (
        <form onSubmit={resetPassword}>
          <h2>Reset Password</h2>
          <input type="email" placeholder="Enter your email" value={email} onChange={(e) => setEmail(e.target.value)} required />
          <button type="submit">Send Reset Link</button>
        </form>
      )}
    </div>
  );
};

export default AuthForm;
