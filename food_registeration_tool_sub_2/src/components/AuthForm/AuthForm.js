import React, { useState } from 'react';
import { Link } from 'react-router-dom';

const AuthForm = () => {
  const [activeForm, setActiveForm] = useState('login');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userType, setUserType] = useState('normal');
  const [error, setError] = useState('');

  const login = async (event) => {
    event.preventDefault();
    console.log('Logging in with:', email, password);
    // Logikk for innlogging...
  };

  const signup = async (event) => {
    event.preventDefault();
    console.log('Signing up with:', email, password, userType);
    // Logikk for registrering...
  };

  const resetPassword = async (event) => {
    event.preventDefault();
    console.log('Resetting password for:', email);
    // Logikk for tilbakestilling...
  };

  return (
    <div className="auth-form">
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