const AuthForm = () => {
  const [activeForm, setActiveForm] = useState('login');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userType, setUserType] = useState('normal');
  const [error, setError] = useState('');

  const handleResponse = async (response) => {
    const data = await response.json();
    if (data.success) {
      if (data.redirectUrl) {
        window.location.href = data.redirectUrl;
      }
    } else {
      setError(data.errorMessage || 'Something went wrong. Please try again.');
    }
  };

  const login = async (event) => {
    event.preventDefault();
    try {
      const response = await fetch('/Auth/verify', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'X-CSRF-TOKEN': document.querySelector('input[name="__RequestVerificationToken"]').value,
        },
        body: JSON.stringify({
          email,
          password,
          userType: userType === 'normal' ? 0 : userType === 'super' ? 1 : 2, // Match enum values
        }),
      });
      await handleResponse(response);
    } catch (error) {
      console.error('Login error:', error);
      setError('Login failed. Please try again.');
    }
  };

  const signup = async (event) => {
    event.preventDefault();
    // Call your registration endpoint (not implemented in provided AuthController)
    console.log('Sign-up logic not implemented in backend yet.');
  };

  const resetPassword = async (event) => {
    event.preventDefault();
    // Call your password reset endpoint (not implemented in provided AuthController)
    console.log('Password reset logic not implemented in backend yet.');
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
          <select value={userType} onChange={(e) => setUserType(e.target.value)}>
            <option value="normal">Normal User</option>
            <option value="super">Super User</option>
            <option value="admin">Admin User</option>
          </select>
          <button type="submit">Login</button>
        </form>
      )}

      {/* Similar forms for Sign-Up and Reset Password */}
    </div>
  );
};
