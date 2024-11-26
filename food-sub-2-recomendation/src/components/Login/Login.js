import React from 'react';
import './Login.css';

const Login = () => {
    const handleLogin = () => {
        alert('Login form submitted');
    };

    return (
        <div className="login-container">
            <h1>Select User Type</h1>
            <div className="user-types">
                <a href="/normaluser" className="user-type-button">NormalUser</a>
                <a href="/superuser" className="user-type-button">SuperUser</a>
                <a href="/adminuser" className="user-type-button">AdminUser</a>
                <img
                    src="https://res.cloudinary.com/dnwuimgi4/image/upload/v1731605769/loging_icxsj8.svg"
                    className="highlight"
                    alt="Login Highlight"
                />
            </div>

            <div id="LoginForm" style={{ display: 'none', marginTop: '20px' }}>
                <h2>Login</h2>
                <form>
                    <div>
                        <label htmlFor="email">Email:</label>
                        <input type="email" id="email" name="email" required />
                    </div>
                    <div>
                        <label htmlFor="password">Password:</label>
                        <input type="password" id="password" name="password" required />
                    </div>
                    <button type="button" onClick={handleLogin}>Login</button>
                </form>
                <div id="errorMessages" style={{ color: 'red', display: 'none' }}></div>
            </div>
        </div>
    );
};

export default Login;
