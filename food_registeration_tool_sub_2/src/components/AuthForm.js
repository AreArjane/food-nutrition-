import React, { useState } from 'react';

const AuthForm = () => {
  const [activeForm, setActiveForm] = useState('login'); // 'login', 'signup', or 'reset'
  const [error, setError] = useState(''); // State to hold error messages
  const [userType, setUserType] = useState('normal'); // Default user type

 
  const showLogin = () => setActiveForm('login');
  const showSignup = () => setActiveForm('signup');
  const showPasswordReset = () => setActiveForm('reset');

  const login = async (event) => {
    event.preventDefault();
    const username = event.target.username.value;
    const password = event.target.password.value;
    try {
        console.log('Logging in with:', username, password); // Logging innlogging
        const response = await fetch('/api/auth/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ username, password }), // Send username and passord like JSON
        });
  
        if (!response.ok) {
          const errorData = await response.json();
          throw new Error(errorData.message || 'Login failed. Please try again.');
        }
  
        const data = await response.json();
        console.log('Login successful:', data); // Logg inn suksess
        setError(''); // Nullstill feilmeldinger
      } catch (err) {
        console.error('Error during login:', err);
        setError(err.message); // Sett feilmelding for visning
      }
    };
    
    

  const signup = async (event) => {
    event.preventDefault();
    const email = event.target.email.value;
    const password = event.target.password.value;


     try {
        console.log('Signing up with:', email, password, 'User Type:', userType); // Logging registrering
        const response = await fetch('/api/auth/signup', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ email, password }), // Send e-post and passord like JSON
        });
         
  
        if (!response.ok) {
          const errorData = await response.json();
          throw new Error(errorData.message || 'Signup failed. Please try again.');
        }
  
        const data = await response.json();
        console.log('Signup successful:', data);
        setError(''); // Nullstill feilmeldinger
      } catch (err) {
        console.error('Error during signup:', err);
        setError(err.message); // Sett feilmelding for visning
      }
    
  };

  const resetPassword = (event) => {
    event.preventDefault();
    const email = event.target.email.value;
    console.log('Sending reset link to:', email); // Implement password reset logic
    
  };


  return (
    <div className="auth-form"> {/* Legg til klasse for styling */}
      <nav>
        <button onClick={showLogin}>Login</button>
        <button onClick={showSignup}>Sign Up</button>
        <button onClick={showPasswordReset}>Forgot Password?</button>
      </nav>

      {error && <p className="error-message">{error}</p>} {/* show feil message */}

      {activeForm === 'login' && (
        <form onSubmit={login}>
          <h2>Login</h2>
          <input type="text" name="username" placeholder="Username or Email" required />
          <input type="password" name="password" placeholder="Password" required />
          <button type="submit">Login</button>

        </form>
      )}

      {activeForm === 'signup' && (
        <form onSubmit={signup}>
          <h2>Sign Up</h2>
          <input type="email" name="email" placeholder="Email" required />
          <input type="password" name="password" placeholder="Password" required />

          {/* Brukertype velger */}
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
          <input type="email" name="email" placeholder="Enter your email" required />
          <button type="submit">Send Reset Link</button>
        </form>
      )}
    </div>
  );
};

export default AuthForm;