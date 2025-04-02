import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import axios from 'axios';


const AuthForm = () => {
  const [activeForm, setActiveForm] = useState('login');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userType, setUserType] = useState('normal');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const login = async (event) => {
    event.preventDefault();
    console.log('Logging in with:', email, password);
    
     // Logg dataene som sendes til API-en
     console.log('Sending data:', { email, password });
    try {
      const response = await axios.post('/api/login', { email, password });
      console.log('Response:', response); // Logg responsen fra API-en

      if (response.status === 200) {
        console.log('Login successful');
        localStorage.setItem('user', JSON.stringify(response.data)); // Lagre brukerdata
        navigate('/info');  // Naviger til informasjonssiden
      }
    } catch (error) {
      console.error('Login failed:', error.response ? error.response.data : error.message);
    
      setError('Invalid email or password');
    }
  };

  const signup = async (event) => {
    event.preventDefault();
    console.log('Signing up with:', email, password, userType);

    try {
      const response = await axios.post('/api/signup', { email, password, userType });
      if (response.status === 201) {
        console.log('Sign-up successful');
        setActiveForm('login');
      }
    } catch (error) {
      console.error('Sign up failed:', error);
      setError('There was an error signing up');
    }
  };

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
      <div className="auth-image-container" style={{ backgroundImage: `url(})` }}></div>
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
